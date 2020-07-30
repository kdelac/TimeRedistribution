using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IRescheduleService
    {
        Task Reschedule(int deleyMin, DateTime date, string status);
        void Send(string message);
    }
}
