using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Repositry
{
    public class CategoryRepository:ICategoryRepository
    {
        ITIAPI context;
        public CategoryRepository(ITIAPI db)
        {
            context = db;
        }


        public List<Category> GetAllCategory()
        {
            List<Category> categories = context.Categorys.ToList();
            return categories;
        }


        public Category GetWithProduct(int id)
        {
            Category categors = context.Categorys.Include(d => d.Products).FirstOrDefault(c => c.Id == id);
            return categors;
        }

    }
}
