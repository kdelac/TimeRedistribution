using AutoMapper;
using ElasticSearch;
using ElasticSearch.Models;
using MedAppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRedistribution.Resources;

namespace TimeRedistribution.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorResource>();
            CreateMap<Patient, PatientResource>();
            CreateMap<Appointment, AppointmentResource>();
            CreateMap<Doctor, Users>();

            CreateMap<DoctorResource, Doctor>();
            CreateMap<PatientResource, Patient>();
            CreateMap<AppointmentResource, Appointment>();
            CreateMap<Users, Doctor>();
        }
    }
}
