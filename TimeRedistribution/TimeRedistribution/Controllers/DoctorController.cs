using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        // GET: api/<DoctorController>
        [HttpGet]
        public IEnumerable<Doctor> Get()
        {            
            return _context.Doctors.ToList();
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorController>
        [HttpPost]
        public void Post(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public void Put(int id, Doctor doctor)
        {            
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
