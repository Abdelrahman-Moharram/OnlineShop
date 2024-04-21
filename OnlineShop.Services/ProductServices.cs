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
        IUnitOfWork _unitOfWork { get; set; }
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
                var productList =  _mapper.Map<IEnumerable<ListProductsDTO>>(await _unitOfWork.Products.GetAllAsync(includes: new[] { "UploadedFiles" }));
                return productList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<ListProductsDTO>();
            }
        }

        
        public async Task<BaseResponseDTO> AddProduct(FormProductDTO productDTO, List<UploadedFile> uploadedFiles, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Bad Request"
                };
            var product = _mapper.Map<Product>(productDTO);
            product.CreatedBy = CreatedBy;
            foreach (var file in uploadedFiles)
            {
                file.ProductId = product.Id;
                await _unitOfWork.UploadedFiles.AddAsync(file);
            }
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return new BaseResponseDTO
            {
                IsSuccessed = true,
                Message = $"Product {product.Name} added Successfully"
            };
            try
            {
                
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
    }
}
