using GreenSale.Application.Exceptions;
using GreenSale.Application.Exceptions.Users;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.Persistence.Dtos;
using GreenSale.Persistence.Dtos.Notifications;
using GreenSale.Persistence.Dtos.Security;
using GreenSale.Service.Helpers;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Notifications;
using GreenSale.Service.Service.Notifications;
using Microsoft.Extensions.Caching.Memory;

namespace GreenSale.Service.Service.Auth;

public class AuthServise : IAuthServices
{
    private const int CACHED_FOR_MINUTS_REGISTER = 60;
    private const int CACHED_FOR_MINUTS_VEFICATION = 5;
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private readonly ISmsSender _smsSender;
    private  readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;

    public AuthServise(IMemoryCache memoryCache, IUserRepository userRepository, ISmsSender smsSender)
    {
        this._smsSender = smsSender;
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
    }

    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(UserRegisterDto dto)
    {
        var dbResult = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if(dbResult is not null)
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

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phoneNumber)
    {
        if(_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phoneNumber, out UserRegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = 1234; // than genereted code, at nowdefoukt code
            _memoryCache.Set(phoneNumber, verificationDto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));          

            if(_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phoneNumber,
                out VerificationDto OldverificationDto))
            {
                _memoryCache.Remove(phoneNumber);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phoneNumber, verificationDto,
                TimeSpan.FromMinutes(VERIFICATION_MAXIMUM_ATTEMPTS));

            SmsSenderDto smsSenderDto = new SmsSenderDto();
            smsSenderDto.Title = "Green sale\n";
            smsSenderDto.Content = "Your verification code : " + verificationDto.Code;
            smsSenderDto.Recipent = phoneNumber.Substring(1);
            var result = await _smsSender.SendAsync(smsSenderDto);

            if (result is true)
                return (Result: true, CachedVerificationMinutes: CACHED_FOR_MINUTS_VEFICATION);
            else 
                return (Result: false, CACHED_FOR_MINUTS_VEFICATION: 0);
        }
        else
        {
            throw new ExpiredException();
        }
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
