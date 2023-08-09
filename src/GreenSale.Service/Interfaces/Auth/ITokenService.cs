using GreenSale.DataAccess.ViewModels.UserRoles;
using GreenSale.Domain.Entites.Users;

namespace GreenSale.Service.Interfaces.Auth;

internal interface ITokenService
{
    public string GenerateToken(UserRoleViewModel user);
}
