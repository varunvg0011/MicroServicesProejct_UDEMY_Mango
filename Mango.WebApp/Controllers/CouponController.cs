using Mango.WebApp.Models;
using Mango.WebApp.Models.DTO;
using Mango.WebApp.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.WebApp.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO>? list = new();
            ResponseDTO? response = await _couponService.GetAllCouponsAsync();
            if (response != null && response.IsSuccess)
            {
                //deserialize our response.result as it is not null
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);

        }


        public async  Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO couponDTOModel)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _couponService.CreateCouponAsync(couponDTOModel);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created succesfully";
                    return RedirectToAction("CouponIndex");
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            
            return View(couponDTOModel);
        }

        

        
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDTO? response = await _couponService.GetCouponByIDAsync(couponId);
            if (response != null && response.IsSuccess)
            {
                //deserialize our response.result as it is not null
                CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            //a 404 view can also be returned
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO couponDTO)
        {
            ResponseDTO? response = await _couponService.DeleteCouponAsync(couponDTO.CouponId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted succesfully";
                return RedirectToAction("CouponIndex");
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            //a 404 view can also be returned
            return View(couponDTO);
        }
    }
}
