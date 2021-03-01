using Microsoft.EntityFrameworkCore;
using SiteMercadoProdutos.Models;

namespace SiteMercadoProdutos.Data
{
    public class SiteMercadoContext : DbContext
    {
        public SiteMercadoContext(DbContextOptions<SiteMercadoContext> opt):base(opt)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        
        
    }
}