using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using ServiceLB;
using ServiceLB.Models;

namespace AwesomeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(

            IAuthService authService
            )
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Auth auth)

        {
            string result = await _authService.CreateAccessToken(auth).ConfigureAwait(false);

            if (string.IsNullOrEmpty(result) || result.Contains("not match"))
            {
                Log.Error($"Login failed email: {auth.Email}");

                return Unauthorized();
            }

            Log.Information($"Login Complete email: {auth.Email}");

            return Ok(new { token = result });
        }
    }
}