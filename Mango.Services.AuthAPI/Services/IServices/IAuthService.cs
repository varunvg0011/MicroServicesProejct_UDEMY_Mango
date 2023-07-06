using Mango.Services.AuthAPI.Models.DTO;

namespace Mango.Services.AuthAPI.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDTO registerRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginDTO);
        Task<bool> Login(string email, string roleName);

    }
}
