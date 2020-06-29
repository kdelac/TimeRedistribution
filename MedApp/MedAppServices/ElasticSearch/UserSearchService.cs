using AutoMapper;
using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Models.ElasticSearch;
using MedAppCore.Services;
using MedAppCore.Services.ElasticSearch;
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
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        private readonly string indexName = "users";
        public UserSearchService(IUnitOfWork unitOfWork, IPatientService patientService, IDoctorService doctorService, IMapper mapper, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
            _patientService = patientService;
            _mapper = mapper;
            _uriService = uriService;
        }
        public void CreateIndex()
        {
            _unitOfWork.UserSearch.CreateIndex(indexName);
        }

        public async Task DeleteFromIndex()
        {
            await _unitOfWork.UserSearch.DeleteAllFromIndex(indexName);
        }

        public async Task DeleteIndex()
        {
            await _unitOfWork.UserSearch.DeleteIndexAsync(indexName);
        }

        public List<string> GetUrisWithType(string keyWord, int? skip, int? size, string type)
        {
            var result = _unitOfWork.UserSearch.OnGet(keyWord, indexName, skip, size, type);
            var results = result.Documents.ToList();
            var resultsUri = _mapper.Map<List<User>, List<UriCreator>>(results);
            return _uriService.CreateUris(resultsUri);
        }

        public List<string> GetAllUris(string keyWord, int? skip, int? size)
        {
            var result = _unitOfWork.UserSearch.OnGet(keyWord, indexName, skip, size, null);
            var results = result.Documents.ToList();
            var resultsUri = _mapper.Map<List<User>, List<UriCreator>>(results);
            return _uriService.CreateUris(resultsUri);
        }

        public List<string> GetUrisAutocomplete(string keyWord)
        {
            var results = _unitOfWork.UserSearch.AutocompleteSearch(keyWord, indexName).ToList();
            var resultsUri = _mapper.Map<List<User>, List<UriCreator>>(results);
            return _uriService.CreateUris(resultsUri);
        }

        public async Task AddRangeToIndexAsync()
        {
            var doctor = await _doctorService.GetAll();
            var patient = await _patientService.GetAll();
            var doctors = doctor.ToList();
            var patients = patient.ToList();

            var users = _mapper.Map<List<Doctor>, List<User>>(doctors);
            await _unitOfWork.UserSearch.AddToIndex(users, indexName);

            var usersP = _mapper.Map<List<Patient>, List<User>>(patients);
            await _unitOfWork.UserSearch.AddToIndex(usersP, indexName);
        }        
    }
}
