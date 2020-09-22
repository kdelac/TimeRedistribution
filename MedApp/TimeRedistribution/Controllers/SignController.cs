using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Signatures;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;

namespace TimeRedistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignController : ControllerBase
    {
        private readonly ISignService _signService;
        public SignController(ISignService signService)
        {
            _signService = signService;
        }

        [HttpPost]
        public void SignDocument([FromForm]SigningDocument file)
        {
            file.Location = "Varaždin";
            file.destinationSave = @"C:\Users\CSVarazdin\Documents\GitHub\TimeRedistribution\MedApp\SecretToken\";
            file.CertificatDestination = @"C:\Users\CSVarazdin\Documents\GitHub\TimeRedistribution\MedApp\SecretToken\mediion.com.p12";
            file.Password = "Cert1f1kat";
            _signService.SignDocument(file);
            //return Ok();
        }
    }
}
