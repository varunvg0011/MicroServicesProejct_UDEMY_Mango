
using Mango.WebApp.Models;


namespace Mango.WebApp.Service.IService
{
    public interface IBaseService
    {
        //make API calls here
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO); 
    }
}
