using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BatchProcessor.ManagerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly Random _random = new Random();

        [HttpGet("a")]
        public IEnumerable<int> GetStreamOfData()
        {
            foreach(var i in Enumerable.Range(0,1000).Select(x => x))
            {
                yield return _random.Next(0, 100);
                Thread.Sleep(500);
            }
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var url = "https://localhost:5001/WeatherForecast/a";

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                using (var response = await client.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead))
                {
                    using (var body = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(body))
                        while (!reader.EndOfStream)
                            Console.WriteLine(reader.ReadLine());
                }
            }


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
