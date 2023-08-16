using GreenSale.Application.Exceptions.Users;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.ViewModels.Users;
using GreenSale.Domain.Entites.Users;
using GreenSale.Persistence.Dtos;
using GreenSale.Persistence.Dtos.UserDtos;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.Users;
using GreenSale.Service.Security;
using static Dapper.SqlMapper;

namespace GreenSale.Service.Service.Users;

public class UserService : IUserService
{
    private readonly IIdentityService _identity;
    private readonly IPaginator _paginator;
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository,
        IPaginator paginator,
        IIdentityService identity)
    {
        this._identity = identity;
        this._paginator = paginator;
        this._userRepository = userRepository;
    }

    public async Task<long> CountAsync()
    {
        var DbResult = await _userRepository.CountAsync();

        return DbResult;
    }

    public async Task<bool> DeleteAsync(long userId)
    {
        var DbFound = await _userRepository.GetByIdAsync(userId);
        if (DbFound is null)
            throw new UserNotFoundException();
       
        var DbResult = await _userRepository.DeleteAsync(userId);

        return DbResult > 0;
    }

    public async Task<List<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var DbResult = await _userRepository.GetAllAsync(@params);
        var count = await _userRepository.CountAsync();
        _paginator.Paginate(count, @params);

        return DbResult;
    }

    public async Task<UserViewModel> GetByIdAsync(long userId)
    {
        var DbFound = await _userRepository.GetByIdAsync(userId);
        if(DbFound is null)
            throw new UserNotFoundException();
        var DbResult = await _userRepository.GetByIdAsync(userId);

        return DbResult;
    }

    public async Task<bool> UpdateAsync(UserUpdateDto dto)
    {
        var DbFound = await _userRepository.GetByIdAsync(_identity.Id);

        if( DbFound is null)
            throw new UserNotFoundException();

        User user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirme = true,
            Region = dto.Region,
            District = dto.District,
            Address = dto.Address
        };
        var hasher = PasswordHasher.Hash(dto.Password);
        user.PasswordHash = hasher.Hash;
        user.Salt = hasher.Salt;
        var DbResult = await _userRepository.UpdateAsync(_identity.Id,user);

        return DbResult > 0;
    }

    public async Task<bool> UpdateByAdminAsync(long userId, UserUpdateDto dto)
    {
        var DbFound = await _userRepository.GetByIdAsync(userId);

        if (DbFound is null)
            throw new UserNotFoundException();

        User user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirme = true,
            Region = dto.Region,
            District = dto.District,
            Address = dto.Address
        };
        var hasher = PasswordHasher.Hash(dto.Password);
        user.PasswordHash = hasher.Hash;
        user.Salt = hasher.Salt;
        var DbResult = await _userRepository.UpdateAsync(userId, user);

        return DbResult > 0;
    }
}
