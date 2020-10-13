
using PetsApi.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetsApi.Services
{
    public interface IPetsApi
    {

        Task<List<PetData>> GetPets();
    }
}
