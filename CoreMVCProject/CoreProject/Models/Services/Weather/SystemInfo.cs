using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.Services.Weather
{
    public class SystemInfo
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Country { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
}
