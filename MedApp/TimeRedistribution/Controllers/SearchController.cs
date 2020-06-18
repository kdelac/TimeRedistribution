using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ElasticSearch;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly INameSearchService _nameSearchService;
        private readonly IMapper _mapper;

        public SearchController(INameSearchService nameSearchService)
        {
            _nameSearchService = nameSearchService;
        }

        [HttpGet("CreateIndex")]
        public void CreateIndex()
        {
            _nameSearchService.CreateIndex();
        }

        [HttpGet("DeleteIndex")]
        public async Task DeleteIndex()
        {
            await _nameSearchService.DeleteIndex();
        }

        [HttpGet("DeleteFromIndex")]
        public async Task DeleteFromIndex()
        {
            await _nameSearchService.DeleteFromIndex();
        }

        [HttpGet("AddDoctors")]
        public async Task AddDoctors()
        {
            await _nameSearchService.AddRangeToIndexAsync();
        }

        [HttpGet("Search")]
        public void Search(string keyWord)
        {
            _nameSearchService.GetFromIndex(keyWord);
        }
    }
}
