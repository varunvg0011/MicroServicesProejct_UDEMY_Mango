
using Mango.WebApp.Models;


namespace Mango.WebApp.Service.IService
{
    public interface IAuthService
    {
        //make API calls here
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO); 
        Task<ResponseDTO?> RegisterAsync(RegisterRequestDTO regRequestDTO); 
        Task<ResponseDTO?> AssignRoleAsync(RegisterRequestDTO regRequestDTO); 
    }
}
