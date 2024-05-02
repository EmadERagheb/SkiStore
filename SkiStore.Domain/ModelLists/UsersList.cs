using Microsoft.AspNetCore.Identity;
using SkiStore.Domain.Identity;

namespace SkiStore.Domain.ModelLists
{
    public static class UsersList
    {
        public static List<AppUser> Users { get; set; } = new List<AppUser>()
        {
            new AppUser()
            {
                Id="c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                DisplayName="Emad",
                Email="Emaderagheb@gmail.com",
                UserName="Emaderagheb@gmail.com",
                PasswordHash=(new PasswordHasher<AppUser>()).HashPassword(null,"Pa$$word123")
            }
        };
    }
}
