﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Models
{
    public class RecivedLoginMessageEvent
    {
        public string PatientId { get; set; }
        public string OrdinationId { get; set; }
    }
}