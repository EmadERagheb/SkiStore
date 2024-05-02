using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.AppUser;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthManger _manger;

        public AccountsController(IAuthManger manger)
        {
            _manger = manger;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDTO)
        {
            var response = await _manger.LoginAsync(loginDTO);
            return response is not null ? Ok(response) : Unauthorized(new APIResponse(401));

        }
        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponseDTO>>Register(RegisterDTO registerDTO)
        {
          var errors=  await _manger.RegisterAsync(registerDTO);
            if(!errors.Any())
            {
                return await _manger.LoginAsync(new LoginDTO() { Email=registerDTO.Email, Password=registerDTO.Password});
            }
            return BadRequest(new APIValidationErrorResponse(errors.Select(e=>e.Description).ToList()));
        }
    }
}
