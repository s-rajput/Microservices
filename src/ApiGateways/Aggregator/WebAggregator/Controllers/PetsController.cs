using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using WebAggregator.Models;
using WebAggregator.Services;

namespace WebAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        //inject the service
        private readonly IAglCoreSvc _svc; 

      
        public PetsController(IAglCoreSvc svc)
        { 
            _svc = svc; 
        }

        /// <summary>
        ///Method to get cats from the api
        /// </summary>
        [ProducesResponseType(200, Type = typeof(List<PetData>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<ActionResult<List<PetData>>> Get(string name,string city,string gender)
        {
            try
            {
                var cats = await _svc.GetPets();
                if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(city)) {
                    cats = (from c in cats 
                            let p = c.Pets.Where(y => y.City.Equals(!string.IsNullOrEmpty(city) ? city : y.City,
                                                    StringComparison.CurrentCultureIgnoreCase) &&
                                                    y.Name.Equals(!string.IsNullOrEmpty(name) ? name : y.Name,
                                                    StringComparison.CurrentCultureIgnoreCase)).ToArray()
                            where p.Length > 0
                            select new PetData
                            {
                                Gender = c.Gender,
                                Pets = p
                            }).ToList();
                }
                if (!string.IsNullOrEmpty(gender))
                {
                    cats = cats.Where(x => x.Gender.Equals(gender, StringComparison.CurrentCultureIgnoreCase)).ToList();
                }
                if (cats == null) { return NotFound(); }
                else { return Ok(cats); }
            }
            catch (AggregateException) { return BadRequest(); }
        }

    } 
} 