using System.Threading.Tasks;

namespace ServiceLB
{
    public interface ICacheService
    {
        Task<bool> Delete(string key);
        Task<string> Get(string key);
        Task<bool> Set(string key, string value);
    }
}