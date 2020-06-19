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

            CreateMap<Patient, Date>().ForMember(d => d.DateTime, p => p.MapFrom(src => src.DateOfBirth));
            CreateMap<Doctor, Date>().ForMember(dt => dt.DateTime, d => d.MapFrom(src => src.DateOfBirth));
            CreateMap<Appointment, Date>().ForMember(d => d.DateTime, ap => ap.MapFrom(src => src.DateTime));


            CreateMap<DoctorResource, Doctor>();
            CreateMap<PatientResource, Patient>();
            CreateMap<AppointmentResource, Appointment>();

            CreateMap<User, Doctor>();
            CreateMap<User, Patient>();

            CreateMap<Date, Patient>().ForMember(p => p.DateOfBirth, p => p.MapFrom(src => src.DateTime));
            CreateMap<Date, Doctor>().ForMember(d => d.DateOfBirth, dt => dt.MapFrom(src => src.DateTime));
            CreateMap<Date, Appointment>().ForMember(a => a.DateTime, ap => ap.MapFrom(src => src.DateTime));
        }
    }
}
