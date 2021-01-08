using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.Services.Weather
{
    public class MainInfo
    {
        public string Temp { get; set; }
        public string Feels_Like { get; set; }
        public string Temp_Min { get; set; }
        public string Temp_Max { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }

    }
}
