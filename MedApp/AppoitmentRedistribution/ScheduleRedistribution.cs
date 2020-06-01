using System;
using System.Collections.Generic;
using System.Text;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.Extensions.Logging;

namespace AppoitmentRedistribution
{
    public class ScheduleRedistribution
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly ILogger<Worker> _logger;

        public ScheduleRedistribution()
        {
        }

        public ScheduleRedistribution(IAppointmentService appointmentService, ILogger<Worker> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        public void Redistribut()
        {
            Console.WriteLine("Holi shit");
        }
    }
}
