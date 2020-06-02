using System;
using System.Collections.Generic;
using System.Text;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.Extensions.Logging;

namespace AppoitmentRedistribution
{
    public class ScheduleRedistribution : IScheduleRedistribution
    {
        public ScheduleRedistribution()
        {
            
        }

        public void Redistribut()
        {
            Console.WriteLine("aaaaaa");
        }
    }
}
