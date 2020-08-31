using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Client
{
    public interface IApiCall
    {
        Task<HttpResponseMessage> Create<TEntity>(TEntity entity, string baseUrl, string pathUrl);
        Task<TEntity> GetById<TEntity>(string baseUrl, string pathUrl, string id);
        Task<HttpStatusCode> Delete(string baseUrl, string pathUrl, string id);
    }
}
