using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.Constants;
using OnlineShop.Core.DTOs.HomeDTOs;
using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;
using OnlineShop.Infrastructure.Data;


namespace OnlineShop.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger<HomeServices> _logger;

        private readonly ApplicationDbContext _context;
        public HomeServices(IUnitOfWork unitOfWork, ILogger<HomeServices> logger, IMapper mapper, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IndexDTO> GetHomePageData()
        {
            IndexDTO indexDTO = new();
            
            // get banner images
            indexDTO.BannerImages = _unitOfWork.Banners.GetAllAsync()?.Result?.Select(i=>"Banner/"+ i.FileName)?.ToList();

            // 
            var mostDiscount = await _context.Products.OrderByDescending(i=>i.discount).Take(10).Skip(0).Include(i=>i.ProductFiles).ToListAsync();
              /* await _unitOfWork.Products.GetAllAsync(
                includes: new[] { "ProductFiles" }, 
                orderBy:i=>i.discount, 
                orderDirection: OrderDirections.Descending,
                take:10,
                skip:0
             );*/
            var desktopCategory = await _context.Categories.FirstOrDefaultAsync(i => i.Name == "Desktop");
            var lessPrices = await _context
                .Products
                .Where(i=> i.CategoryId == desktopCategory.Id)
                .OrderBy(i=>
                    i.Price 
                )
                .Take(10)
                .Skip(0)
                .Include(i => i.ProductFiles)
                .ToListAsync();
                /*await _unitOfWork.Products.GetAllAsync(
                includes: new[] { "ProductFiles" },
                orderBy: i => i.Price,
                orderDirection: OrderDirections.Ascending,
                take: 10,
                skip: 0
             );*/

            

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
