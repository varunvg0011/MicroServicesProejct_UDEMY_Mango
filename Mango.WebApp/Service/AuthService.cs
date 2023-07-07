using Mango.WebApp.Models;
using Mango.WebApp.Models.Utility;
using Mango.WebApp.Service.IService;

namespace Mango.WebApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> AssignRoleAsync(RegisterRequestDTO regRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole",
                Data = regRequestDTO
            });
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/login",
                Data = loginRequestDTO
            });
        }

        public async Task<ResponseDTO?> RegisterAsync(RegisterRequestDTO regRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/register",
                Data = regRequestDTO
            });
        }
    }
}
