using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Client
{
    public interface IApiCall
    {
        Task<HttpResponseMessage> Create<TEntity>(TEntity entity, string baseUrl, string pathUrl);
    }
}
