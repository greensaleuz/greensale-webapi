using GreenSale.Persistence.Dtos;
using GreenSale.Persistence.Dtos.UserDtos;
using GreenSale.Persistence.Validators.Users;
using GreenSale.Service.Interfaces.Users;
using GreenSale.WebApi.Controllers.Common;
using GreenSaleuz.Persistence.Validators.Dtos.AuthUserValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client
{
    [Route("api/client/profile")]
    [ApiController]
    public class ClientAccountController : BaseClientController
    {
        private readonly IUserService _userService;

        public ClientAccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long userId, [FromQuery] UserUpdateDto dto)
        {
            UserUpdateValidator validations = new UserUpdateValidator();
            var resltvalid = validations.Validate(dto);
            if (resltvalid.IsValid)
            {
                var result = await _userService.UpdateAsync(userId, dto);

                return Ok(result);
            }
            else
                return BadRequest(resltvalid.Errors);
        }
    }
}
