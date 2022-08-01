namespace Store.Dto
{
    public class CatergoryNameWithProductName
    {

        public CatergoryNameWithProductName()
        {
            productDot = new List<ProductDto>();
        }
        public int CatId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductDto> productDot { get; set; }



    }



    public class ProductDto
    {
        public string productName { get; set; }
        public string productDescription { get; set; }




    }


}

