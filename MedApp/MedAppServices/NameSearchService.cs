using AutoMapper;
using ElasticSearch;
using ElasticSearch.Models;
using MedAppCore.Models;
using MedAppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class NameSearchService : INameSearchService
    {
        private readonly Work _work;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly string indexName = "users";
        public NameSearchService(Work work, IPatientService patientService, IDoctorService doctorService)
        {
            _work = work;
            _doctorService = doctorService;
            _patientService = patientService;
        }
        public void CreateIndex()
        {
            _work.NameSearch.CreateIndex(indexName);
        }

        public async Task DeleteFromIndex()
        {
            await _work.NameSearch.DeleteAllFromIndex(indexName);
        }

        public async Task DeleteIndex()
        {
             await _work.NameSearch.DeleteIndexAsync(indexName);
        }

        public List<Users> GetFromIndex(string keyWord)
        {
            var result = _work.NameSearch.OnGet(keyWord);
            var results = result.Documents.ToList();
            return results;
        }

        public async Task AddRangeToIndexAsync()
        {
            var doctor = await _doctorService.GetAll();
            List<Users> users = new List<Users>();
            doctor.ToList().ForEach(_ => users.Add(new Users { Id = _.Id, Name = _.Name, Surname = _.Surname}));
            await _work.NameSearch.AddToIndex(users, indexName);
        }
    }
}
