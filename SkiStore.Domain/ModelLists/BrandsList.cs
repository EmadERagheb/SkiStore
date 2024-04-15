using SkiStore.Domain.Models;
using System.Text.Json;

namespace SkiStore.Domain.ModelLists
{
    public static class BrandsList
    {
        public static List<Brand> Brands { get; set; } = JsonSerializer.Deserialize<List<Brand>>(File.ReadAllText(@"..\SkiStore.Data\Seeding Data\brands.json"));

    }
}
