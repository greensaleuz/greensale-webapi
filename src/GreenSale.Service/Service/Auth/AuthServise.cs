using GreenSale.Application.Exceptions.Users;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.Persistence.Dtos;
using GreenSale.Service.Interfaces.Auth;
using Microsoft.Extensions.Caching.Memory;

namespace GreenSale.Service.Service.Auth;

public class AuthServise : IAuthServices
{
    private const int CACHED_FOR_MINUTS_REGISTER = 60;
    private const int CACHED_FOR_MINUTS_VEFICATION = 5;

    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    private  readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;

    public AuthServise(IMemoryCache memoryCache, IUserRepository userRepository)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(UserRegisterDto dto)
    {
        var dbResult = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if(dbResult is null)
            throw new UserAlreadyExistsException();

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out UserRegisterDto registerDto))
        {
            registerDto.PhoneNumber = registerDto.PhoneNumber;
            _memoryCache.Remove(dto.PhoneNumber);
        }
        else
        {
            _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto, TimeSpan.FromMinutes
                (CACHED_FOR_MINUTS_REGISTER));
        }
        return (Result: true, CachedMinutes: CACHED_FOR_MINUTS_REGISTER);
    }

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phoneNumber, int code)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> LoginAsync(UserLoginDto dto)
    {
        throw new NotImplementedException();
    }
}
