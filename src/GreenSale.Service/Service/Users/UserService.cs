using GreenSale.Application.Exceptions.Auth;
using GreenSale.Application.Exceptions.Users;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.ViewModels.Users;
using GreenSale.Persistence.Dtos.UserDtos;
using GreenSale.Persistence.Validators.Users;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.Users;
using GreenSale.Service.Security;

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

        if (DbFound is null)
            throw new UserNotFoundException();

        var DbResult = await _userRepository.GetByIdAsync(userId);

        return DbResult;
    }

    public async Task<bool> UpdateAsync(UserUpdateDto dto)
    {
        var DbFound = await _userRepository.GetByPhoneAsync(_identity.PhoneNumber);

        if (DbFound is null)
            throw new UserNotFoundException();

        DbFound.FirstName = dto.FirstName;
        DbFound.LastName = dto.LastName;
        DbFound.PhoneNumber = dto.PhoneNumber;
        DbFound.Region = dto.Region;
        DbFound.District = dto.District;
        DbFound.Address = dto.Address;

        var DbResult = await _userRepository.UpdateAsync(_identity.Id, DbFound);

        return DbResult > 0;
    }

    /*  public async Task<bool> UpdateByAdminAsync(long userId, UserUpdateDto dto)
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
      }*/

    public async Task<bool> UpdateSecuryAsync(UserSecurityUpdate dto)
    {
        var user = await _userRepository.GetByPhoneAsync(_identity.PhoneNumber);
        if (user is null) throw new UserNotFoundException();


        var hasherResult = PasswordHasher.Verify(dto.OldPassword, user.Salt, user.PasswordHash);
        if (hasherResult == false) throw new PasswordNotMatchException();

        if (dto.NewPassword == dto.ReturnNewPassword)
        {
            var hasher = PasswordHasher.Hash(dto.NewPassword);
            user.PasswordHash = hasher.Hash;
            user.Salt = hasher.Salt;

            var res = await _userRepository.UpdateAsync(_identity.Id, user);

            return res > 0;
        }

        return false;
    }
}
