using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SecondProject.Interfaces;
using SecondProject.Models;
using SecondProject.Models.Dtos;
using System.Security.Claims;

namespace SecondProject.Services
{
    public class IdentityUserServices : IUser
    {
        //inject user manager service it's help us to manage related actions (CRUD), also it will do all the validations for us
        private UserManager<ApplicationUser> userManager;

        private JwtTokenService tokenService;
        public IdentityUserServices(UserManager<ApplicationUser> manager, JwtTokenService tokenService)
        {
            userManager = manager;
            this.tokenService = tokenService;
        }

        public async Task<UserDTO> Register(RegisterUserDTO RegisterUserDTO, ModelStateDictionary modelState, ClaimsPrincipal User)
        {


            var user = new ApplicationUser
            {
                UserName = RegisterUserDTO.Username,
                Email = RegisterUserDTO.Email,
                PhoneNumber = RegisterUserDTO.PhoneNumber
            };

            //now I will add the above new user info and create new user by the usermanager
            var result = await userManager.CreateAsync(user, RegisterUserDTO.Password);

            if (result.Succeeded)
            {

                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromHours(3)),
                };
            }
            else
            {


                foreach (var error in result.Errors)
                {
                    var errorKey = error.Code.Contains("Password") ? nameof(RegisterUserDTO.Password) :
                        error.Code.Contains("Email") ? nameof(RegisterUserDTO.Email) :
                        error.Code.Contains("Username") ? nameof(RegisterUserDTO.Username) :
                        "";

                    modelState.AddModelError(errorKey, error.Description);
                }
                return null;
            }

        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool validPassword = await userManager.CheckPasswordAsync(user, password);

            if (validPassword)
            {
                return
                    new UserDTO
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Token = await tokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
                    };
            }

            return null;

        }

    }
}
