using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nortal.Models
{
    public class ProductListFilterModel
    {
        public List<string> Manufacturers { get; set; }
        public List<int> Storages { get; set; }
        public List<string> Os { get; set; }
        public List<double> Cameras { get; set; }
    }
}
