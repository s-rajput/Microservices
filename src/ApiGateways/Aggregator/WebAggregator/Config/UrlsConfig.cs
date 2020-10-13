using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAggregator.Config
{
    public class UrlsConfig
    {
        public class PetsOperations
        {
            public static string Get() => "/api/Pets"; 
        }
        public string PetsMicroservice { get; set; }
    }
}
