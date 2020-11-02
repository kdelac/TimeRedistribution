using System;

namespace Scheduling.Application.Clinics.GetAllClinics
{
    public class ClinicsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
