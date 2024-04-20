namespace SkiStore.Data.Specifications
{
    public class ProductSpecPrams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize=6;
        public int PageSize
        {
            get => _pageSize ;
            set => _pageSize = value > 50 ? MaxPageSize : value;
        }
        public string? Sort { get; set; } 
        public int? BrandId { get; set; }
        public int? ProductTypeId { get; set; }

    }
}
