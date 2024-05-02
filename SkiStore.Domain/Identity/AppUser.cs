// Ignore Spelling: App

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkiStore.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        #region Column
        public string DisplayName { get; set; }

        #endregion
        #region RS
        #region User-Address RS
        //user must has one address
        //column AddressID
        //required
        //navigation property
        //public Address AppUserAddress { get; set; }
        #endregion
        #endregion
    }
}
