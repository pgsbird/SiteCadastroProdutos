using System;
using System.Collections.Generic;
using System.Linq;
using SiteMercadoProdutos.Models;

namespace SiteMercadoProdutos.Data
{
    public class SqlSiteMercadoRepo : ISiteMercadoRepo
    {
        private readonly SiteMercadoContext _context;

        public SqlSiteMercadoRepo(SiteMercadoContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product prod)
        {
            if(prod == null)
            {
                throw new ArgumentNullException(nameof(prod));
            }
            _context.Products.Add(prod);
        }


        public void DeleteProduct(Product prod)
        {
            if(prod == null)
            {
                throw new ArgumentNullException(nameof(prod));
            }
            _context.Products.Remove(prod);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0);
        }

        public void UpdateProduct(Product prod)
        {
            if(prod == null)
            {
                throw new ArgumentNullException(nameof(prod));
            }
            _context.Products.Update(prod);
        }
    }
}