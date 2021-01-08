using CoreProject.Models;
using CoreProject.Models.Services.Weather;
using CoreProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;
        private readonly WeatherService _weatherService;

        public HomeController(ILogger<HomeController> logger, AppDBContext context, WeatherService weatherService)
        {
            _logger = logger;
            _db = context;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {            
            var curWeather = _weatherService.GetCurrentWeather("Аршалы");
            var x = curWeather.Main.Temp;
        
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
