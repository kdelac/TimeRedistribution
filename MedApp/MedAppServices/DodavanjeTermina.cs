﻿using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    /// <summary>
    /// Pomocno, samo u svrhu pregleda podataka
    /// </summary>
    /// <returns></returns>
    public class DodavanjeTermina : IDodavanjeTermina
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public DodavanjeTermina(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
        }

        public async Task Dodaj(int pocMin, int pocSat, int razmak)
        {
            var a = await _doctorService.GetAllWithAppointment();
            var b = await _patientService.GetAllWithAppointment();
            var doctors = a.ToList();
            var patients = b.ToList();
            doctors.ForEach(async _ =>
            {
                List<Appointment> app = new List<Appointment>();
                patients.ForEach(d =>
                {
                    Appointment www = new Appointment();
                    www.Id = Guid.NewGuid();
                    www.DoctorId = _.Id;
                    www.PatientId = d.Id;
                    www.DateTime = DateTime.Now;
                    app.Add(www);
                });
                Provjera(app, pocMin, pocSat, razmak);
            });
        }

        public void Provjera(List<Appointment> appointments, int pocMin, int pocSat, int razmak)
        {
            List<Appointment> appointments1 = new List<Appointment>();
            TimeSpan brojac = new TimeSpan(0, 0, 0);

            appointments.ForEach(async _ => {

                Appointment appointmentToSave = new Appointment();
                TimeSpan first = new TimeSpan(pocSat, pocMin, 0);
                TimeSpan next = new TimeSpan(0,razmak, 0);      

                if (appointments1.Count() == 0)
                {
                    appointmentToSave.DateTime = _.DateTime.Date + first;
                    appointmentToSave.DoctorId = _.DoctorId;
                    appointmentToSave.PatientId = _.PatientId;
                    appointmentToSave.Id = _.Id;

                        appointmentToSave.Status = "Waiting";

                    appointments1.Add(appointmentToSave);
                }
                else
                {
                    brojac += next;
                    appointmentToSave.DateTime = appointments1[0].DateTime.Date + appointments1[0].DateTime.TimeOfDay + brojac;
                    appointmentToSave.DoctorId = _.DoctorId;
                    appointmentToSave.PatientId = _.PatientId;
                    appointmentToSave.Id = _.Id;

                        appointmentToSave.Status = "Waiting";


                    appointments1.Add(appointmentToSave);
                }

            });
             _appointmentService.AddRangeAsync(appointments1);
            Task.Delay(1000);
        }
    }
}
