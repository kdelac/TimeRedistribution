using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeRedistribution.Resources;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllWithAppointment();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(Guid id)
        {
            var patient = await _patientService.GetPatientById(id);
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(PatientResource patientResource)
        {
            var patient = _mapper.Map<PatientResource, Patient>(patientResource);
            await _patientService.CreatePatient(patient);
            return Ok(patient);
        }        

        [HttpPost("AddMultiple")]
        public async Task CreateMultiplePatients(IEnumerable<PatientResource> patients)
        {
            var patientResource = _mapper.Map<IEnumerable<PatientResource>, IEnumerable<Patient>>(patients);
            await _patientService.AddRangeAsync(patientResource);
        }

        [HttpPut("{id}/Update")]
        public async Task<ActionResult<Patient>> UpdatePatient(Guid id, [FromBody] PatientResource savePatient)
        {
            var patientToBeUpdate = await _patientService.GetPatientById(id);

            if (patientToBeUpdate == null)
                return NotFound();

            var patientResuource = _mapper.Map<PatientResource, Patient>(savePatient);

            await _patientService.UpdatePatient(patientToBeUpdate, patientResuource);

            var updatedPatient = await _patientService.GetPatientById(id);

            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            if (id == null)
                return BadRequest();

            var patient = await _patientService.GetPatientById(id);

            if (patient == null)
                return NotFound();

            await _patientService.DeletePatient(patient);

            return NoContent();
        }

        #region Mongo

        [HttpPost("Mongo")]
        public async Task<ActionResult<Users>> CreatePatientMongo(UsersResource patientResource)
        {
            var patient = _mapper.Map<UsersResource, Users>(patientResource);
            await _patientService.CreatePatientMongo(patient);

            return Ok(patient);
        }

        [HttpGet("Mongo")]
        public ActionResult<IEnumerable<Users>> GetAllPatientsMongo()
        {
            var pat = _patientService.GetAllMongo();
            return Ok(pat);
        }

        [HttpPost("AddMultipleMongo")]
        public async Task CreateMultipleDoctorsMongo(IEnumerable<UsersResource> patientResources)
        {
            var patients = _mapper.Map<IEnumerable<UsersResource>, IEnumerable<Users>>(patientResources);
            _patientService.AddRangeAsyncMongo(patients);
        }

        [HttpGet("Mongo/{id}")]
        public async Task<ActionResult<Users>> GetPatientMongo(Guid id)
        {
            var patient = await _patientService.GetPatientByIdMongo(id);
            return Ok(patient);
        }
        #endregion
    }
}
