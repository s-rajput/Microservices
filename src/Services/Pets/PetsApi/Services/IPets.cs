
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PetsApi.Services
{
    public interface IPets
    {
        Task<List<PetData>> GetPets();
    }
}