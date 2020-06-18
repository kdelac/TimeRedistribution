using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorPatientController : ControllerBase
    {
        private readonly IDoctorPatientService _doctorPatientService;

        public DoctorPatientController(IDoctorPatientService doctorPatientService)
        {
            _doctorPatientService = doctorPatientService;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<DoctorPatient>> GetDoctorPatient(Guid id)
        {
            return await _doctorPatientService.GetPatientByDoctorId(id);
        }
    }
}
