using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Data.DTOs.Product
{
    public class PostProductDTO:BaseProductDTO
    {
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
    }
}
