using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAppCore;
using MedAppCore.Client;
using MedAppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IApiCall _apiCall;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IApiCall apiCall)
        {
            _logger = logger;
            _apiCall = apiCall;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<ActionResult> Post(AppointmentResource appointment)
        {
            var result = await _apiCall.Create(appointment, Urls.BaseUrlCreateAppointment, Urls.UrlToBaseAppointment);

            if (result.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
