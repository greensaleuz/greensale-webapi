using GreenSale.Application.Utils;
using GreenSale.DataAccess.ViewModels.Users;
using GreenSale.Persistence.Dtos;
using GreenSale.Persistence.Dtos.UserDtos;

namespace GreenSale.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> DeleteAsync(long userId);
    public Task<bool> UpdateAsync(long userId, UserUpdateDto dto);
    public Task<long> CountAsync();
    public Task<List<UserViewModel>> GetAllAsync(PaginationParams @params);
    public Task<UserViewModel> GetByIdAsync(long userId);
}
