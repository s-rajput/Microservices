using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAggregator.Models;

namespace WebAggregator.Services
{
    public interface IAglCoreSvc
    {
        Task<List<PetData>> GetPets();
    }
}
