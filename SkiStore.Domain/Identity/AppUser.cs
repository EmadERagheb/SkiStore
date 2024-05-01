// Ignore Spelling: App

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SkiStore.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        #region Column
        public string DisplayName { get; set; }
        [Required]
        public int AddressId { get; set; }
        #endregion
        #region RS
        #region User-Address RS
        //user must has one address
        //column AddressID
        //required
        //navigation property
        public Address Address { get; set; }
        #endregion
        #endregion
    }
}
