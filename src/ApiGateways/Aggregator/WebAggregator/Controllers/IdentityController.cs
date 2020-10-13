using System;
using System.Collections.Generic; 
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using WebAggregator.Models;
using WebAggregator.Services;

namespace WebAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        //inject the service
        private readonly IAglIdentitySvc _svc; 

      
        public IdentityController(IAglIdentitySvc svc)
        { 
            _svc = svc; 
        }

        /// <summary>
        ///To get token from identity server
        /// </summary>
        [ProducesResponseType(200, Type = typeof(Token))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("GetToken")]
        [HttpGet]
        public async Task<ActionResult<Token>> Get()
        {
            try
            {
                var cats = await _svc.GetToken();
                if (cats == null) { return Unauthorized(); }
                else { return Ok(cats); }
            }
            catch (AggregateException)
            {
                return BadRequest();
            }
        }
      
    } 
} 