using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLB;
using ServiceLB.StaticModels;

namespace AwesomeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = nameof(Role.Admin))]
    public class CacheController : ControllerBase
    {
        private readonly ILogger<CacheController> _logger;

        private readonly ICacheService _cacheService;

        public CacheController(
            ILogger<CacheController> logger,
            ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        [HttpPost("set/{key}/{set}")]
        public async Task<IActionResult> Set([FromRoute, Required]string key, [FromRoute, Required]string set)
        {
            await _cacheService.Set(key, set).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery, Required]string key)
        {
            string result = await _cacheService.Get(key).ConfigureAwait(false);

            if (string.IsNullOrEmpty(result))
            {
                return NotFound("Cache Result Not Found");
            }

            return Ok(result);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute, Required]string key)
        {
            await _cacheService.Delete(key).ConfigureAwait(false);

            return Ok();
        }
    }
}