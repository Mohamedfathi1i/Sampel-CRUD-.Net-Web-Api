using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }

        [ForeignKey("category")]
        public int? Cat_ID { get; set; }

        public virtual Category? category { get; set; }


    }
}
