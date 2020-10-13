using PetsApi.Config;
using PetsApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
//for unit testing
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace PetsApi.Services.Implementations
{
    internal class Pets : IPets
    {

        private readonly HttpClient _httpClient;
        private readonly ConfigSettings _urls;
        readonly AsyncRetryPolicy<HttpResponseMessage> _httpRetryPolicy;
        readonly AsyncTimeoutPolicy _timeoutPolicy;

        //constructor injection
        public Pets(HttpClient httpClient, IOptions<ConfigSettings> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;

            //handle re-tries for http failures (can be pushed to start up at time of registeration)
            _httpRetryPolicy =
                 Policy.HandleResult<HttpResponseMessage>(msg => msg.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                 .Or<TimeoutRejectedException>()
                 .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //set timeout policy
            _timeoutPolicy = Policy.TimeoutAsync(25);

        }

        public async Task<List<PetData>> GetPets()
        {
            var Pets = await GetCityPets(Cities.Melbourne);

            Pets.AddRange(await GetCityPets(Cities.Sydney));

            return (from o in Pets.GroupBy(g => g.Gender)
                        select new PetData
                        {
                            Gender = o.First().Gender,
                            Pets = (from p in o.SelectMany(i => i.Pets.Where(j =>
                                              !string.IsNullOrWhiteSpace(j.Name))).OrderBy(x => x.Name).Select(y => y).ToArray()
                                    select new CityPets
                                    {
                                        Name = p.Name,
                                        City =p.City
                                    }).ToArray()
                        }).ToList();
        }

        private async Task<List<PetData>> GetCityPets(Cities city)
        {
            // Get cats
            try
            {
                var apiRepo = city.Equals(Cities.Sydney) ? _urls.SydneyRepositoryURL : _urls.MelbourneRepositoryURL;

                var Response = await _httpClient.GetStringAsync(apiRepo);

                var Owners = JsonConvert.DeserializeObject<List<PetOwner>>(Response);

                return (from o in Owners.Where(x => x.Pets != null).GroupBy(g => g.Gender)                        
                        select new PetData
                        {
                            Gender = o.First().Gender,
                            Pets = (from p in o.SelectMany(i => i.Pets.Where(j =>
                                              !string.IsNullOrWhiteSpace(j.Type))).OrderBy(x => x.Name).Select(y => y.Name).ToArray()
                                   select new CityPets
                                   {
                                       Name = p,
                                       City = city.ToString()
                                   }).ToArray()
                        }).ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
