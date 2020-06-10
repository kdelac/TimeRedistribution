using iText.Kernel.Pdf;
using MedAppCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface ISignService
    {
        void SignDocument(SigningDocument sd);
    }
}
