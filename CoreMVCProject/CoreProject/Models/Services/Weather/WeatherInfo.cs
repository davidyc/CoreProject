using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.Services.Weather
{
    public class WeatherInfo
    {
        public string Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }        
        public string Icon { get; set; }
    }
}
