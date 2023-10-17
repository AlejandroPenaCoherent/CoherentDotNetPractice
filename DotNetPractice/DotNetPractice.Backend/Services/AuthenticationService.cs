using DotNetPractice.Backend.Models;
using DotNetPractice.Backend.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetPractice.Backend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IdentityServerSettings _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;
        private readonly HttpClient _httpClient;

        public AuthenticationService (IConfiguration configuration)
        {
            _identityServerSettings = configuration
                .GetSection(nameof(IdentityServerSettings))
                .Get<IdentityServerSettings>();

            _httpClient = new HttpClient();
            _discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(_identityServerSettings.DiscoveryUrl).Result;

            if (_discoveryDocument.IsError)
            {
                throw new Exception($"Unable to get discovery document at ${nameof(AuthenticationService)}.", _discoveryDocument.Exception);
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = _discoveryDocument.TokenEndpoint,
                ClientId = _identityServerSettings.ClientName,
                ClientSecret = _identityServerSettings.ClientPassword,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Unable to get token.", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
