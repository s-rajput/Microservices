using System.Collections.Generic;
using System.Threading.Tasks;

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
