using CoreProject.Models.Services.Weather;
using CoreProject.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Services
{
    public class WeatherService : IWeather
    {
        private readonly string _url;
        private readonly string _apiid;
        private readonly string _units;
        private readonly string _lang;
        public WeatherService()
        {
            _units = "metric";
            _lang = "ru";
            _url = "https://api.openweathermap.org/data/2.5/weather";
            _apiid = "1c0fce73161e75da30ec9fcabf2a1b9c";
        }
        public WeatherService(string url, string apiid, string units, string lang)
        {
            _units = units;
            _lang = lang;
            _url = url;
            _apiid = apiid;
        }

        public Weather GetCurrentWeather(string city)
        {
            var client = new RestClient($"{_url}?q={city}&units={_units}&appid={_apiid}&lang={_lang}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var jsonString = response.Content;

            return JsonConvert.DeserializeObject<Weather>(jsonString);
            
        }
    }
}
