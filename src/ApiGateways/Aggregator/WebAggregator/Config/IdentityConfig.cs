using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAggregator.Config
{
    public class IdentityConfig
    {
        public string IdentityURL { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
}
