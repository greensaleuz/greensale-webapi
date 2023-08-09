using GreenSale.DataAccess.ViewModels.UserRoles;
using GreenSale.Domain.Entites.Roles.UserRoles;

namespace GreenSale.DataAccess.Interfaces.Roles
{
    public interface IUserRoles : IRepository<UserRole, UserRoleViewModel>
    { }
}