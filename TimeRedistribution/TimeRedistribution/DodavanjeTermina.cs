using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRedistribution.Model;

namespace TimeRedistribution
{
    public class DodavanjeTermina
    {
        private readonly DatabaseContext _context;

        public DodavanjeTermina(DatabaseContext context)
        {
            _context = context;
        }

        public void insertMulti(List<Appointment> appointment)
        {
            appointment.ForEach(_ => {
                var app = _context.Appointments.Where(g => g.DoctorId == _.DoctorId && _.DateTime.Date == g.DateTime.Date);

                Appointment appointmentToSave = new Appointment();
                TimeSpan first = new TimeSpan(8, 0, 0);
                TimeSpan next = new TimeSpan(0, 30, 0);

                app = app.OrderByDescending(_ => _.DateTime.TimeOfDay);
                var u = app.ToList();


                if (app.Count() == 0)
                {
                    appointmentToSave.DateTime = _.DateTime.Date + first;
                    appointmentToSave.DoctorId = _.DoctorId;
                    appointmentToSave.PatientId = _.PatientId;

                    if (appointmentToSave.DateTime.TimeOfDay < DateTime.Now.TimeOfDay)
                    {
                        appointmentToSave.Status = "Completed";
                    }

                    _context.Appointments.Add(appointmentToSave);
                    _context.SaveChanges();
                }
                else
                {
                    appointmentToSave.DateTime = u[0].DateTime.Date + u[0].DateTime.TimeOfDay + next;
                    appointmentToSave.DoctorId = _.DoctorId;
                    appointmentToSave.PatientId = _.PatientId;

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
                    _context.SaveChanges();
                }

            });
        }
    }
}
