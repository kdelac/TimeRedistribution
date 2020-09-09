using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Models
{
    public class ReciveLogoutMessageEvent
    {
        public string PatientId { get; set; }
        public string OrdinationId { get; set; }
    }
}
