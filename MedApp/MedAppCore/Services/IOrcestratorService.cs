using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAppCore
{
    public interface IOrcestratorService
    {
        void LogStrat();
        void Listening();
    }
}