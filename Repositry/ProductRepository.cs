using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Dto;
using Store.Models;
using System.Collections.Generic;

namespace Store.Repositry
{
    public class ProductRepository: IProductRepository
    {

        ITIAPI context;
        public ProductRepository(ITIAPI db)
        {
            context = db;
        }

        public List<Product> GetAll()
        {
            List<Product> products = context.Products.Include(p=>p.category).ToList();
            return products;
        }

        public Product GetById(int id)
        {
            Product product =context.Products.FirstOrDefault(product => product.Id == id);
            return product;
        }

        public Product GetByName(string name)
        {
            Product product = context.Products.FirstOrDefault(product => product.Name == name);
            return product;
        }

        public void Insert(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update(Product oldproduct, Product product)
        {
            oldproduct.Name = product.Name;
            oldproduct.Description = product.Description;
            oldproduct.Price = product.Price;
            oldproduct.ImageName = product.ImageName;
            oldproduct.Cat_ID = product.Cat_ID;
            context.SaveChanges();
        }
        
        public void Delete(Product product)
        {
            context.Remove(product);
            context.SaveChanges();
        }

        public string NameFile(string file)
        {
            var _DirectoryPath = Path.Combine("Images");

            string extension = Path.GetExtension(file);
            string fileName = file.Remove(file.Length - extension.Length);

            string result = file;

            //int count = 1;
            //while (File.Exists(Path.Combine(_DirectoryPath, result)))
            //{
            //    result = fileName + $"({count})" + extension;
            //    count++;
            //}
            return result;
        }





    }
}
