using SkiStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.ModelLists
{
    public static class AddressList
    {
        public static List<Address> Addresses { get; set; }= new List<Address>()
        {
            new Address()
            {
                Id = 1,
                FirstName="Emad",
                LastName="Ragheb",
                AppUserId="c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                City="Cairo",
                State="6th-October",
                Street="Harm City",
                ZipCode="123456",
            }
        };
    }
}
