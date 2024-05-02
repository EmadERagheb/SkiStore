// Ignore Spelling: App

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkiStore.Domain.Identity
{
    
    public class Address
    {
        #region Columns
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string AppUserId { get; set; }
        #endregion
        #region Rs
        #region Address-User RS
        //user Has one Address
        //column AppUserId
        //required
        //navigation probertiy
        public AppUser AppUser { get; set; } = null!;
        #endregion
        #endregion

    }
}