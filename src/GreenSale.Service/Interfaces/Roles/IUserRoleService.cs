using GreenSale.Application.Utils;
using GreenSale.DataAccess.ViewModels.UserRoles;
using GreenSale.Domain.Entites.Roles;
using GreenSale.Persistence.Dtos.RoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Service.Interfaces.Roles
{
    public interface IUserRoleService
    {
        public Task<bool> UpdateAsync(long UserroleId, UserRoleDtoUpdate dto);
        public Task<bool> DeleteAsync(long UserroleId);
        public Task<List<UserRoleViewModel>> GetAllAsync(PaginationParams @params);
        public Task<UserRoleViewModel> GetByIdAsync(long UserroleId);
        public Task<long> CountAsync();
    }
}
