using CoreProject.Models.Services.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Services.Interfaces
{
    public interface IWeather
    {
        Weather GetCurrentWeather(string city);
    }

}
