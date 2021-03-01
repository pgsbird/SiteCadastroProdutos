using System.Collections.Generic;
using SiteMercadoProdutos.Models;

namespace SiteMercadoProdutos.Data
{
    public interface ISiteMercadoRepo
    {

        bool SaveChanges();
        
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        void CreateProduct(Product prod);

        void UpdateProduct(Product prod);

        void DeleteProduct(Product prod);
    }
}