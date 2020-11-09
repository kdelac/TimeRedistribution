using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Appoitments.CreateAppoitment;
using Scheduling.Application.Contracts;

namespace Scheduling.API.Modules.Scheduling.Appoitment
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ISchedulingModule _schedulerModule;


        public AppointmentController(ISchedulingModule schedulerModule
             )
        {
            _schedulerModule = schedulerModule;
        }        

        [HttpPost]
        public async Task<ActionResult<AddAppoitmentRequest>> AddAppoitment(AddAppoitmentRequest request)
        {
            await _schedulerModule.ExecuteCommandAsync(
                new CreateAppointmentCommand(
                    request.CalendarId,
                    request.PatientId,
                    request.StartAppoitment,
                    request.EndAppoitment));
            return Ok();
        }
    }
}
