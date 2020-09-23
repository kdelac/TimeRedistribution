using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcAppointment;
using MedAppCore;
using MedAppCore.Client;
using MedAppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit.Encodings;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IApiCall _apiCall;

        public WeatherForecastController(IApiCall apiCall)
        {
            _apiCall = apiCall;
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
