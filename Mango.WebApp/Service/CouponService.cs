using Mango.WebApp.Models;
using Mango.WebApp.Models.DTO;
using Mango.WebApp.Models.Utility;
using Mango.WebApp.Service.IService;

namespace Mango.WebApp.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        //we need the url of coupon so we are adding it inside our SD class in utility folder
        public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.CouponAPIBase + "/api/coupon/",
                Data = couponDTO
            });
        }

        public async Task<ResponseDTO?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
                //coupon in above URL is case insensitive
            });
        }

        public async Task<ResponseDTO?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/Coupon/"
            });
        }

        public async Task<ResponseDTO?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDTO?> GetCouponByIDAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.CouponAPIBase + "/api/coupon/",
                Data = couponDTO
            });
        }
    }
}
