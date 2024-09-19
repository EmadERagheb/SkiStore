// Ignore Spelling: Auth

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Address;
using SkiStore.Domain.DTOs.AppUser;
using SkiStore.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SkiStore.Data.Repositories
{
    public class AuthManger : IAuthManger
    {
        private readonly UserManager<AppUser> _manager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthManger(UserManager<AppUser> manager, IConfiguration configuration, IMapper mapper)
        {
            _manager = manager;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _manager.FindByEmailAsync(loginDTO.Email);
            if (user is not null)
            {
                bool result = await _manager.CheckPasswordAsync(user, loginDTO.Password);
                if (result)
                {
                    return new AuthResponseDTO
                    {
                        UserId = user.Id,
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = await GenerateTokenAsync(user),
                    };
                }

            }
            return null;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(RegisterDTO registerDTO)
        {
            AppUser newUser = new AppUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
            };
            IdentityResult resualt = await _manager.CreateAsync(newUser, registerDTO.Password);
            return resualt.Errors;
        }
        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            //Token Security
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            //Claims
            // Get Roles Claims
            var roles = await _manager.GetRolesAsync(user);
            List<Claim> roleClims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
            //Get User Claim From DB IF Exists
            var userClaims = await _manager.GetClaimsAsync(user);
            var claims = new List<Claim>()
            {
                new Claim (JwtRegisteredClaimNames.GivenName,user.DisplayName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            }.Union(roleClims).Union(userClaims);

            //build token
            var token = new JwtSecurityToken(
                issuer: _configuration["JWTSettings:Issuer"],
                audience: _configuration["JWTSettings:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWTSettings:DurationInMinutes"])),
                claims: claims,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<AuthResponseDTO> GetCurrentUserAsync(string email)
        {
            var user = await _manager.FindByEmailAsync(email);
            if (user is not null)
            {
                return new AuthResponseDTO()
                {
                    UserId = user.Id,
                    Email = email,
                    DisplayName = user.DisplayName,
                    Token = await GenerateTokenAsync(user)
                };
            }
            else
                return null;
        }


        public async Task<bool> IsMailExistsAsync(string email)
        {
            return await _manager.FindByEmailAsync(email) is null ? false : true;
        }

        public async Task<AddressDTO> GetUserAddressAsync(string email)
        {
            var user = await _manager.Users.Include(q => q.Address).FirstOrDefaultAsync(q => q.Email == email);
            if (user is not null)
            {
                AddressDTO addressDTO = _mapper.Map<AddressDTO>(user.Address);
                return addressDTO;
            }
            return null;
        }

        public async Task<IEnumerable<IdentityError>> UpdateUserAddressAsync(string email, AddressDTO addressDTO)
        {
            var user = await _manager.Users.Include(q => q.Address).FirstOrDefaultAsync(e => e.Email == email);
            if (user.Address is null)
            {
                user.Address = new Address();
            }
            _mapper.Map(addressDTO, user.Address);
            var result = await _manager.UpdateAsync(user);
            
            return result.Errors;
        }
    }
}
