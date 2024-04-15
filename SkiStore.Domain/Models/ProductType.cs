namespace SkiStore.Domain.Models
{
    public class ProductType : BaseDomainModel
    {
        #region Column
        public string Name { get; set; }
        #endregion
        #region RelationShips

        #region ProductType-Product
        //ProductType Has Many Product
        //Required
        //noColumn
        //Navigation Property
        public List<Product> Products { get; set; } = new List<Product>();
        #endregion
        #endregion
    }
}
