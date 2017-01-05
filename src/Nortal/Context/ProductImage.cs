using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Nortal.Models.enums;

namespace Nortal.Context
{
    [Table("ProductImages")]
    public class ProductImage
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public ProductImageSizeEnum Size { get; set; }
        public string FileName { get; set; }

        public virtual Product Product { get; set; }
    }
}
