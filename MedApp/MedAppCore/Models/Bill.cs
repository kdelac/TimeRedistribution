﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Total { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
