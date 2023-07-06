using Mango.WebApp.Models;
using Mango.WebApp.Models.DTO;

namespace Mango.WebApp.Service.IService
{
    public interface ICouponService
    {
        //we are using this coupon Service to call our CouponAPI so we also need Coupon DTO as CouponDTO
        //is the type of API model. so copy that
        Task<ResponseDTO?> GetCouponAsync(string couponCode);
        Task<ResponseDTO?> GetAllCouponsAsync();
        Task<ResponseDTO?> GetCouponByIDAsync(int id);
        Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO);
        Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO);
        Task<ResponseDTO?> DeleteCouponAsync(int id);
        
    }
}
