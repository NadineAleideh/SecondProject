﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SecondProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SecondProject.Services
{
    public class JwtTokenService
    {
        private IConfiguration configuration;
        private SignInManager<ApplicationUser> signInManager;

        public JwtTokenService(IConfiguration config, SignInManager<ApplicationUser> manager)
        {
            configuration = config;
            signInManager = manager;
        }

        public static TokenValidationParameters GetValidationPerameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null)
            {
                throw new InvalidOperationException("JWT: Secret key is not exist");
            }

            var secretBytes = Encoding.UTF8.GetBytes(secret);

            return new SymmetricSecurityKey(secretBytes);
        }



        public async Task<string> GetToken(ApplicationUser user, TimeSpan expiresIn)
        {
            var principle = await signInManager.CreateUserPrincipalAsync(user);

            if (principle == null)
            {
                return null;
            }
            var signingKey = GetSecurityKey(configuration);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(signingKey,
                SecurityAlgorithms.HmacSha256),
                claims: principle.Claims

                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

    }
}
