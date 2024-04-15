using SkiStore.Domain.Models;
using System.Text.Json;

namespace SkiStore.Domain.ModelLists
{
    public static class ProductList
    {

        public static List<Product> Products { get; set; } = JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(@"..\SkiStore.Data\Seeding Data\products.json"));
    }
}
