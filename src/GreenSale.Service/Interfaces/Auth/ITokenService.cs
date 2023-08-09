using GreenSale.DataAccess.ViewModels.UserRoles;

namespace GreenSale.Service.Interfaces.Auth;

internal interface ITokenService
{
    public string GenerateToken(UserRoleViewModel user);
}
