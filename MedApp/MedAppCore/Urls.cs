﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore
{
    public static class Urls
    {
        public static string BaseUrlBilling { get; set; } = "https://localhost:44315/";
        public static string BaseUrlCreateAppointment { get; set; } = "https://localhost:44308/"; 
        public static string UrlToCreateBill { get; set; } = "/api/Appointment"; 
        public static string UrlToCreateAppointment { get; set; } = "/api/Appointment"; 

    }
}
