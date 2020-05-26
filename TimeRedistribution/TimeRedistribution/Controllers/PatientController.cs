using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRedistribution.Model;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PatientController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get()
        {
            return await _context.Patients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> Post(PatientForInsert patient)
        {
            Patient pat = new Patient();
            pat.Id = Guid.NewGuid();
            pat.Name = patient.Name;
            pat.Surname = patient.Surname;
            pat.Email = patient.Email;
            _context.Patients.Add(pat);
            await _context.SaveChangesAsync();
            return pat;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PatientForUpdate patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            var pat = await _context.Patients.FindAsync(id);
            if (pat == null)
            {
                return NotFound();
            }

            pat.Name = patient.Name;
            pat.Surname = patient.Surname;
            pat.Email = patient.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> Delete(Guid id)
        {
            Patient patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }
    }
}
