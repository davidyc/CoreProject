using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.Services.Weather
{
    public class Weather
    {
        public Coord Coord { get; set; }
        public WeatherInfo[] weather { get; set; }
        public string Base { get; set; }
        public MainInfo Main { get; set; }
        public string Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public string DT { get; set; }
        public SystemInfo Sys { get; set; }
        public string TimeZone { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cod { get; set; }


    }

}
