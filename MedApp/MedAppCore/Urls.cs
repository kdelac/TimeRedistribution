using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore
{
    public static class Urls
    {
        public static string BaseUrlBilling { get; set; } = "https://localhost:44315/";
        public static string BaseUrlCreateAppointment { get; set; } = "https://localhost:44308/"; 
        public static string UrlToCreateBill { get; set; } = "/api/Bill"; 
        public static string UrlToBaseAppointment { get; set; } = "/api/Appointment"; 
        public static string ActiveMQ { get; set; } = "tcp://activemq:61616"; 
        public static string ActiveMQLoc { get; set; } = "tcp://localhost:8888"; 
        

        public static string gRPCAppoitment { get; set; } = "https://localhost:5654";

    }
}
