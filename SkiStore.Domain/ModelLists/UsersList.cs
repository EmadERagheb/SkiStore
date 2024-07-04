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
                Email="emaderagheb@gmail.com",
                NormalizedEmail="emaderagheb@gmail.com".ToUpper(),
                UserName="emaderagheb@gmail.com",
                NormalizedUserName="emaderagheb@gmail.com".ToUpper(),
                EmailConfirmed=true,
                PasswordHash=(new PasswordHasher<AppUser>()).HashPassword(null,"Pa$$word123"),
            },  
            new AppUser()
            {
                Id="21c033a9-a5d5-4fee-97e3-5d8c51563060",
                DisplayName="Public",
                Email="public@skiNet.com",
                NormalizedEmail="public@skiNet.com".ToUpper(),
                UserName="public@skiNet.com",
                NormalizedUserName="public@skiNet.com".ToUpper(),
                EmailConfirmed=true,
                PasswordHash=(new PasswordHasher<AppUser>()).HashPassword(null,"Pa$$word123"),
            },
           
        };
    }
}
