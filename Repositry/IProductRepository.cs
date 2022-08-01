using Store.Dto;
using Store.Models;

namespace Store.Repositry
{
    public interface IProductRepository
    {

        public List<Product> GetAll();
        public Product GetById(int id);
        public Product GetByName(string name);
        public void Insert(Product product);
        public void Update(Product oldproduct, Product product);
        public void Delete(Product product);


    }
}
