using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;

namespace OnlineShop.Services
{
    public class ProductServices: IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductServices> _logger;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<ListProductsDTO>> ListProducts()
        {
            try
            {
                var productList =  _mapper.Map<IEnumerable<ListProductsDTO>>(await _unitOfWork.Products.GetAllAsync(includes: new[] { "ProductFiles" }));
                return productList;
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
    }
}
