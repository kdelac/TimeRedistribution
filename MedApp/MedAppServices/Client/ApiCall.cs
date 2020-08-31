using MedAppCore.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedAppServices.Client
{
    public class ApiCall : IApiCall
    {
        private HttpClient _httpClient;

        public ApiCall()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> Create<TEntity>(TEntity entity, string baseUrl, string pathUrl)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var obj = JsonConvert.SerializeObject(entity);
            var response = await _httpClient.PostAsync(pathUrl, new StringContent(obj, Encoding.UTF8, "application/json")).ConfigureAwait(false);

            return response;
        }

        public async Task<TEntity> GetById<TEntity>(string baseUrl, string pathUrl, string id)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var response = await _httpClient.GetAsync(CreatePathUrl(pathUrl, id));

            return await response.Content.ReadAsAsync<TEntity>();
        }

        public async Task<HttpStatusCode> Delete(string baseUrl, string pathUrl, string id)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            HttpResponseMessage response = await _httpClient.DeleteAsync(CreatePathUrl(pathUrl, id));
            return response.StatusCode;
        }

        private string CreatePathUrl(string pathUrl, string id)
        {
            return $"{pathUrl}/{id}";
        }
    }
}
