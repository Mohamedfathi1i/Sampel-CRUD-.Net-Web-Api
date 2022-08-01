using Store.Models;

namespace Store.Repositry
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategory();
        public Category GetWithProduct(int id);

    }
}
