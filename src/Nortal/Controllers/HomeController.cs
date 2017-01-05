using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nortal.ExtensionMethods;
using Nortal.Models;
using Nortal.Models.enums;
using Nortal.Services;
using Nortal.ViewModels;

namespace Nortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductsService _service;
        private readonly AppSettings _appSettings;

        public HomeController(ProductsService service, IOptions<AppSettings> appSettings)
        {
            _service = service;
            _appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList(ProductListFilterModel filterModel)
        {
            var products = _service.GetProducts(filterModel);

            var vm = products.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                OperatingSystem = product.ProductSpecifications?.OperatingSystem,
                Name = product.Name,
                Storage = product.ProductSpecifications?.Storage,
                CameraMpix = product.ProductSpecifications?.CameraMpix,
                Price = product.Price,
                Manufacturer = product.ProductSpecifications?.Manufacturer,
                ImageLarge = product.ProductImages.GetImageFileName(ProductImageSizeEnum.Large, _appSettings.ImagesPath),
                ImageSmall = product.ProductImages.GetImageFileName(ProductImageSizeEnum.Small, _appSettings.ImagesPath)
            }).ToList();

            return PartialView("_ProductList", vm);
        }

        public IActionResult ProductDetails(int productId)
        {
            var product = _service.GetProductDetails(productId);

            if (product == null)
            {
                return NotFound();
            }

            var vm = new ProductViewModel()
            {
                Id = product.Id,
                OperatingSystem = product.ProductSpecifications?.OperatingSystem,
                Name = product.Name,
                Storage = product.ProductSpecifications?.Storage,
                CameraMpix = product.ProductSpecifications?.CameraMpix,
                Price = product.Price,
                Manufacturer = product.ProductSpecifications?.Manufacturer,
                ImageLarge = product.ProductImages.GetImageFileName(ProductImageSizeEnum.Large, _appSettings.ImagesPath),
                ImageSmall = product.ProductImages.GetImageFileName(ProductImageSizeEnum.Small, _appSettings.ImagesPath)
            };

            return PartialView("_ProductDetails", vm);
        }
    }
}
