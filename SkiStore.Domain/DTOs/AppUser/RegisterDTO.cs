using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.DTOs.AppUser
{
    public class RegisterDTO:BaseAppUserDTO
    {
        [Required]
        public string DisplayName { get; set; }

    }
}
