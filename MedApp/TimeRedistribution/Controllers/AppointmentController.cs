﻿using System;
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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var apointments = await _appointmentService.GetAllWithPatientsAndDoctorAsync();
            return Ok(apointments);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(AppointmentResource appointment)
        {
            var appointmentResuource = _mapper.Map<AppointmentResource, Appointment>(appointment);
            await _appointmentService.CreateAppointment(appointmentResuource);

            return Ok(appointmentResuource);
        }

        [HttpPut("{doctorId}/{patientId}")]
        public async Task<ActionResult<Appointment>> UpdateAppointment(Guid doctorId, Guid patientId, [FromBody] AppointmentResource saveAppointment)
        {
            var appointmentToBeUpdate = await _appointmentService.GetAppointmentForDoctorAndPatient(doctorId, patientId);

            if (appointmentToBeUpdate == null)
                return NotFound();

            var appointmentResuource = _mapper.Map<AppointmentResource, Appointment>(saveAppointment);

            await _appointmentService.UpdateAppointment(appointmentToBeUpdate, appointmentResuource);

            var updatedAppointment = await _appointmentService.GetAppointmentForDoctorAndPatient(doctorId, patientId);

            return Ok(updatedAppointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid doctorId, Guid patientId)
        {
            if (doctorId == null && patientId == null)
                return BadRequest();

            var appointment = await _appointmentService.GetAppointmentForDoctorAndPatient(doctorId, patientId);

            if (appointment == null)
                return NotFound();

            await _appointmentService.DeleteAppointment(appointment);

            return NoContent();
        }
    }
}
