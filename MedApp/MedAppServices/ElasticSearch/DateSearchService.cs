using AutoMapper;
using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Models.ElasticSearch;
using MedAppCore.Services;
using MedAppCore.Services.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices.ElasticSearch
{
    public class DateSearchService : IDateSearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        private readonly string indexName = "dates";
        private readonly string url = "https://localhost:44308/";
        public DateSearchService(IUnitOfWork unitOfWork, IPatientService patientService, IDoctorService doctorService, IAppointmentService appointmentService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
            _mapper = mapper;
        }
        public void CreateIndex()
        {
            _unitOfWork.DateSearch.CreateIndex(indexName);
        }

        public async Task DeleteFromIndex()
        {
            await _unitOfWork.DateSearch.DeleteAllFromIndex();
        }

        public async Task DeleteIndex()
        {
            await _unitOfWork.DateSearch.DeleteIndexAsync(indexName);
        }

        public List<string> GetUrisIndex(DateTime keyWord)
        {
            var result = _unitOfWork.DateSearch.OnGet(keyWord, indexName);
            var results = result.Documents.ToList();
            List<string> urls = new List<string>();
            results.ForEach(_ => {
                string urlFull = $"{url}{_.Path}{_.Id}";
                urls.Add(urlFull);
            });

            return urls;
        }

        public async Task AddRangeToIndexAsync()
        {
            var doctor = await _doctorService.GetAll();
            var patient = await _patientService.GetAll();
            var doctors = doctor.ToList();
            var patients = patient.ToList();
            var appoitment = await _appointmentService.GetAll();
            var appointments = appoitment.ToList();
            var dateD = _mapper.Map<List<Doctor>, List<Date>>(doctors);
            dateD.ForEach(_ => _.Path = "api/Doctor/");
            await _unitOfWork.DateSearch.AddToIndex(dateD, indexName);
            var dateP = _mapper.Map<List<Patient>, List<Date>>(patients);
            dateP.ForEach(_ => _.Path = "api/Patient/");
            await _unitOfWork.DateSearch.AddToIndex(dateP, indexName);
            var dateA = _mapper.Map<List<Appointment>, List<Date>>(appointments);
            dateA.ForEach(_ => _.Path = "api/Appointment/");
            await _unitOfWork.DateSearch.AddToIndex(dateA, indexName);
        }
    }
}
