using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.Constants;
using OnlineShop.Core.DTOs.BrandDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;
using OnlineShop.Infrastructure.Data;


namespace OnlineShop.Services
{
    public class BrandServices : IBrandServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandServices> _logger;
        private readonly ApplicationDbContext _context;
        public BrandServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BrandServices> logger, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<GetBrandDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<GetBrandDTO>>(await _unitOfWork.Brands.GetAllAsync());
        }
        public async Task<IEnumerable<GetBrandDTO>> GetAllWithBaseIncludes(int size)
        {
            var Brands =
                await _context
                        .Brands
                        .Include(i => i.Products)
                        .OrderByDescending(i => i.Products.Count())
                        .Take(size)
                        .Skip(0)
                        .ToListAsync();
            /*await _unitOfWork.Brands.GetAllAsync(
                includes: new[] { "Products" },
                orderBy: i => i.Products.Count(),
                orderDirection: OrderDirections.Descending,
                take: size,
                skip: 0
              );*/
            return _mapper.Map<IEnumerable<GetBrandDTO>>(
                Brands
                );
        }
        public async Task<GetBrandDTO> GetById(string id)
        {
            return _mapper.Map<GetBrandDTO>(await _unitOfWork.Brands.GetById(id));
        }
        /*public async Task<IEnumerable<SimpleModule>> GetAsSelectList()
        {
            return _mapper.Map<IEnumerable<SimpleModule>>(
                    await _unitOfWork.Brands.GetAsSelectList(i => new SimpleModule { Id = i.Id, Name = i.Name })
                );
        }*/



        public async Task<IEnumerable<GetBrandDTO>> Search(string SearchQuery)
        {
            return _mapper.Map<IEnumerable<GetBrandDTO>>(
                await _unitOfWork.Brands.FindAllAsync(
                        i => i.Name.Contains(SearchQuery) || i.Id.Contains(SearchQuery)
                    ));

        }

        public async Task<BaseResponseDTO> AddNew(Brand newBrand, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponseDTO { Message = "Can't assign Transacation To user, user id is empty", IsSuccessed = false };
            if (newBrand.Name == null)
                return new BaseResponseDTO { Message = "Invalid Brand Name", IsSuccessed = false };

            /*else if (await _unitOfWork.Brands.FindAsync(i => i.Name == newBrand.Name) != null)
                return new BaseResponseDTO { Message = $"Brand with {newBrand.Name} Name Already Exisits", IsSuccessed = false };*/
            try
            {
                newBrand.CreatedBy = CreatedBy;
                await _unitOfWork.Brands.AddAsync(newBrand);
                await _unitOfWork.SaveAsync();
                return new BaseResponseDTO { Message = $"Brand {newBrand.Name} added Successfully", IsSuccessed = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newBrand.Name}", ex);
                return new BaseResponseDTO { Message = $"Something went wrong while adding {newBrand.Name}", IsSuccessed = false };
            }

        }
        /*public async Task<BaseResponse> Update(Brand updateBrand, string UpdatedBy)
        {
            if (string.IsNullOrEmpty(UpdatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (updateBrand.Name == null)
                return new BaseResponse { Message = "Invalid Brand Name", IsSucceeded = false };

            else if (await _unitOfWork.Brands.Find(i => i.Id == updateBrand.Id) == null)
                return new BaseResponse { Message = $"Brand Doesn't Exisit", IsSucceeded = false };


            try
            {
                updateBrand.UpdatedBy = UpdatedBy;
                await _unitOfWork.Brands.UpdateAsync(updateBrand);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Brand {updateBrand.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateBrand.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateBrand.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id, string DeletedBy)
        {
            if (string.IsNullOrEmpty(DeletedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (id == null)
                return new BaseResponse { Message = "Invalid Brand id", IsSucceeded = false };

            var Brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (Brand == null)
                return new BaseResponse { Message = "this Brand not found", IsSucceeded = false };
            try
            {
                Brand.DeletedBy = DeletedBy;
                await _unitOfWork.Brands.DeleteAsync(Brand);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Brand {Brand.Name} Deleted Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Deleting {Brand.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while Deleting {Brand.Name}", IsSucceeded = false };
            }
        }*/



    }
}