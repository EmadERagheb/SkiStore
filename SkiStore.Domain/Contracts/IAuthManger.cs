// Ignore Spelling: Auth

using Microsoft.AspNetCore.Identity;
using SkiStore.Domain.DTOs.Address;
using SkiStore.Domain.DTOs.AppUser;
using SkiStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.Contracts
{
    public interface IAuthManger
    {
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<IEnumerable<IdentityError>> RegisterAsync(RegisterDTO registerDTO);
        Task<string> GenerateTokenAsync(AppUser user);
        Task<AuthResponseDTO> GetCurrentUserAsync(string email);
        Task<bool> IsMailExistsAsync(string email);
        Task<AddressDTO> GetUserAddressAsync(string email);
        Task<IEnumerable<IdentityError>> UpdateUserAddressAsync(string email, AddressDTO addressDTO);
    }
}
