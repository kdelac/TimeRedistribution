using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Mvc;
using TimeRedistribution.Resources;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllWithAppointment();
            return Ok(doctors);
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(DoctorResource doctor)
        {
            var doctorResuource = _mapper.Map<DoctorResource, Doctor>(doctor);
            await _doctorService.CreateDoctor(doctorResuource);
            
            return Ok(doctorResuource);
        }

        [HttpPut("{id}")]
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
            if (id == null)
                return BadRequest();

            var doctor = await _doctorService.GetDoctorById(id);

            if (doctor == null)
                return NotFound();

            await _doctorService.DeleteDoctor(doctor);

            return NoContent();
        }
    }
}
