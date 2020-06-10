using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    
    public class SigningDocument
    {
        public IFormFile File { get; set; }
        public string Location { get; set; }
        public string destinationSave { get; set; }
        public string CertificatDestination { get; set; }
        public string Password { get; set; }
    }
}
