using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Collections.Generic; 
using System.Net.Http;
using System.Threading.Tasks; 
using WebAggregator.Config;
using WebAggregator.Models;

namespace WebAggregator.Services.Implementations
{
    public class AglCoreSvc : IAglCoreSvc
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AglCoreSvc> _logger;
        private readonly UrlsConfig _urls;
        private readonly IAglIdentitySvc _IdentityServer;

        //POLLY retry pattern
        readonly AsyncRetryPolicy<HttpResponseMessage> _httpRetryPolicy;
        readonly AsyncTimeoutPolicy _timeoutPolicy;

        public AglCoreSvc(HttpClient httpClient, ILogger<AglCoreSvc> logger, IOptions<UrlsConfig> config, IAglIdentitySvc IdentityServer)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
            _IdentityServer = IdentityServer;

            //set polly retry policies for microservices
            _httpRetryPolicy =
                 Policy.HandleResult<HttpResponseMessage>(msg => msg.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                 .Or<TimeoutRejectedException>()
                 .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //set timeout policy
            _timeoutPolicy = Policy.TimeoutAsync(25);
        }

        async Task<List<PetData>> IAglCoreSvc.GetPets()
        {
            var url = _urls.PetsMicroservice + UrlsConfig.PetsOperations.Get();

            //call identity server to get the token
            var TokenObj = await _IdentityServer.GetToken();

            //set bearer
            _httpClient.SetBearerToken(TokenObj.Access_Token);

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var catsResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PetData>>(catsResponse);
        }

    }
}
