using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using MedAppCore.Models;
using MedAppCore.Models.ElasticSearch;
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

        [HttpGet("CreateIndex/Users")]
        public void CreateIndex()
        {
            _userSearchService.CreateIndex();
        }

        [HttpGet("DeleteIndex/Users")]
        public async Task DeleteIndex()
        {
            await _userSearchService.DeleteIndex();
        }

        [HttpGet("DeleteFromIndex/Users")]
        public async Task DeleteFromIndex()
        {
            await _userSearchService.DeleteFromIndex();
        }

        [HttpGet("AddToIndex/Users")]
        public async Task AddDoctors()
        {
            await _userSearchService.AddRangeToIndexAsync();
        }

        [HttpGet("Search/Users")]
        public List<string> Search(string keyWord, int? skip, int? size)
        {
            var result = _userSearchService.GetAllUris(keyWord, skip, size);
            return result;
        }

        [HttpGet("Search/UsersAutocomplete")]
        public List<string> SearchAutocomplete(string keyWord)
        {
            var result = _userSearchService.GetUrisAutocomplete(keyWord);
            return result;
        }

        [HttpGet("Search/Users/WithType")]
        public List<string> SearchWithType(string keyWord, int? skip, int? size)
        {
            var result = _userSearchService.GetUrisWithType(keyWord, skip, size, typeof(Doctor).ToString());
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
        public List<string> SearchDate(DateTime keyWord, int? skip, int? size)
        {
            var result = _dateSearchService.GetAllUris(keyWord, skip, size);
            return result;
        }

        [HttpGet("Search/Date/WithType")]
        public List<string> SearchDateWithType(DateTime keyWord, int? skip, int? size)
        {
            var result = _dateSearchService.GetUrisWithType(keyWord, skip, size, typeof(Doctor).ToString());
            return result;
        }

        [HttpGet("Delete/Index")]
        public async Task SearchDate(string name)
        {
            await _dateSearchService.DeleteIndex(name);
        }
    }
}
