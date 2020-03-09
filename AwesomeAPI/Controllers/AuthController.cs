using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLB;
using ServiceLB.Models;

namespace AwesomeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService
            )
        {
            _logger = logger;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Auth auth)

        {
            string result = await _authService.CreateAccessToken(auth).ConfigureAwait(false);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(new { token = result });
        }
    }
}