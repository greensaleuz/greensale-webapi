using GreenSale.Application.Utils;
using GreenSale.Persistence.Dtos.RoleDtos;
using GreenSale.Persistence.Validators.Roles;
using GreenSale.Service.Interfaces.Roles;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin.Roles
{
    [Route("api/admin/roles")]
    [ApiController]
    public class AdminRolesController : AdminBaseController
    {
        private readonly IRoleService _service;

        public AdminRolesController(IRoleService roleService)
        {
            this._service = roleService;
        }
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateAsync(long roleId, RoleCreatDto dto)
        {
            RoleValidator validationRules = new RoleValidator();
            var result = validationRules.Validate(dto);

            if (result.IsValid)
                return Ok(await _service.UpdateAsync(roleId, dto));

            return BadRequest(result.Errors);
        }
    }
}
