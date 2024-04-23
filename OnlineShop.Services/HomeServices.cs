using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.Constants;
using OnlineShop.Core.DTOs.HomeDTOs;
using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;


namespace OnlineShop.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger<HomeServices> _logger;
        public HomeServices(IUnitOfWork unitOfWork, ILogger<HomeServices> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IndexDTO> GetHomePageData()
        {
            IndexDTO indexDTO = new();
            
            // get banner images
            indexDTO.BannerImages = _unitOfWork.Banners.GetAllAsync()?.Result?.Select(i=>"Banner/"+ i.FileName)?.ToList();

            // 
            var mostDiscount = await _unitOfWork.Products.GetAllAsync(
                includes: new[] { "ProductFiles" }, 
                orderBy:i=>i.discount, 
                orderDirection: OrderDirections.Descending,
                take:10,
                skip:0
             );

            var lessPrices = await _unitOfWork.Products.GetAllAsync(
                includes: new[] { "ProductFiles" },
                orderBy: i => i.Price,
                orderDirection: OrderDirections.Ascending,
                take: 10,
                skip: 0
             );

            

            indexDTO.ProductSections = new List<ProductSectionDTO>
            {
                new ProductSectionDTO
                {
                    Title = "Top Offers",
                    ProductsList = _mapper.Map<List<ListProductsDTO>>(mostDiscount)
                },
                new ProductSectionDTO
                {
                    Title = "Low Prices",
                    ProductsList = _mapper.Map<List<ListProductsDTO>>(lessPrices)
                }

            };

            try
            {

            }catch ( Exception ex ) 
            {
                _logger.LogError(ex.Message);
            }


            return indexDTO;

        }
    }
}
