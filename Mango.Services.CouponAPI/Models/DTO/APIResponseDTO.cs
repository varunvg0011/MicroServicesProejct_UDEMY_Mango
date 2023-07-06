namespace Mango.Services.CouponAPI.Models.DTO
{
    public class APIResponseDTO
    {

        public Object? Result { get; set; }
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
    }
}
