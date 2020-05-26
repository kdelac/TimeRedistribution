using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRedistribution.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DoctorController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Get()
        {
            
            return await _context.Doctors.ToListAsync();            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> Get(Guid id)
        {
            var doctor = _context.Doctors.FirstOrDefault(a => a.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> Post(DoctorFInsert doctor)
        {
            Doctor doc = new Doctor();
            doc.Id = Guid.NewGuid();
            doc.Name = doctor.Name;
            doc.Surname = doctor.Surname;
            doc.OIB = doctor.OIB;
            doc.Specialization = doctor.Specialization;
            _context.Doctors.Add(doc);
            await _context.SaveChangesAsync();
            return doc;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, DoctorForUpdate doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            var doc = await _context.Doctors.FindAsync(id);
            if (doc == null)
            {
                return NotFound();
            }

            doc.Name = doctor.Name;
            doc.Surname = doctor.Surname;
            doc.OIB = doctor.OIB;
            doc.Specialization = doctor.Specialization;
            doc.StartOfWork = doctor.StartOfWork;
            doc.EndOfWorl = doctor.EndOfWorl;
            doc.SrartBreak = doctor.SrartBreak;
            doc.EndBreak = doctor.EndBreak;

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
        public async Task<ActionResult<Doctor>> Delete(Guid id)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(a => a.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }
    }
}
