﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;
using OnlineShop.Infrastructure.Data;
using System.Security.Cryptography;


namespace OnlineShop.Services
{
    public class ProductServices: IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductServices> _logger;
        private readonly ApplicationDbContext _context;
        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductServices> logger, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }
        public async Task<ProductPageDTO> ListProducts(int take, int skip, string? sort, decimal? minprice, decimal? maxprice)
        {
            try
            {
                IQueryable<Product> products = _context.Products;
                if (sort == "asc")
                    products = products.OrderBy(i => (i.Price - (i.Price * i.discount / 100)));
                else if(sort == "des")
                    products = products.OrderByDescending(i => (i.Price - (i.Price * i.discount / 100)));

                if(minprice != null)
                    products = products.Where(i=> (i.Price - (i.Price * i.discount / 100)) > minprice);
                if(maxprice != null)
                    products = products.Where(i => (i.Price - (i.Price * i.discount / 100)) < maxprice);

                /*if (sort == "asc")
                {
                    products = _mapper.Map<IEnumerable<ListProductsDTO>>(
                        await _unitOfWork.Products.GetAllAsync(include:i=>
                            i.Include(a=>a.ProductFiles), 
                            take: take, 
                            skip: take * skip,
                            orderBy: i => i.OrderBy(p => p.Price)
                        ));
                }
                */
                var productsCount = await _unitOfWork.Products.GetCount();
                return new ProductPageDTO
                {
                    ProductList = _mapper.Map<IEnumerable<ListProductsDTO>>(await products.Include(a => a.ProductFiles).Skip(take * skip).Take(take).ToListAsync()),
                    pages =  (productsCount / take) + 1,
                    minPrice = _context.Products.Min(i => (i.Price - (i.Price * i.discount / 100))),
                    maxPrice = _context.Products.Max(i => (i.Price - (i.Price * i.discount / 100)))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ProductPageDTO();
            }
        }

        public async Task<ProductDetailsDTO> ProductDetails(string Id)
        {
            // new[] { "ProductFiles", "Category", "Brand" }
            var product =  await _unitOfWork.Products.FindAsync(
                i => i.Id == Id, 
                include:i => i.Include(a=>a.ProductFiles).Include(i=>i.Category).Include(i=>i.Brand) );
            return _mapper.Map<ProductDetailsDTO>(product);

        }

        public async Task<IEnumerable<ListProductsDTO>> GetProductByCategoryIdOrBrandId(string Id, string productId)
            => _mapper.Map<IEnumerable<ListProductsDTO>>(
                await _unitOfWork.Products.FindAllAsync(
                    i => i.Id != productId && (i.CategoryId == Id || i.BrandId == Id),
                    include: i => i.Include(a => a.ProductFiles), 
                    take:10, 
                    skip:0
                   )
                );

        public async Task<IEnumerable<ListProductsDTO>> Search(string query, int take = 5)
        {
            try
            {
                return _mapper.Map<IEnumerable<ListProductsDTO>>(
                        await _unitOfWork.Products.FindAllAsync(
                            expression:i=>i.Name.ToLower().Contains(query.ToLower()),
                            include: i => i.Include(a => a.ProductFiles),
                            take: take,
                            skip:0
                            )
                        );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<ListProductsDTO>();
            }
        }

        
        public async Task<BaseResponseDTO> AddProduct(FormProductDTO productDTO, List<ProductFile> uploadedFiles, string CreatedBy)
        {
            try
            {
                if (string.IsNullOrEmpty(CreatedBy))
                    return new BaseResponseDTO
                    {
                        IsSuccessed = false,
                        Message = "Invalid User"
                    };
                if (await _unitOfWork.Categories.GetById(productDTO.CategoryId) == null)
                    return new BaseResponseDTO
                    {
                        Message="Category Not Found"
                    };

                if (await _unitOfWork.Brands.GetById(productDTO.BrandId) == null)
                    return new BaseResponseDTO
                    {
                        Message = "Brand Not Found"
                    };

                var product = _mapper.Map<Product>(productDTO);
                product.CreatedBy = CreatedBy;
                foreach (var file in product?.ProductFiles)
                {
                    file.ProductId = product.Id;
                    await _unitOfWork.ProductFiles.AddAsync(file);
                }
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveAsync();
                return new BaseResponseDTO
                {
                    IsSuccessed = true,
                    Message = $"Product {product.Name} added Successfully"
                };
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BaseResponseDTO 
                {
                    IsSuccessed = false,
                    Message = "Something went wrong !"
                };
            }
            
        }

        public async Task<BaseResponseDTO> SeedProduct(SeedProductDTO productDTO, string CreatedBy)
        {
            if(string.IsNullOrEmpty(CreatedBy))
                return new BaseResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Invalid User"
                };
            var product = _mapper.Map<Product>(productDTO);


            if (await _unitOfWork.Categories.FindAsync(i=>i.Id == productDTO.CategoryId) == null)
            {
                await _unitOfWork.Categories.AddAsync(new Category
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = CreatedBy,
                    Id = productDTO.CategoryId,
                    Name = productDTO.Category,

                });

            }
            if (await _unitOfWork.Brands.FindAsync(i => i.Id == productDTO.BrandId) == null)
            {
                await _unitOfWork.Brands.AddAsync(new Brand
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = CreatedBy,
                    Id = productDTO.BrandId,
                    Name = productDTO.Brand,

                });
            }



            foreach (var file in product?.ProductFiles)
            {
                await _unitOfWork.ProductFiles.AddAsync(file);
            }
            await _unitOfWork.Products.AddAsync(product);

            await _unitOfWork.SaveAsync();

            return new BaseResponseDTO 
            {
                IsSuccessed = true,
                Message = $"Product {product.Name} added successfully!"
            };
            

        }

        public async Task<BaseResponseDTO> SeedProductItem(string createdBy)
        {
            Random rnd = new Random();
            var randomNumber = new byte[8];
            if (string.IsNullOrEmpty(createdBy))
                return new BaseResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Invalid User"
                };


            foreach (var item in await _unitOfWork.Products.GetAllAsync())
            {
                 for (var i = 0; i < rnd.Next(10);  i++)
                {
                    using var generator = RandomNumberGenerator.Create();
                    generator.GetBytes(randomNumber);

                    await _unitOfWork.ProductItems.AddAsync(new ProductItem
                    {
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = createdBy,
                        ProductId = item.Id,
                        SerialNo = Convert.ToBase64String(randomNumber),
                    });
                }

            }
            await _unitOfWork.SaveAsync();
            return new BaseResponseDTO 
            {
                IsSuccessed = true,
                Message = "Product items Seeds Successfully !"
            };
        }
        
    }
}
