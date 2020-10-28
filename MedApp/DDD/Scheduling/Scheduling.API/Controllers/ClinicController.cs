using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Domain.Clinics;

namespace Scheduling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicController(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Clinic>> CreateDoctor(Clinic bill)
        {
            string name = "Nova klinika";
            string location = "Varazdin";
            await _clinicRepository.AddAsync(new Clinic(name, location));
            return Ok(bill);
        }
    }
}
