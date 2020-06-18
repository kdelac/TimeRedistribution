using AutoMapper;
using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Models.ElasticSearch;
using MedAppCore.Services;
using MedAppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class UserSearchService : IUserSearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;        
        private readonly IMapper _mapper;
        private readonly string indexName = "users";
        private readonly string url = "https://localhost:44308/";
        public UserSearchService(IUnitOfWork unitOfWork, IPatientService patientService, IDoctorService doctorService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
            _patientService = patientService;
            _mapper = mapper;
        }
        public void CreateIndex()
        {
            _unitOfWork.UserSearch.CreateIndex(indexName);
        }

        public async Task DeleteFromIndex()
        {
            await _unitOfWork.UserSearch.DeleteAllFromIndex();
        }

        public async Task DeleteIndex()
        {
            await _unitOfWork.UserSearch.DeleteIndexAsync(indexName);
        }

        public List<string> GetUrisIndex(string keyWord)
        {
            var result = _unitOfWork.UserSearch.OnGet(keyWord, indexName);
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
            var users = _mapper.Map<List<Doctor>, List<User>>(doctors);
            users.ForEach(_ => _.Path = "api/Doctor/");
            await _unitOfWork.UserSearch.AddToIndex(users, indexName);
            var usersP = _mapper.Map<List<Patient>, List<User>>(patients);
            usersP.ForEach(_ => _.Path = "api/Patient/");
            await _unitOfWork.UserSearch.AddToIndex(usersP, indexName);
        }
    }
}
