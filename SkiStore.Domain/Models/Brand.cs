namespace SkiStore.Domain.Models
{
    public class Brand : BaseDomainModel
    {
        #region Columns
        public string Name { get; set; }
        #endregion


        #region Relations
        #region Brand-Product-RS
        //Brand  Has Many product
        // Brand Must Have Products
        //No Column 
        //Navigation Property
        public List<Product> Products { get; set; } = new List<Product>();
        #endregion





        #endregion
    }
}
