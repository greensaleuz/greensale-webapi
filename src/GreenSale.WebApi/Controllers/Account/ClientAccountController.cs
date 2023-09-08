using GreenSale.Persistence.Dtos.UserDtos;
using GreenSale.Persistence.Validators.Users;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Users;
using GreenSale.WebApi.Controllers.Client;
using GreenSale.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Account
{
    [Route("api/profile")]
    [ApiController]
    public class ClientAccountController : BaseController
    {
        private readonly IIdentityService _identity;
        private readonly IUserService _userService;

        public ClientAccountController(
            IUserService userService,
            IIdentityService identity)
        {
            _identity = identity;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
            => Ok(await _userService.GetByIdAsync(_identity.Id));

        [HttpPut("information")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto dto)
        {
            UserUpdateValidator validations = new UserUpdateValidator();
            var resltvalid = validations.Validate(dto);
            if (resltvalid.IsValid)
            {
                var result = await _userService.UpdateAsync(dto);

                return Ok(result);
            }
            else
                return BadRequest(resltvalid.Errors);
        }

        [HttpPut("security")]
        public async Task<IActionResult> UpdateSecurity([FromBody] UserSecurityUpdate dto)
        {
            var result = await _userService.UpdateSecuryAsync(dto);
            return Ok(result);
        }
    }
}
