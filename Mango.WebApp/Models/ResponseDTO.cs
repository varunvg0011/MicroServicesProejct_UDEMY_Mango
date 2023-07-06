namespace Mango.WebApp.Models
{
    public class ResponseDTO
    {

        public Object? Result { get; set; }
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
    }
}
