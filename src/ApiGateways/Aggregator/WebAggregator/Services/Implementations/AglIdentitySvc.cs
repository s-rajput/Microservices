using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using System.Net.Http;
using Newtonsoft.Json.Linq;
using WebAggregator.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAggregator.Config;
using Polly.Retry;
using Polly.Timeout;
using Polly;

namespace WebAggregator.Services.Implementations
{
    public class AglIdentitySvc : IAglIdentitySvc
    {

        private readonly HttpClient client;
        private readonly ILogger<AglIdentitySvc> _logger;
        private readonly IdentityConfig _urls;

        //POLLY retry pattern for microservices
        readonly AsyncRetryPolicy<HttpResponseMessage> _httpRetryPolicy;
        readonly AsyncTimeoutPolicy _timeoutPolicy;

        public AglIdentitySvc(HttpClient httpClient, ILogger<AglIdentitySvc> logger, IOptions<IdentityConfig> config)
        {
            client = httpClient;
            _logger = logger;
            _urls = config.Value;

            //set polly retry policies for microservices
            _httpRetryPolicy =
                 Policy.HandleResult<HttpResponseMessage>(msg => msg.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                 .Or<TimeoutRejectedException>()
                 .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //set timeout policy
            _timeoutPolicy = Policy.TimeoutAsync(25);
        }

        public async Task<Token> GetToken()
        {
            // discover endpoints from metadata  
            var disco = await client.GetDiscoveryDocumentAsync(_urls.IdentityURL);
            if (disco.IsError) { return null; }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _urls.ClientId,
                ClientSecret = _urls.ClientSecret,
                Scope = _urls.Scope
            });

            if (tokenResponse.IsError) { return null; }

            return JsonConvert.DeserializeObject<Token>(tokenResponse.Json.ToString());

        }
    }
     
}
