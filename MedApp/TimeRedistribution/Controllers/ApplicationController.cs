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
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(
            IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetAllDoctors()
        {
            var applications = await _applicationService.GetAll();

            if (applications == null)
            {
                return NotFound();
            }

            return Ok(applications);
        }
    }
}
