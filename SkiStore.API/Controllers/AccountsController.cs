using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Address;
using SkiStore.Domain.DTOs.AppUser;
using System.Security.Claims;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthManger _manger;
      

        public AccountsController(IAuthManger manger,IUnitOfWork unitOfWork)
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
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO registerDTO)
        {
            if (await _manger.IsMailExistsAsync(registerDTO.Email))
            {
                return BadRequest(new APIValidationErrorResponse(new List<string>() { $"Username '{registerDTO.Email}' is already taken." }));
            }
            var errors = await _manger.RegisterAsync(registerDTO);
            if (!errors.Any())
            {
                return await _manger.LoginAsync(new LoginDTO() { Email = registerDTO.Email, Password = registerDTO.Password });
            }
            return BadRequest(new APIValidationErrorResponse(errors.Select(e => e.Description).ToList()));
        }
        [Authorize]
        [HttpGet("getCurrentUser")]
        public async Task<ActionResult<AuthResponseDTO>> GetCurrentUser()
        {
            string? email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email)?.Value;
            AuthResponseDTO result = default;

            if (!string.IsNullOrEmpty(email))
            {
                result = await _manger.GetCurrentUserAsync(email);
            }
            return result is not null ? Ok(result) : BadRequest();

        }



        [HttpGet("isEmailExists")]
        public async Task<ActionResult<bool>> IsMailExists(string email)
        {
            return await _manger.IsMailExistsAsync(email);
        }
        [Authorize]
        [HttpGet("getUserAddress")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var existingMail = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(existingMail))
            {
                var address = await _manger.GetUserAddressAsync(existingMail);
                return Ok(address);
            }
            return BadRequest();

        }
        [Authorize]
        [HttpPut("updateAddress")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var mail = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(mail))
            {

                var result = await _manger.UpdateUserAddressAsync(mail, addressDTO);
             
                if (!result.Any())
                {
                    return Ok(addressDTO);
                }
                else return BadRequest(new APIValidationErrorResponse(result.Select(e => e.Description).ToList()));
            }
            else
                return BadRequest(new APIResponse(400, "email can not be empty or null"));
        }
    }
}
