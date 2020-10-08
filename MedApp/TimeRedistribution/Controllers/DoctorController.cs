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
        public async Task<ActionResult<Doctor>> CreateDoctor(DoctorResource doctorResource)
        {
            var doctor = _mapper.Map<DoctorResource, Doctor>(doctorResource);
            await _doctorService.CreateDoctor(doctor);

            return Ok(doctor);
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

        #region mongo

        [HttpGet("Mongo")]
        public ActionResult<IEnumerable<Users>> GetAllDoctorsMongo()
        {
            var doctors = _doctorService.GetAllMongo();

            if (doctors == null)
            {
                return NotFound();
            }

            return Ok(doctors);
        }

        [HttpPost("Mongo")]
        public async Task<ActionResult<Users>> CreateDoctorMongo(UsersResource doctorResource)
        {
            var doctor = _mapper.Map<UsersResource, Users>(doctorResource);
            await _doctorService.CreateDoctorMongo(doctor);

            return Ok(doctor);
        }

        [HttpPost("AddMultipleMongo")]
        public async Task CreateMultipleDoctorsMongo(IEnumerable<UsersResource> doctorsResources)
        {
            var doctors = _mapper.Map<IEnumerable<UsersResource>, IEnumerable<Users>>(doctorsResources);
            _doctorService.AddRangeAsyncMongo(doctors);
        }

        [HttpGet("Mongo/{id}")]
        public async Task<ActionResult<Users>> GetDoctorMongo(Guid id)
        {
            var patient = await _doctorService.GetDoctorByIdMongo(id);
            return Ok(patient);
        }

        #endregion
    }
}
