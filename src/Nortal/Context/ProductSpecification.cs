using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Nortal.Context
{
    public class ProductSpecifications
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public int Storage { get; set; }
        public string OperatingSystem { get; set; }
        public double CameraMpix { get; set; }

        [ForeignKey("Product")]
        [JsonIgnore]
        public int ProductId { get; set; }
        public virtual Product Product{ get; set; }
    }
}
