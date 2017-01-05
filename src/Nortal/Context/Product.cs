using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nortal.Context
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        public ProductSpecifications ProductSpecifications { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
