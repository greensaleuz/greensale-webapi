﻿using GreenSale.Application.Exceptions.Auth;
using GreenSale.Application.Exceptions.Users;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.BuyerPosts;
using GreenSale.DataAccess.Interfaces.Roles;
using GreenSale.DataAccess.Interfaces.SellerPosts;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.ViewModels.Users;
using GreenSale.Persistence.Dtos.UserDtos;
using GreenSale.Persistence.Validators.Users;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.BuyerPosts;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.SellerPosts;
using GreenSale.Service.Interfaces.Storages;
using GreenSale.Service.Interfaces.Users;
using GreenSale.Service.Security;
using GreenSale.Service.Service.BuyerPosts;
using Microsoft.IdentityModel.Tokens;

namespace GreenSale.Service.Service.Users;

public class UserService : IUserService
{
    private readonly IFileService _fileservice;
    private readonly IStorageRepository _storagerepository;
    private readonly IBuyerPostImageRepository _buyerImgrepository;
    private readonly IBuyerPostRepository _buyerRepository;
    private readonly IUserRoles _rolerepository;
    private readonly ISellerPostsRepository _sellerpostrepository;
    private readonly ISellerPostImageRepository _sellerImagerepository;
    private readonly IIdentityService _identity;
    private readonly IPaginator _paginator;
    private readonly IUserRepository _userRepository;
    private readonly IBuyerPostService _buyerPostService;
    private readonly ISellerPostService _sellerPostService;
    private readonly IStoragesService _storagesService;

    public UserService(
        IUserRepository userRepository,
        IPaginator paginator,
        IIdentityService identity,
        ISellerPostsRepository sellerPostsRepository,
        ISellerPostImageRepository sellerPostImageRepository,
        IUserRoles userRoles,
        IBuyerPostRepository buyerPostRepository,
        IBuyerPostService buyerPostService,
        IBuyerPostImageRepository buyerPostImageRepository,
        IStorageRepository storageRepository,
        ISellerPostService sellerPostService,
        IStoragesService storagesService,
        IFileService fileService)
    {
        this._fileservice = fileService;
        this._storagerepository = storageRepository;
        this._buyerImgrepository = buyerPostImageRepository;
        this._buyerRepository = buyerPostRepository;
        this._rolerepository = userRoles;
        this._sellerpostrepository = sellerPostsRepository;
        this._sellerImagerepository = sellerPostImageRepository;
        this._identity = identity;
        this._paginator = paginator;
        this._userRepository = userRepository;
        this._buyerPostService=buyerPostService;
        this._storagesService=storagesService;
        this._sellerPostService = sellerPostService;
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

        var sellerPost = await _sellerpostrepository.GetAllByIdAsync(userId);

        if (sellerPost is not null)
        {
            foreach (var item in sellerPost)
            {
                var delpost = await _sellerPostService.DeleteAsync(item.Id);
            }
        }

        var buyerPost = await _buyerRepository.GetAllByIdAsync(userId);

        if (buyerPost is not null)
        {
            foreach (var item in buyerPost)
            {
                var delpost = await _buyerPostService.DeleteAsync(item.Id);
            }
        }

        var storg = await _storagerepository.GetAllByIdAsync(userId);

        if (storg is not null)
        {
            foreach (var item in storg)
            {
                var del = await _storagesService.DeleteAsync(item.Id);
            }
        }

        var roldel = await _rolerepository.DeleteAsync(userId);
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
