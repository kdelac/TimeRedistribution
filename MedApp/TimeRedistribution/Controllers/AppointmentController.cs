using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Mvc;
using a = TimeRedistribution.Resources;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IRescheduleService _rescheduleService;
        private readonly IMapper _mapper;
        private readonly IDodavanjeTermina _dodajTermin;
        private readonly IPatientService _patientService;
        private readonly ILogService _logService;
        //private readonly IAmqService _amqService;
        private readonly string EVENT_NAME_STATUS = "appoitmentStatus";

        public AppointmentController(
            IAppointmentService appointmentService, 
            IMapper mapper,
            IRescheduleService rescheduleService, 
            IDodavanjeTermina dodavanjeTermina, 
            IPatientService patientService,
            ILogService logService
            /*IAmqService amqService*/)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _rescheduleService = rescheduleService;
            _dodajTermin = dodavanjeTermina;
            _patientService = patientService;
            _logService = logService;
            //_amqService = amqService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var apointments = await _appointmentService.GetAll();
            return Ok(apointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);
            return Ok(appointment);
        }

        [HttpGet("Reschedule")]
        public async Task Reschedule()
        {
            await _rescheduleService.Reschedule(5, DateTime.Now, "Waiting");
        }

        [HttpGet("SendMessage")]
        public void SendMessage(string message)
        {
            _rescheduleService.Send(message);
        }

        [HttpPut("{doctorId}/{patientId}/NewUpdate")]
        public void UpdateAppointment(Guid doctorId, Guid patientId, DateTime date)
        {
            _appointmentService.UpdateAppointment(date, doctorId, patientId);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(a.AppointmentResource appointment)
        {
            var appointmentResuource = _mapper.Map<a.AppointmentResource, Appointment>(appointment);
            var result = await _appointmentService.CreateAppointment(appointmentResuource);
            //if (result != null)
            //{
            //    TransactionSetup transactionSetup = new TransactionSetup();
            //    transactionSetup.TransactionStatus = Status.Start;
            //    transactionSetup.EventRaised = true;
            //    transactionSetup.AppoitmentId = result.Id;
            //    await _logService.CreateLog(transactionSetup);
            //    if (!_amqService.SendEvent(transactionSetup, EVENT_NAME_STATUS))
            //    {
            //        transactionSetup.EventRaised = false;
            //        var id = new Guid(result.Id.ToString());
            //        var log = await _logService.GetLogByAppoitmentId(id);
            //        await _logService.UpdateLog(transactionSetup, log);
            //    }               

            //}

            return Ok(result);
        }

        [HttpPut("{doctorId}/{patientId}")]
        public async Task<ActionResult<Appointment>> UpdateAppointment(Guid doctorId, Guid patientId, [FromBody] a.AppointmentResource saveAppointment)
        {
            var appointmentToBeUpdate = await _appointmentService.GetAppointmentForDoctorAndPatient(doctorId, patientId);

            if (appointmentToBeUpdate == null)
                return NotFound();

            var appointmentResuource = _mapper.Map<a.AppointmentResource, Appointment>(saveAppointment);

            await _appointmentService.UpdateAppointment(appointmentToBeUpdate, appointmentResuource);

            var updatedAppointment = await _appointmentService.GetAppointmentForDoctorAndPatient(doctorId, patientId);

            return Ok(updatedAppointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            if (id == null)
                return BadRequest();

            var appointment = await _appointmentService.GetAppointmentById(id);

            if (appointment == null)
                return NotFound();

            await _appointmentService.DeleteAppointment(appointment);

            return NoContent();
        }

        [HttpDelete("{doctorId}/{patientId}")]
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


        /// <summary>
        /// Pomocno, samo u svrhu pregleda podataka
        /// </summary>
        /// <returns></returns>
        [HttpGet("DodajTermine")]
        public async Task DodavanjeTermina(int pocMin, int pocSat, int razmak)
        {
            await _dodajTermin.Dodaj(pocMin, pocSat, razmak);
        }
    }
}
