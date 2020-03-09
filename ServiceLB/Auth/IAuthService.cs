using System.Threading.Tasks;
using ServiceLB.Models;

namespace ServiceLB
{
    public interface IAuthService
    {
        Task<string> CreateAccessToken(Auth auth);
    }
}