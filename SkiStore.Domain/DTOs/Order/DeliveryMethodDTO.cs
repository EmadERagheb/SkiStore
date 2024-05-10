using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.DTOs.Order
{
    public class DeliveryMethodDTO
    {
        public int Id {  get; set; }
        public string ShortName { get; set; }

        public string DeliveryTime { get; set; }

        public string Description { get; set; }
        public Decimal Price { get; set; }
    }
}
