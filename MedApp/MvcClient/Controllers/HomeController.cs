using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using MvcClient.Services;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMvcClient _mvcHttpClient;


        public HomeController(IMvcClient mvcHttpClient)
        {
            _mvcHttpClient = mvcHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = await _mvcHttpClient.GetClient();

            var response = await httpClient.GetAsync("api/Doctor").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var doctor = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var doctorViewModel = JsonConvert.DeserializeObject<List<Doctor>>(doctor).ToList();

                return View(doctorViewModel);
            }

            throw new Exception($"Problem with fetching data from the API: {response.ReasonPhrase}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
