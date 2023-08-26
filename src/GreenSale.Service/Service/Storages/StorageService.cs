using GreenSale.Application.Exceptions;
using GreenSale.Application.Exceptions.Storages;
using GreenSale.Application.Exceptions.Users;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.ViewModels.Storages;
using GreenSale.Domain.Entites.Storages;
using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Service.Helpers;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.Storages;
using Microsoft.AspNetCore.Http;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace GreenSale.Service.Service.Storages;

public class StorageService : IStoragesService
{
    private IUserRepository _userep;
    private IIdentityService _identity;
    private IStorageRepository _repository;
    private IPaginator _paginator;
    private IFileService _fileService;
    private readonly string STORAGEPOSTIMAGES = "StoragePostImages";

    public StorageService(
        IStorageRepository repository,
        IPaginator paginator,
        IFileService fileService,
        IIdentityService identity,
        IUserRepository userRepository)
    {
        this._userep = userRepository;
        this._identity = identity;
        this._repository = repository;
        this._paginator = paginator;
        this._fileService = fileService;
    }
    public async Task<long> CountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<bool> CreateAsync(StoragCreateDto dto)
    {

        /*string fileBytesPath = @"C:\StoragesDefoult.jpeg"; // Faylning manzili (yo'li)

        byte[] fileBytes = File.ReadAllBytes(fileBytesPath); // Faylni byte massiviga o'qish

        dto.ImagePath = Convert.ToBase64String(fileBytes); // Faylning byte massivini IFromFile tipidagi stringga o'girish*/
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath, STORAGEPOSTIMAGES);
        Storage storage = new Storage()
        {
            UserId = _identity.Id,
            Name = dto.Name,
            Description = dto.Description,
            Region = dto.Region,
            District = dto.District,
            Address = dto.Address,
            Info = dto.Info,
            AddressLatitude = dto.AddressLatitude,
            AddressLongitude = dto.AddressLongitude,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime(),
            ImagePath = imagePath
        };

        var result = await _repository.CreateAsync(storage);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long storageId)
    {
        var storageGet = await _repository.GetByIdAsync(storageId);

        if (storageGet.Id == 0)
            throw new StorageNotFoundException();

        var deleteImage = await _fileService.DeleteImageAsync(storageGet.ImagePath);

        if (deleteImage == false)
            throw new ImageNotFoundException();

        var result = await _repository.DeleteAsync(storageId);

        return result > 0;
    }

    public async Task<List<StoragesViewModel>> GetAllAsync(PaginationParams @params)
    {
        var getAll = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return getAll;
    }

    public async Task<StoragesViewModel> GetBYIdAsync(long storageId)
    {
        var getId = await _repository.GetByIdAsync(storageId);

        if (getId.Id == 0)
            throw new StorageNotFoundException();

        return getId;
    }

    public async Task<bool> UpdateAsync(long storageID, StoragUpdateDto dto)
    {
        var getId = await _repository.GetByIdAsync(storageID);

        if (getId.Id == 0)
            throw new StorageNotFoundException();

        Storage storage = new Storage()
        {
            Name = dto.Name,
            UserId = getId.UserId,
            Address = dto.Address,
            Info = dto.Info,
            Description = dto.Description,
            District = dto.District,
            Region = dto.Region,
            AddressLongitude = dto.AddressLongitude,
            AddressLatitude = dto.AddressLatitude,
            CreatedAt = getId.CreatedAt,
            UpdatedAt = TimeHelper.GetDateTime()
        };

   /*     if (dto.ImagePath is not null)
        {
            //delete old image
            var deleteImage = await _fileService.DeleteImageAsync(getId.ImagePath);


            //upload new image
            string imagePath = await _fileService.UploadImageAsync(dto.ImagePath, STORAGEPOSTIMAGES);
            storage.ImagePath = imagePath;
        }*/

        var result = await _repository.UpdateAsync(storageID, storage);

        return result > 0;
    }

    public async Task<bool> UpdateImageAsync(long storageID, StorageImageUpdateDto dto)
    {
        var DbFound = await _repository.GetByIdAsync(storageID);

        if (DbFound.Id == 0) throw new StorageNotFoundException();

        var img = await _fileService.DeleteImageAsync(DbFound.ImagePath);
        var res = await _fileService.UploadImageAsync(dto.StorageImage, STORAGEPOSTIMAGES);
        DbFound.ImagePath = res;

        Storage storage = new Storage()
        {
            Name = DbFound.FullName.Split(' ')[0],
            UserId = DbFound.UserId,
            Description = DbFound.Description,
            Region = DbFound.Region,
            District = DbFound.District,
            Address = DbFound.Address,
            AddressLatitude = DbFound.AddressLatitude,
            AddressLongitude = DbFound.AddressLongitude,
            Info = DbFound.Info,
            CreatedAt = DbFound.CreatedAt,
            UpdatedAt = TimeHelper.GetDateTime(),
            ImagePath = res
        };

        var Result = await _repository.UpdateAsync(storageID, storage);

        return Result > 0;
    }

    public async Task<List<StoragesViewModel>> GetAllByIdAsync(long userId, PaginationParams @params)
    {
        var userdev = await _userep.GetByIdAsync(userId);
        if (userdev.Id == 0)
            throw new UserNotFoundException();

        var DbFound = await _repository.GetAllByIdAsync(userId, @params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return DbFound;
    }
}
