using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        private readonly ILogger<WeatherForecastController> _logger;

        private List<WeatherForecast> WFC;
        string path = "WFC.xml";

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public string Read()
        {
            if (System.IO.File.Exists(path)) return System.IO.File.ReadAllText(path); else return $"Nothing to read.";
        }

        [HttpGet("read")]
        public WeatherForecast Get([FromQuery] DateTime date)
        {
            if (System.IO.File.Exists(path))
            {
                WFC = Deserialize(path);

                for (int i = 0; i < WFC.Count; i++)
                {
                    if (WFC[i].Date == date) return WFC[i];
                }
            }
            return null;
        }

        public WeatherForecast Get([FromQuery] int counter)
        {
            if (System.IO.File.Exists(path)) return Deserialize(path)[counter]; else return null;
        }

        [HttpGet("range")]
        public List<WeatherForecast> Get([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (System.IO.File.Exists(path))
            {
                List<WeatherForecast> temp = new List<WeatherForecast>();
                WFC = Deserialize(path);
                for (int i = 0; i < WFC.Count; i++)
                {
                    if ((WFC[i].Date >= startDate) & (WFC[i].Date <= endDate))
                    {
                        temp.Add(WFC[i]);
                    }
                }
                return temp;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public string Create([FromQuery] WeatherForecast wfc)
        {

            if (System.IO.File.Exists(path)) WFC = Deserialize(path);
            WFC.Add(wfc);
            System.IO.File.WriteAllText(path, JsonSerializer.Serialize(wfc));
            return $"Saving {wfc.Date} {wfc.TemperatureC} {wfc.TemperatureF} to File {path}";
        }

        [HttpPut]
        public string Replace([FromQuery] DateTime date, [FromQuery] int TempC)
        {
            WFC = Deserialize(path);
            for (int i = 0; i < WFC.Count; i++)
            {
                if (WFC[i].Date == date)
                {
                    string temp = WFC[i].TemperatureC.ToString();
                    WFC[i].TemperatureC = TempC;
                    FileStream fs = new FileStream(path, FileMode.Open);
                    XmlSerializer srl = new XmlSerializer(typeof(List<WeatherForecast>));
                    srl.Serialize(fs, WFC);
                    return $"Was {temp}, Now {WFC[i].TemperatureC.ToString()}";
                }
            }
            return "Error";
        }

        [HttpDelete]
        public string Del([FromQuery] DateTime date)
        {
            if (System.IO.File.Exists(path)) WFC = Deserialize(path);

            for (int i = 0; i < WFC.Count; i++)
            {
                if (WFC[i].Date == date)
                {
                    WFC.RemoveAt(i);
                    return "Deleted";
                }
            }
            return "Nothing to delete";
        }

        private List<WeatherForecast> Deserialize(string path)
        {
            string json = System.IO.File.ReadAllText(path);
            List<WeatherForecast> lst = JsonSerializer.Deserialize<List<WeatherForecast>>(json);

            return lst;
        }
    }
}
