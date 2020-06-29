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
            CreateMap<Doctor, DoctorResource>().ReverseMap();
            CreateMap<Patient, PatientResource>().ReverseMap();
            CreateMap<Appointment, AppointmentResource>().ReverseMap();

            CreateMap<Doctor, User>()
                .BeforeMap((d, p) => p.Path = "api/Doctor/")
                .BeforeMap((d, p) => p.Type = typeof(Doctor).ToString())
                .BeforeMap((d, p) => p.Suggest = new Nest.CompletionField { Input = new[] { d.Name } })
                .ReverseMap();
            CreateMap<Patient, User>()
                .BeforeMap((d, p) => p.Type = typeof(Patient).ToString())
                .BeforeMap((d, p) => p.Path = "api/Patient/")
                .BeforeMap((d, p) => p.Suggest = new Nest.CompletionField { Input = new[] { d.Name } })
                .ReverseMap();

            CreateMap<Patient, Date>()
                .BeforeMap((d, p) => p.Type = typeof(Patient).ToString())
                .ForMember(d => d.DateTime, p => p.MapFrom(src => src.DateOfBirth))
                .BeforeMap((d, p) => p.Path = "api/Patient/")
                .ReverseMap();
            CreateMap<Doctor, Date>()
                .BeforeMap((d, p) => p.Type = typeof(Doctor).ToString())
                .ForMember(dt => dt.DateTime, d => d.MapFrom(src => src.DateOfBirth))
                .BeforeMap((d, p) => p.Path = "api/Doctor/")
                .ReverseMap();
            CreateMap<Appointment, Date>()
                .BeforeMap((d, p) => p.Type = typeof(Appointment).ToString())
                .ForMember(d => d.DateTime, ap => ap.MapFrom(src => src.DateTime))
                .BeforeMap((d, p) => p.Path = "api/Appointment/")
                .ReverseMap();

            CreateMap<UriCreator, Date>()
                .ReverseMap();

            CreateMap<UriCreator, User>()
                .ReverseMap();


        }
    }
}
