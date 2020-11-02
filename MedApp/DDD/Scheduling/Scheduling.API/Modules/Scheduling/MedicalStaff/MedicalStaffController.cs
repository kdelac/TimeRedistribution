using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Contracts;
using Scheduling.Application.MedicalStaff.CreateMedicalStaff;
using Scheduling.Application.MedicalStaff.CreateMedicalStaff.CreateNurse;

namespace Scheduling.API.Modules.Scheduling.MedicalStaff
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalStaffController : ControllerBase
    {
        private readonly ISchedulingModule _schedulerModule;

        public MedicalStaffController(ISchedulingModule schedulerModule)
        {
            _schedulerModule = schedulerModule;
        }

        [HttpPost("doctor")]
        public async Task<ActionResult<CreateDoctorRequest>> CreateDoctor(CreateDoctorRequest request)
        {
            await _schedulerModule.ExecuteCommandAsync(
                new CreateDoctorCommand(
                    request.ClinicId,
                    request.Firstname,
                    request.Lastname,
                    request.DateOfBirth,
                    request.NursesIds));
            return Ok();
        }

        [HttpPost("nurse")]
        public async Task<ActionResult<CreateNurseRequest>> CreateNurse(CreateNurseRequest request)
        {
            await _schedulerModule.ExecuteCommandAsync(
                new CreateNurseCommand(
                    request.ClinicId,
                    request.Firstname,
                    request.Lastname,
                    request.DateOfBirth));
            return Ok();
        }


    }
}
