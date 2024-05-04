namespace SkiStore.Domain.DTOs.Product
{
    public class GetProductDTO:BaseProductDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ProductType { get; set; }

    }
}
