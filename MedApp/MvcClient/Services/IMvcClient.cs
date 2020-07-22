using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClient.Services
{
    public interface IMvcClient
    {
        Task<HttpClient> GetClient();
    }
}