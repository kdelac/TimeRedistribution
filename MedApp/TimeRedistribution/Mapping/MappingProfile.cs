using AutoMapper;
using MedAppCore.Models;
using MedAppCore.Models.ElasticSearch;
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
            CreateMap<Doctor, User>();
            CreateMap<Patient, User>();
            CreateMap<Patient, Date>();
            CreateMap<Doctor, Date>();
            CreateMap<Appointment, Date>();

            CreateMap<DoctorResource, Doctor>();
            CreateMap<PatientResource, Patient>();
            CreateMap<AppointmentResource, Appointment>();
            CreateMap<User, Doctor>();
            CreateMap<User, Patient>();
            CreateMap<Date, Patient>();
            CreateMap<Date, Doctor>();
            CreateMap<Date, Appointment>();
        }
    }
}
