namespace Nortal.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageSmall { get; set; }
        public string ImageLarge { get; set; }
        public string Manufacturer { get; set; }
        public int? Storage { get; set; }
        public string OperatingSystem { get; set; }
        public double? CameraMpix { get; set; }
    }
}
