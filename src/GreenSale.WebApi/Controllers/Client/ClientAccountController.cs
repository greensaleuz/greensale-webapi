﻿using GreenSale.Persistence.Dtos.UserDtos;
using GreenSale.Persistence.Validators.Users;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client
{
    [Route("api/client/profile")]
    [ApiController]
    public class ClientAccountController : BaseClientController
    {
        private readonly IIdentityService _identity;
        private readonly IUserService _userService;

        public ClientAccountController(
            IUserService userService,
            IIdentityService identity)
        {
            this._identity = identity;
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
            => Ok(await _userService.GetByIdAsync(_identity.Id));

        [HttpPut]
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
    }
}
