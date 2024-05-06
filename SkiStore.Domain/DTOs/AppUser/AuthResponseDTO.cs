// Ignore Spelling: Auth

namespace SkiStore.Domain.DTOs.AppUser
{
    public class AuthResponseDTO
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
