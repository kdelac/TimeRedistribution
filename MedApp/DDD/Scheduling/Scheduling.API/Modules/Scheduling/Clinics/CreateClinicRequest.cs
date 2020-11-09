using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.API.Modules.Scheduling.Clinics
{
    public class CreateClinicRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
