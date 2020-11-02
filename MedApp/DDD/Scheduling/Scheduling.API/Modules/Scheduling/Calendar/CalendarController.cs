using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Calendars.AddNurseToCalendar;
using Scheduling.Application.Calendars.GetAllAppoitmentsForDoctor;
using Scheduling.Application.Contracts;

namespace Scheduling.API.Modules.Scheduling.Calendar
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ISchedulingModule _schedulerModule;


        public CalendarController(ISchedulingModule schedulerModule
             )
        {
            _schedulerModule = schedulerModule;
        }

        [HttpGet("{doctorId}/{nurseId}")]
        public async Task<IActionResult> GetAllMeetingGroups(Guid doctorId, Guid nurseId)
        {
            var clinics = await _schedulerModule.ExecuteQueryAsync(new GetAllAppoitmentsForDoctorQuery(doctorId, nurseId));

            return Ok(clinics);
        }

        [HttpPost]
        public async Task<ActionResult<AddAccesToCalendarRequest>> AddAccesToCalendar(AddAccesToCalendarRequest request)
        {
            await _schedulerModule.ExecuteCommandAsync(
                new AddNurseToCalendarCommand(
                    request.DoctorId,
                    request.NurseId));
            return Ok();
        }
    }
}
