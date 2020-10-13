using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAggregator.Models
{
    public class Token
    {
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }
        public string Token_Type { get; set; }
        public string Scope { get; set; }
    }
}
