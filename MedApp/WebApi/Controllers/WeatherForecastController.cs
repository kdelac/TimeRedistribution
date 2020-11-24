using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcAppointment;
using MedAppCore;
using MedAppCore.Client;
using MedAppCore.Models;
using MedAppCore.Services;
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
        private readonly IAmqService _amqService;
        private readonly string SEND_TOPIC = "VirtualTopic.Message";

        public WeatherForecastController(IApiCall apiCall,
            IAmqService amqService)
        {
            _apiCall = apiCall;
            _amqService = amqService;
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

        [HttpPost("Posalji")]
        public async Task<ActionResult> Message(string poruka)
        {
            return !_amqService.SendEventTopic(poruka, SEND_TOPIC) ? (ActionResult)BadRequest() : Ok();
        }
    }
}
