using GreenSale.Application.Utils;
using GreenSale.Persistence.Dtos.RoleDtos;
using GreenSale.Service.Interfaces.Roles;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.SuperAdmin.Roles;

[Route("api/superadmin/UserRoles")]
[ApiController]
public class SuperAdminUsersRoleController : SuperAdminBaseController
{
    private readonly IUserRoleService _service;
    private int maxPage = 30;

    public SuperAdminUsersRoleController(IUserRoleService userRoleService)
    {
        this._service = userRoleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPage)));

    [HttpGet("{UserRoleId}")]
    public async Task<IActionResult> GetByIdAsync(long UserRoleId)
        => Ok(await _service.GetByIdAsync(UserRoleId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpPut("{UserRoleId}")]
    public async Task<IActionResult> UpdateAsync(long UserRoleId, UserRoleDtoUpdate dto)
        => Ok(await _service.UpdateAsync(UserRoleId, dto));

    [HttpDelete("{UserRoleId}")]
    public async Task<IActionResult> DeleteAsync(long UserRoleId)
        => Ok(await _service.DeleteAsync(UserRoleId));
}
