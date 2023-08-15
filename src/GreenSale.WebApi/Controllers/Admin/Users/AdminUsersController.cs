using GreenSale.Application.Utils;
using GreenSale.Persistence.Dtos;
using GreenSale.Persistence.Dtos.UserDtos;
using GreenSale.Persistence.Validators.Users;
using GreenSale.Service.Interfaces.Users;
using GreenSaleuz.Persistence.Validators.Dtos.AuthUserValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin.Users
{
    [Route("api/admin/users")]
    [ApiController]
    public class AdminUsersController : AdminBaseController
    {
        private readonly IUserService _userService;
        private readonly int maxPage = 30;

        public AdminUsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _userService.GetAllAsync(new PaginationParams(page, maxPage)));

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
            => Ok(await _userService.GetByIdAsync(userId));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _userService.CountAsync());

        [HttpPut("{userId}")]
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

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(long userId)
            => Ok(await _userService.DeleteAsync(userId));
    }
}
