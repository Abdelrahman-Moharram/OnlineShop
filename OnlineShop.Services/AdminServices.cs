using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;


namespace OnlineShop.Services
{
    public class AdminServices: IAdminServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger<AdminServices> _logger;
        public AdminServices(IUnitOfWork unitOfWork, ILogger<AdminServices> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResponseDTO> AddBannerImages(List<Banner> Banners, string userId)
        {
            if(string.IsNullOrEmpty(userId))
                return new BaseResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Invalid User"
                };
            var siteSettings = _unitOfWork.SiteSettings.GetAllAsync().Result.FirstOrDefault();
            if (siteSettings == null) 
            {
                siteSettings = new SiteSetting
                {
                    CreatedBy = userId,
                    Images = Banners
                };
                await _unitOfWork.SiteSettings.AddAsync(siteSettings);
            }
            foreach (var banner in Banners)
            {
                banner.SiteSettingId = siteSettings.Id;
                await _unitOfWork.Banners.AddAsync(banner);
            }
            await _unitOfWork.SaveAsync();

            return new BaseResponseDTO 
            {
                IsSuccessed = true,
                Message = "Images Added Successfully"
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
                    Message = "something went wrong"
                };
            }
        }
    }
}
