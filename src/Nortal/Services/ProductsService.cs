using System.Collections.Generic;
using System.Linq;
using Nortal.Context;
using Microsoft.EntityFrameworkCore;
using Nortal.Models;

namespace Nortal.Services
{
    public class ProductsService
    {
        private readonly ApplicationDbContext _context;

        public ProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product GetProductDetails(int productId)
        {
            return _context.Products.Include(x => x.ProductSpecifications).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == productId);
        }

        public List<Product> GetProducts(ProductListFilterModel filterModel = null)
        {
            IQueryable<Product> products = _context.Products.Include(x => x.ProductSpecifications).Include(x => x.ProductImages);

            if (filterModel != null)
            {
                if (filterModel.Manufacturers != null && filterModel.Manufacturers.Any())
                {
                    products = products.Where(x => filterModel.Manufacturers.Contains(x.ProductSpecifications.Manufacturer));
                }
                if (filterModel.Os != null && filterModel.Os.Any())
                {
                    products = products.Where(x => filterModel.Os.Contains(x.ProductSpecifications.OperatingSystem));
                }
                if (filterModel.Storages != null && filterModel.Storages.Any())
                {
                    products = products.Where(x => filterModel.Storages.Contains(x.ProductSpecifications.Storage));
                }
                if (filterModel.Cameras != null && filterModel.Cameras.Any())
                {
                    products = products.Where(x => filterModel.Cameras.Contains(x.ProductSpecifications.CameraMpix));
                }
            }

            return products.ToList();
        }
    }
}
