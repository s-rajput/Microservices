using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System.Net.Http;
using PetsApi.Config;
using PetsApi.Models;
using System.Text.RegularExpressions;

//for unit testing secure content
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("PetsApi.Tests")]

namespace PetsApi.Services.Implementations
{
    public class PetsApiSvc : IPetsApi
    { 
        IPets _pets;

        //constructor injection
        public PetsApiSvc(IPets pets)
        { 
            _pets = pets;

        }

       
        public async Task<List<PetData>> GetPets()
        {
            return await _pets.GetPets();
        }

    }
}
