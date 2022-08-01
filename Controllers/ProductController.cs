using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Dto;
using Store.Models;
using Store.Repositry;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace Store.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IProductRepository productRepo,IWebHostEnvironment hostEnvironment)
        {
            productRepository = productRepo;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            var url = HttpContext.Request;

            var products = productRepository.GetAll();
            List<ShowProductDto> datdDto = new List<ShowProductDto>();
            
            foreach (var c in products)
            {
                datdDto.Add(new ShowProductDto() {
                
                     Id =c.Id,
                     Name = c.Name,
                     Description = c.Description,
                     Price = c.Price,
                     Cat_ID = (int)c.Cat_ID,
                     Cat_Name =c.category.Name,
                     ImageSrc = string.Format("{0}://{1}{2}/Images/{3}", url.Scheme, url.Host, url.PathBase , c.ImageName)
                });
            }

                return Ok(datdDto);
        }

        [HttpGet("{id:int}", Name ="ProductDetailsRoute")]
        public IActionResult GetById([FromRoute]int id)
        {
            Product product = productRepository.GetById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProductModel([FromForm] productdetails prod)
        {
            productdetails proddetailsdto = new productdetails();

            try
            {
                string ImageName = await SaveImage(prod.ImageFile);
                Product productfinal = new Product()
                {
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    Cat_ID = prod.Cat_ID,
                    ImageName = ImageName
                };

                productRepository.Insert(productfinal);
                string url = Url.Link("ProductDetailsRoute", new { id = productfinal.Id });
                return Created(url, productfinal);
            }
            catch (Exception e)
            {
              return BadRequest("e");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, [FromForm] Product productModel)
        {

            try
            {
            if (id != productModel.Id)
            {
                return BadRequest();
            }

            if (productModel.ImageFile != null)
            {
                DeleteImage(productModel.ImageName);
                string imgname = await SaveImage(productModel.ImageFile);
                productModel.ImageName = imgname;
            }

                Product oldproduct = productRepository.GetById(id);
                DeleteImage(oldproduct.ImageName);
                productRepository.Update(oldproduct, productModel);
            
            }
            catch (Exception e)
            {
                if (productRepository.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

  
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProductModel(int id)
        {
            Product productModel = productRepository.GetById(id);
            if (productModel == null)
            {
                return NotFound();
            }
            DeleteImage(productModel.ImageName);
            productRepository.Delete(productModel);
            return productModel;
        }









        #region -------------------------------- noActions Methods-------------------------------

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }





        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }





        #endregion






        #region back up


        //[HttpGet("{name:alpha}")]
        //public IActionResult GetByName([FromRoute] string name)
        //{
        //    Product product = productRepository.GetByName(name);
        //    return Ok(product);
        //}




        #endregion











    }
}
