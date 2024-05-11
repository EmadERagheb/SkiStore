namespace SkiStore.Domain.Models
{
    public class Product : BaseDomainModel
    {
        #region Columns
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public int BrandId { get; set; }

        public int ProductTypeId { get; set; }
        #endregion
        #region Relations
        #region Product-Brand RS 
        //product must have one Brand
        //column ProductBrandId
        //required
        //navigation Property
        public Brand Brand { get; set; }
        #endregion
        #region Product-ProductType RS
        //product must have one ProductType
        // column ProductTypeId
        //required
        //navigation Property 
        public ProductType ProductType { get; set; }
        #endregion








        #endregion


    }
}
