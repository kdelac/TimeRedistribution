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

        public async Task DeleteIndex(string name)
        {
            await _unitOfWork.DateSearch.DeleteIndexAsync(name);
        }

        public List<string> GetUris(DateTime keyWord, int? skip, int? size, Type type)
        {
            var result = _unitOfWork.DateSearch.OnGet(keyWord, indexName, skip, size, type);
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
            var appoitment = await _appointmentService.GetAll();
            var doctors = doctor.ToList();
            var patients = patient.ToList();            
            var appointments = appoitment.ToList();

            var dateD = _mapper.Map<List<Doctor>, List<Date>>(doctors);
            await _unitOfWork.DateSearch.AddToIndex(dateD, indexName);

            var dateP = _mapper.Map<List<Patient>, List<Date>>(patients);
            await _unitOfWork.DateSearch.AddToIndex(dateP, indexName);

            var dateA = _mapper.Map<List<Appointment>, List<Date>>(appointments);
            await _unitOfWork.DateSearch.AddToIndex(dateA, indexName);
        }
    }
}
