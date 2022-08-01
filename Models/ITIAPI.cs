using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class ITIAPI:DbContext
    {
        public ITIAPI()
        {

        }
        public ITIAPI(DbContextOptions options):base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ITIAPI;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
