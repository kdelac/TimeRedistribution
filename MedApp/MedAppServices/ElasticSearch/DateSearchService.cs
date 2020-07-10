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
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        private readonly string indexName = "dates";
        public DateSearchService(IUnitOfWork unitOfWork, IPatientService patientService, IDoctorService doctorService, IAppointmentService appointmentService, IMapper mapper, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
            _mapper = mapper;
            _uriService = uriService;
        }
        public void CreateIndex()
        {
            _unitOfWork.DateSearch.CreateIndex(indexName);
        }

        public async Task DeleteFromIndex()
        {
            await _unitOfWork.DateSearch.DeleteAllFromIndex(indexName);
        }

        public async Task DeleteIndex()
        {
            await _unitOfWork.DateSearch.DeleteIndexAsync(indexName);
        }

        public async Task DeleteIndex(string name)
        {
            await _unitOfWork.DateSearch.DeleteIndexAsync(name);
        }

        public List<string> GetUrisWithType(DateTime keyWord, int? skip, int? size, string type)
        {
            var result = _unitOfWork.DateSearch.OnGet(keyWord, indexName, skip, size, type);
            var results = result.Documents.ToList();
            var resultsUri = _mapper.Map<List<Date>, List<UriCreator>>(results);
            return _uriService.CreateUris(resultsUri);
        }

        public List<string> GetAllUris(DateTime keyWord, int? skip, int? size)
        {
            var result = _unitOfWork.DateSearch.OnGet(keyWord, indexName, skip, size, null);
            var results = result.Documents.ToList();
            var resultsUri = _mapper.Map<List<Date>, List<UriCreator>>(results);
            return _uriService.CreateUris(resultsUri);
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
