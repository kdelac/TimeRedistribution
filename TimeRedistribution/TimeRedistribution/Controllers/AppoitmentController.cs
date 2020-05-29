﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRedistribution.Model;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppoitmentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private DodavanjeTermina dodavanje;

        public AppoitmentController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get()
        {
            return await _context.Appointments.ToListAsync();
        }

        [HttpGet("{doctorId}/Doctor")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsForDoctor(Guid doctorId)
        {
            var appoitments = await _context.Appointments.Where(a => a.DoctorId == doctorId).ToListAsync();

            if (appoitments == null)
            {
                return NotFound();
            }

            return appoitments;
        }

        [HttpGet("DodajTermine")]
        public async Task<ActionResult<IEnumerable<Appointment>>> DodavanjeTermina()
        {
            var a = await _context.Doctors.ToListAsync();
            var b = await _context.Patients.ToListAsync();
            dodavanje = new DodavanjeTermina(_context);
            a.ForEach(_ =>
            {
                List<Appointment> app = new List<Appointment>();
                b.ForEach(d =>
                {
                    Appointment www = new Appointment();
                    www.DoctorId = _.Id;
                    www.PatientId = d.Id;
                    www.DateTime = DateTime.Parse("2020-05-28T10:06:29.928Z");
                    app.Add(www);
                });
                dodavanje.insertMulti(app);
            });
            return null;
        }

        [HttpGet("{patientId}/Patient")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsForPatient(Guid patientId)
        {
            var appoitments =  _context.Appointments.Where(a => a.PatientId == patientId);

            if (appoitments == null)
            {
                return NotFound();
            }

            return await appoitments.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Post(AppointmentInsert appointment)
        {
            var app = _context.Appointments.Where(_ => _.DoctorId == appointment.DoctorId && _.DateTime.Date == appointment.DateTime.Date);

            Appointment appointmentToSave = new Appointment();
            TimeSpan first = new TimeSpan(8, 0, 0);
            TimeSpan next = new TimeSpan(0, 30, 0);

            app = app.OrderByDescending(_ => _.DateTime.TimeOfDay);
            var a = app.FirstOrDefault();


            if (app.Count() == 0)
            {
                appointmentToSave.DateTime = appointment.DateTime.Date + first;
                appointmentToSave.DoctorId = appointment.DoctorId;
                appointmentToSave.PatientId = appointment.PatientId;

                if (appointmentToSave.DateTime.TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    appointmentToSave.Status = "Completed";
                }

                _context.Appointments.Add(appointmentToSave);
                await _context.SaveChangesAsync();
                return appointmentToSave;
            }

            appointmentToSave.DateTime = a.DateTime.Date + a.DateTime.TimeOfDay + next;
            appointmentToSave.DoctorId = appointment.DoctorId;
            appointmentToSave.PatientId = appointment.PatientId;

            if (appointmentToSave.DateTime.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                appointmentToSave.Status = "Completed";
            }

            if (appointmentToSave.DateTime.TimeOfDay == DateTime.Now.TimeOfDay)
            {
                appointmentToSave.Status = "Pending";
            }

            if (appointmentToSave.DateTime.TimeOfDay > DateTime.Now.TimeOfDay)
            {
                appointmentToSave.Status = "Waiting";
            }

            _context.Appointments.Add(appointmentToSave);
            await _context.SaveChangesAsync();
            return appointmentToSave;
        }        

        [HttpPut("{doctorId}/{patientId}")]
        public async Task<IActionResult> Put(Guid doctorId,Guid patientId, AppointmentInsert appointment)
        {
            if (doctorId != appointment.DoctorId)
            {
                return BadRequest();
            }

            var app = await _context.Appointments.FirstAsync(_ => _.DoctorId == doctorId && _.PatientId == patientId);
            if (app == null)
            {
                return NotFound();
            }

            app.PatientId = appointment.PatientId;
            app.DoctorId = appointment.DoctorId;
            app.DateTime = appointment.DateTime;


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

        [HttpDelete("{doctorId}/{patientId}/Delete")]
        public async Task<ActionResult<Appointment>> Delete(Guid doctorId, Guid patientId)
        {
            Appointment appoitment = _context.Appointments.FirstOrDefault(_ => _.DoctorId == doctorId && _.PatientId == patientId);
            if (appoitment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appoitment);
            await _context.SaveChangesAsync();

            return appoitment;
        }        
    }
}
