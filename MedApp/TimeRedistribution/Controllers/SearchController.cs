using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedAppCore.Models;
using MedAppCore.Services;
using MedAppCore.Services.ElasticSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IUserSearchService _userSearchService;
        private readonly IDateSearchService _dateSearchService;
        private readonly IMapper _mapper;

        public SearchController(IUserSearchService userSearchService, IDateSearchService dateSearchService)
        {
            _userSearchService = userSearchService;
            _dateSearchService = dateSearchService;
        }

        [HttpGet("CreateIndex")]
        public void CreateIndex()
        {
            _userSearchService.CreateIndex();
        }

        [HttpGet("DeleteIndex")]
        public async Task DeleteIndex()
        {
            await _userSearchService.DeleteIndex();
        }

        [HttpGet("DeleteFromIndex")]
        public async Task DeleteFromIndex()
        {
            await _userSearchService.DeleteFromIndex();
        }

        [HttpGet("AddToIndex")]
        public async Task AddDoctors()
        {
            await _userSearchService.AddRangeToIndexAsync();
        }

        [HttpGet("Search")]
        public List<string> Search(string keyWord)
        {
            var result =  _userSearchService.GetUrisIndex(keyWord);
            return result;
        }

        [HttpGet("CreateIndex/Date")]
        public void CreateIndexDate()
        {
            _dateSearchService.CreateIndex();
        }

        [HttpGet("DeleteIndex/Date")]
        public async Task DeleteIndexDate()
        {
            await _dateSearchService.DeleteIndex();
        }

        [HttpGet("DeleteFromIndex/Date")]
        public async Task DeleteFromIndexDate()
        {
            await _dateSearchService.DeleteFromIndex();
        }

        [HttpGet("AddToIndex/Date")]
        public async Task AddDoctorsDate()
        {
            await _dateSearchService.AddRangeToIndexAsync();
        }

        [HttpGet("Search/Date")]
        public List<string> SearchDate(DateTime keyWord)
        {
            var result = _dateSearchService.GetUrisIndex(keyWord);
            return result;
        }
    }
}
