using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    public enum Status2 { In, Out };
    public class Application
    {       
        public Guid Id { get; set; }
        public DateTime TimeOfApplication { get; set; }
        public Status2 Position { get; set; }
        public Guid PatientId { get; set; }
        public Guid OrdinationId { get; set; }
        public Ordination Ordination { get; set; }

    }
}
