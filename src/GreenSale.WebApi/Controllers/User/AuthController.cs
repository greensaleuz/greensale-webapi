using GreenSale.Persistence.Dtos;
using GreenSale.Persistence.Dtos.Auth;
using GreenSale.Persistence.Validators;
using GreenSale.Service.Interfaces.Auth;
using GreenSaleuz.Persistence.Validators.Dtos.AuthUserValidators;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromForm] UserRegisterDto dto)
        {
            UserRegisterValidator validations = new UserRegisterValidator();
            var resltvalid = validations.Validate(dto);
            if (resltvalid.IsValid)
            {
                var result = await _authService.RegisterAsync(dto);

                return Ok(new { result.Result, result.CachedMinutes });
            }
            else
                return BadRequest(resltvalid.Errors);

        }

        [HttpPost("register/send-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCodeAsync(string phone)
        {
            var valid = PhoneNumberValidator.IsValid(phone);
            if (valid)
            {
                var result = await _authService.SendCodeForRegisterAsync(phone);

                return Ok(new { result.Result, result.CachedVerificationMinutes });
            }
            else
                return BadRequest("Phone number invalid");

        }

        [HttpPost("register/verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerfyUserDto dto)
        {
            var res = PhoneNumberValidator.IsValid(dto.PhoneNumber);
            if (res == false) return BadRequest("Phone number is invalid!");
            var srResult = await _authService.VerifyRegisterAsync(dto.PhoneNumber, dto.Code);
            return Ok(new { srResult.Result, srResult.Token });
        }

        [HttpPost("login/verify")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto dto)
        {
            var res = PhoneNumberValidator.IsValid(dto.PhoneNumber);
            if (res == false)
                return BadRequest("Phone number is invalid!");

            var serviceResult = await _authService.LoginAsync(dto);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }
    }
}
