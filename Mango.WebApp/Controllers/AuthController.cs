using Mango.WebApp.Models;
using Mango.WebApp.Models.Utility;
using Mango.WebApp.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new();
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{ Text=SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem{ Text=SD.RoleCustomer, Value = SD.RoleCustomer}
            };

            ViewBag.RoleList = roleList;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDTO regRequestDTO)
        {
            ResponseDTO result = await _authService.RegisterAsync(regRequestDTO);
            ResponseDTO assignRole;
            if (result!= null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(regRequestDTO.Role))
                {
                    regRequestDTO.Role = SD.RoleCustomer;
                }
                assignRole = await _authService.AssignRoleAsync(regRequestDTO);
                if(assignRole!=null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Succesfull";
                    return RedirectToAction(nameof(Login));
                }
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{ Text=SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem{ Text=SD.RoleCustomer, Value = SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View(regRequestDTO);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
