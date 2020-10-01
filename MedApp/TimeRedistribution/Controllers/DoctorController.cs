using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Timeout;
using TimeRedistribution.Resources;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        //private readonly IAsyncPolicy _policy;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            //_policy = asyncPolicy;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllWithAppointment();

            if (doctors == null)
            {
                return NotFound();
            }

            return Ok(doctors);
        }

        //[HttpGet("DoctorsPolly")]
        //public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctorsPolly()
        //{
        //    IEnumerable<Doctor> doctors = null;

        //    try
        //    {
        //        _ = _policy.ExecuteAsync(async () =>

        //        {
        //            doctors = await _doctorService.GetAllWithAppointment();
        //        });

                
        //        return Ok(doctors);
        //    }
        //    catch (TimeoutException ex)
        //    {
        //        return NotFound();
        //    }
            

        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(Guid id)
        {
            var doctor = await _doctorService.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(DoctorResource doctor)
        {
            var doctorResuource = _mapper.Map<DoctorResource, Doctor>(doctor);
            await _doctorService.CreateDoctor(doctorResuource);

            return Ok(doctorResuource);
        }

        [HttpPost("AddMultiple")]
        public async Task CreateMultipleDoctors(IEnumerable<DoctorResource> doctors)
        {
            var patientResources = _mapper.Map<IEnumerable<DoctorResource>, IEnumerable<Doctor>>(doctors);
            await _doctorService.AddRangeAsync(patientResources);
        }

        [HttpPut("{id}/Update")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(Guid id, [FromBody] DoctorResource saveDoctor)
        {
            var doctorToBeUpdate = await _doctorService.GetDoctorById(id);

            if (doctorToBeUpdate == null)
                return NotFound();

            var doctorResuource = _mapper.Map<DoctorResource, Doctor>(saveDoctor);

            await _doctorService.UpdateDoctor(doctorToBeUpdate, doctorResuource);

            var updatedDoctor = await _doctorService.GetDoctorById(id);

            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _doctorService.GetDoctorById(id);

            if (doctor == null)
                return NotFound();

            await _doctorService.DeleteDoctor(doctor);

            return NoContent();
        }
    }
}
