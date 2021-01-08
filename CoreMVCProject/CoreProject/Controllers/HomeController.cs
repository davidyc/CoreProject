using CoreProject.Models;
using CoreProject.Models.Services.Weather;
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

        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/weather?q=Аршалы&units=metric&appid=1c0fce73161e75da30ec9fcabf2a1b9c&lang=ru");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var jsonString = response.Content;

            var weather = JsonConvert.DeserializeObject<Weather>(jsonString);
            var x = weather.Main.Temp;
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
