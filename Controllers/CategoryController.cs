using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Dto;
using Store.Models;
using Store.Repositry;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            categoryRepository = categoryRepo;
        }


        [HttpGet]
        public IActionResult GetCategorys()
        {
            List<Category> categories = categoryRepository.GetAllCategory();
            return Ok(categories);
        }





        [HttpGet("{id:int}")]
        public IActionResult GetCategoryWithProduct(int id)
        {
            Category categores = categoryRepository.GetWithProduct(id);
            CatergoryNameWithProductName CatDto = new CatergoryNameWithProductName();
            CatDto.CatId = categores.Id;
            CatDto.CategoryName = categores.Name;
            foreach (var c in categores.Products)
            {
              CatDto.productDot.Add( new ProductDto() { productName=c.Name,productDescription = c.Description});
            }
            return Ok(CatDto);
        }



    }
}
