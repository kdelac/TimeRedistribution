using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Scheduling.Application.Clinics.CreateClinic;
using Scheduling.Application.Clinics.GetAllClinics;
using Scheduling.Application.Contracts;
using Scheduling.Domain.Clinics;

namespace Scheduling.API.Modules.Scheduling.Clinics
{
    [Route("api/[controller]")]
    [ApiController]
    public class CllinicsController : ControllerBase
    {
        private readonly ISchedulingModule _schedulerModule;


        public CllinicsController(ISchedulingModule schedulerModule
             )
        {
            _schedulerModule = schedulerModule;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMeetingGroups()
        {
            var clinics = await _schedulerModule.ExecuteQueryAsync(new GetaAllClinicsQuery());

            return Ok(clinics);
        }
    }
}
