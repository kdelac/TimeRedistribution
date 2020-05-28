namespace AppoitmentRedistribution
{
    using AppoitmentRedistribution.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class ScheduleRedistribution
    {
        private TimeScheduelContext _contex;
        private readonly ILogger<Worker> _logger;

        public ScheduleRedistribution(TimeScheduelContext context, ILogger<Worker> logger)
        {
            _contex = context;
            _logger = logger;
        }

        public void Redistribut()
        {
            var doctors = _contex.Doctors.Include(_ => _.Appointments).Where(_ => _.Appointments.Count() > 0).ToList();
            TimeSpan tenMin = new TimeSpan(0, 10, 0);

            doctors.ForEach(_ =>
            {
                var query = _.Appointments.OrderBy(a => a.DateTime.TimeOfDay).Where(b => b.Status == "Waiting" && b.DateTime.Date == DateTime.Now.Date);

                if (Check(query.FirstOrDefault(), tenMin))
                {
                    ChangeAppoitments(query.ToList(), tenMin);
                    _logger.LogInformation("Promjenjeni termini u: {time}", DateTimeOffset.Now);
                }
            });
        }

        public void ChangeAppoitments(List<Appointments> appoitments, TimeSpan a)
        {
            for (int i = 0; i < appoitments.Count(); i++)
            {
                UpdateAppoitment(appoitments[i], a);

                if (i < appoitments.Count() - 1 && CheckGap(appoitments[i], appoitments[i + 1], a))
                {
                    break;
                }

                if (i < appoitments.Count() - 2 && CheckGap(appoitments[i + 1], appoitments[i + 2], a))
                {
                    UpdateAppoitment(appoitments[i + 1], a);
                    break;
                }
            }
        }

        public void UpdateAppoitment(Appointments appointment, TimeSpan appoitmentExtend)
        {
            appointment.DateTime = appointment.DateTime.Date + appointment.DateTime.TimeOfDay + appoitmentExtend;
            _contex.Appointments.Update(appointment);
            _contex.SaveChanges();
        }

        public bool CheckGap(Appointments appoitment, Appointments appoitmentNext, TimeSpan a)
        {
            if (appoitmentNext.DateTime.TimeOfDay - appoitment.DateTime.TimeOfDay > a)
            {
                return true;
            }
            return false;
        }

        public bool Check(Appointments appointment, TimeSpan ts)
        {
            TimeSpan vr = DateTime.Now.TimeOfDay - ts;
            if (vr > appointment.DateTime.TimeOfDay)
            {
                return true;
            }
            return false;
        }

        public Patients FindPatient(Guid id)
        {
            var p = _contex.Patients.Find(id);
            return p;
        }
    }
}
