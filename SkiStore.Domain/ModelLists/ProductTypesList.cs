using SkiStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkiStore.Domain.ModelLists
{
    public static class ProductTypesList
    {
        public static List<ProductType> ProductTypes { get; set; } = JsonSerializer.Deserialize<List<ProductType>>(File.ReadAllText(@"..\SkiStore.Data\Seeding Data\types.json"));
    }
}
