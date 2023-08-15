using GreenSale.Application.Utils;
using GreenSale.DataAccess.ViewModels.Users;
using GreenSale.Persistence.Dtos;

namespace GreenSale.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> DeleteAsync(long userId);
    public Task<bool> UpdateAsync(long userId, UserRegisterDto dto);
    public Task<long> CountAsync();
    public Task<List<UserViewModel>> GetAllAsync(PaginationParams @params);
    public Task<UserViewModel> GetByIdAsync(long userId);
}
