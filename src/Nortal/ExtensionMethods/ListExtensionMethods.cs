using System;
using System.Collections.Generic;
using System.Linq;
using Nortal.Context;
using Nortal.Models.enums;

namespace Nortal.ExtensionMethods
{
    public static class ListExtensionMethods
    {
        public static string GetImageFileName(this ICollection<ProductImage> productProductImages, ProductImageSizeEnum size, string imagesPath)
        {
            var image = productProductImages.FirstOrDefault(x => x.Size == size);

            return image == null ? string.Concat(imagesPath,GetDefaultImage(size)) : string.Concat(imagesPath,image.FileName);
        }

        private static string GetDefaultImage(ProductImageSizeEnum type)
        {
            switch (type)
            {
                case ProductImageSizeEnum.Large:
                    return "product-default-large.jpg";
                case ProductImageSizeEnum.Small:
                    return "product-default.jpg";
                default:
                    return "product-default.jpg";
            }
        }
    }
}
