using IdentityModel.Client;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<TokenResponse> GetToken(string scope);
    }
}
