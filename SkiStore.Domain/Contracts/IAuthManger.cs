// Ignore Spelling: Auth

using Microsoft.AspNetCore.Identity;
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
    }
}
