using GreenSale.Persistence.Dtos;
using GreenSale.Service.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.User
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authServices)
        {
            this._authService = authServices;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromForm] UserRegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(new { result.Result, result.CachedMinutes });
        }
    }
}
