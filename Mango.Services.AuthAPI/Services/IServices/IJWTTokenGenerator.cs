﻿using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser appUser);
    }
}
