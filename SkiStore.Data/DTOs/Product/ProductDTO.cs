using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Data.DTOs.Product
{
    public class ProductDTO:BaseProductDTO
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ProductTypeId { get; set; }
       

    }
}
