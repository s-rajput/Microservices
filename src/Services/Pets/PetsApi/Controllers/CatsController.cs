using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PetsApi.Models;
using PetsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;

namespace PetsApi.Controllers
{
    [ApiController] 
    [Authorize]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        //inject the service
        private readonly IPetsApi _svc; 

      
        public PetsController(IPetsApi svc)
        { 
            _svc = svc; 
        }

        /// <summary>
        ///Function to get cats from the api
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<PetData>>> Get()
        {
            var cats = await _svc.GetPets();

            if (cats == null)
            {
                //return 404
                return NotFound();
            }
            else
            {
                //return 200
                return Ok(cats);
            }
        }

    } 
} 
