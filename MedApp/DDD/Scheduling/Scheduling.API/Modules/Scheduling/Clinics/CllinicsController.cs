using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Clinics.CreateClinic;
using Scheduling.Application.Contracts;

namespace Scheduling.API.Modules.Scheduling.Clinics
{
    [Route("api/[controller]")]
    [ApiController]
    public class CllinicsController : ControllerBase
    {
        private readonly ISchedulingModule _schedulerModule;

        public CllinicsController(ISchedulingModule meetingsModule)
        {
            _schedulerModule = meetingsModule;
        }

        [HttpPost]
        public async Task<ActionResult<CreateClinicRequest>> CreateClinic(CreateClinicRequest request)
        {
            await _schedulerModule.ExecuteCommandAsync(
                new CreateClinicCommand(
                    request.Name,
                    request.Location));

            return Ok();
        }
    }
}
