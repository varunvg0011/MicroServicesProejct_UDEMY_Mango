using Mango.Services.AuthAPI.data;
using Mango.Services.AuthAPI.IServices;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.DTO;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTTokenGenerator _tokenGenerator;

        public AuthService(ApplicationDBContext dBContext,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJWTTokenGenerator tokenGenerator)
        {
            _dBContext = dBContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
        }
        

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginDTO)
        {
            var user = _dBContext.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == 
                loginDTO.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            //up until here we checked if user has entered correct password and username

            if(user==null || isValid == false)
            {
                //incase the user has entered wrong credentials
                return new LoginResponseDTO()
                {
                    User = null,
                    Token = ""
                };
            }
            //If user was found, Generate JWT Token

            var token = _tokenGenerator.GenerateToken(user);

            UserDTO userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                User = userDTO,
                //for now keeping token empty, we need to generate it and assign it to this var later
                Token = token
            };
            return loginResponseDTO;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _dBContext.ApplicationUsers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                //GetAwaiter().GetResult() is added to make this synchronous as the func is async and we dont
                //want to use await here
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<string> Register(RegisterRequestDTO registerRequestDTO)
        {
            //to register a user, we need Application user and add the properties required by the user to
            //register as we have added in our AppUser class. 
            //we are making a conversion here because only then we will be able to add it to our Identity user
            //class which is ApplicationUser.
            ApplicationUser user = new()
            {
                UserName = registerRequestDTO.Email,
                Name = registerRequestDTO.Name,
                NormalizedEmail = registerRequestDTO.Email.ToUpper(),
                Email = registerRequestDTO.Email,
                PhoneNumber = registerRequestDTO.PhoneNumber
            };
            try
            {
                //registering the user using above details and password
                var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);
                if (result.Succeeded)
                {
                    //this will give us the user
                    var userToReturn = _dBContext.ApplicationUsers.First(u=> u.UserName.ToLower() == registerRequestDTO.Email.ToLower());

                    //based on that user, we can populate our userDTO and return it
                    UserDTO userDTO = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
                
            }
            catch (Exception ex)
            {

                
            }
            //incase of exception
            return "Error Encountered";
        }
    }
}
