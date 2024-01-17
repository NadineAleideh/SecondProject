using Microsoft.AspNetCore.Mvc.ModelBinding;
using SecondProject.Models.Dtos;
using System.Security.Claims;

namespace SecondProject.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState, ClaimsPrincipal User);
        public Task<UserDTO> Authenticate(string username, string password);

    }
}
