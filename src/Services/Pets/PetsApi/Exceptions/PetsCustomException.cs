using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsApi.Exceptions
{ 
    /// <summary>
    /// Custom Exception type for pet service exceptions
    /// </summary>
    public class PetsCustomException : Exception
    {
        public PetsCustomException()
        { }

        public PetsCustomException(string message)
            : base(message)
        { }

        public PetsCustomException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
