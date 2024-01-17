using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondProject.Interfaces;
using SecondProject.Models.Dtos;

namespace SecondProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser userService;

        public UsersController(IUser service)
        {
            userService = service;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO data)
        {
            var user = await userService.Register(data, this.ModelState, User);

            if (ModelState.IsValid)
            {
                if (user != null)
                    return user;

                else
                    return NotFound();
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userService.Authenticate(loginDTO.Username, loginDTO.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            return user;
        }

    }
}
