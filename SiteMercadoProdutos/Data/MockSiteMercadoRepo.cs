using System.Collections.Generic;
using SiteMercadoProdutos.Models;

namespace SiteMercadoProdutos.Data
{
    public class MockSiteMercadoRepo : ISiteMercadoRepo
    {
        public void CreateProduct(Product prod)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteProduct(Product prod)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>
            {
                new Product{Id=0,Name="Hambúrguer",Value = 10.50,Image="http://site.com.br/Imagem-hamburguer.jpg"},
                new Product{Id=1,Name="Refrigerante",Value = 8.50,Image="http://site.com.br/Imagem-refri.jpg"},
                new Product{Id=2,Name="Batatas Fritas",Value = 9.90,Image="http://site.com.br/Imagem-batata.jpg"}
            };
            return products;
        }

        public Product GetProductById(int id)
        {
            return new Product{Id=0,Name="Hambúrguer",Value=10.50,Image="http://site.com.br/Imagem.jpg"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProduct(Product prod)
        {
            throw new System.NotImplementedException();
        }
    }
}