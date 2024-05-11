using SkiStore.Domain.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkiStore.Domain.ModelLists
{
    public static class DeliveryMethodList
    {
        public static List<DeliveryMethod> DeliveryMethods { get; set; } = JsonSerializer.Deserialize<List<DeliveryMethod>>(File.ReadAllText(@"..\SkiStore.Data\Seeding Data\delivery.json"));
    }
}
