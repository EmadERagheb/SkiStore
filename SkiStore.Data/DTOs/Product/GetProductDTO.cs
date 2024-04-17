namespace SkiStore.API.DTOs.Product
{
    public class GetProductDTO:BaseProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string ProductType { get; set; }

    }
}
