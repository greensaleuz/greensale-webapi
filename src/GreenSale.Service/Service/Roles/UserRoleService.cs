using GreenSale.Application.Exceptions.Roles;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Roles;
using GreenSale.DataAccess.ViewModels.UserRoles;
using GreenSale.Domain.Entites.Roles.UserRoles;
using GreenSale.Persistence.Dtos.RoleDtos;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.Roles;

namespace GreenSale.Service.Service.Roles;

public class UserRoleService : IUserRoleService
{
    private readonly IPaginator _paginator;
    private readonly IUserRoles _userRole;

    public UserRoleService(
        IUserRoles userRoles,
        IPaginator paginator)
    {
        this._paginator = paginator;
        this._userRole = userRoles;
    }

    public async Task<long> CountAsync()
    {
        var DbResult = await _userRole.CountAsync();

        return DbResult;
    }

    public async Task<bool> DeleteAsync(long UserroleId)
    {
        var DbResultFound = await _userRole.GetByIdAsync(UserroleId);

        if (DbResultFound == null)
            throw new UserRoleNotFoundException();

        var DbResult = await _userRole.DeleteAsync(UserroleId);

        return DbResult > 0;
    }

    public async Task<List<UserRoleViewModel>> GetAllAsync(PaginationParams @params)
    {
        var rolesGet = await _userRole.GetAllAsync(@params);
        var count = await _userRole.CountAsync();
        _paginator.Paginate(count, @params);

        return rolesGet;
    }

    public async Task<UserRoleViewModel> GetByIdAsync(long UserroleId)
    {
        var DbResultFound = await _userRole.GetByIdAsync(UserroleId);

        if (DbResultFound == null)
            throw new UserRoleNotFoundException();

        return DbResultFound;
    }

    public async Task<bool> UpdateAsync(long UserroleId, UserRoleDtoUpdate dto)
    {
        var DbResultFound = await _userRole.GetByIdAsync(UserroleId);

        if (DbResultFound == null)
            throw new UserRoleNotFoundException();

        UserRole userRole = new UserRole()
        {
            RoleId = dto.RoleId,
        };

        var DbResult = await _userRole.UpdateAsync(UserroleId, userRole);

        return DbResult > 0;
    }
}
