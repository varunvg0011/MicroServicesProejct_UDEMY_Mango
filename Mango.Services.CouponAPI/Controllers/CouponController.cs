using AutoMapper;
using Mango.Services.CouponAPI.data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/Coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDBContext _appDbContext;
        private IMapper _mapper;
        private APIResponseDTO _apiResponse;

        public CouponController(ApplicationDBContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _apiResponse = new APIResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public APIResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _appDbContext.Coupons.ToList();
                _apiResponse.Result = _mapper.Map <IEnumerable<CouponDTO>>(objList);               
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Result = null;
            }            
            return _apiResponse;
        }


        [HttpGet]
        [Route("{id:int}")]
        public APIResponseDTO Get(int id)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(c=>c.CouponId==id);
                _apiResponse.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message;
            }          
            return _apiResponse;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public APIResponseDTO GetByCode(string code)
        {
            try
            {
                //First vs FirstOrDeafult
                //First wil throw an error in case the result is null and other won't
                Coupon obj = _appDbContext.Coupons.FirstOrDefault(c => c.CouponCode.ToLower() == code.ToLower());
                if (obj == null)
                {
                    _apiResponse.IsSuccess = false;
                }
                _apiResponse.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message;
            }
            return _apiResponse;
        }


        [HttpPost]        
        public APIResponseDTO Post([FromBody] CouponDTO couponDTO)
        {
            try
            {
                //First vs FirstOrDeafult
                //First wil throw an error in case the result is null and other won't
                Coupon couponObj = _mapper.Map<Coupon>(couponDTO);
                _appDbContext.Coupons.Add(couponObj);
                _appDbContext.SaveChanges();
                _apiResponse.Message = "Succesfully Added";
                _apiResponse.Result = _mapper.Map<CouponDTO>(couponObj);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message;
            }
            return _apiResponse;
        }

        [HttpPut]
        public APIResponseDTO Update([FromBody] CouponDTO couponDTO)
        {
            try
            {
                //First vs FirstOrDeafult
                //First wil throw an error in case the result is null and other won't
                Coupon couponObj = _mapper.Map<Coupon>(couponDTO);
                _appDbContext.Coupons.Update(couponObj);
                _appDbContext.SaveChanges();
                _apiResponse.Message = "Succesfully Updated";
                _apiResponse.Result = _mapper.Map<CouponDTO>(couponObj);

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message;
            }
            return _apiResponse;
        }



        [HttpDelete]
        [Route("{id:int}")]
        public APIResponseDTO Delete(int id)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(c => c.CouponId == id);
                _appDbContext.Remove(obj);
                _appDbContext.SaveChanges();
                _apiResponse.Message = "Succesfully Deleted";

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message;
            }
            return _apiResponse;
        }
    }
}
