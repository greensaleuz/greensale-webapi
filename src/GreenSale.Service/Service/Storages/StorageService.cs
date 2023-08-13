using GreenSale.Application.Exceptions;
using GreenSale.Application.Exceptions.Storages;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.DataAccess.ViewModels.Storages;
using GreenSale.Domain.Entites.Storages;
using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Service.Helpers;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.Storages;

namespace GreenSale.Service.Service.Storages;

public class StorageService : IStoragesService
{
    private IStorageRepository _repository;
    private IPaginator _paginator;
    private IFileService _fileService;

    public StorageService(
        IStorageRepository repository,
        IPaginator paginator,
        IFileService fileService)
    {
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
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
        Storage storage = new Storage()
        {
            UserId = dto.UserId,
            Name = dto.Name,
            Description = dto.Description,
            Region = dto.Region,
            District = dto.District,
            Address = dto.Address,
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
            Description = dto.Description,
            District = dto.District,
            Region = dto.Region,
            AddressLongitude = dto.AddressLongitude,
            AddressLatitude = dto.AddressLatitude,
            UpdatedAt = TimeHelper.GetDateTime()
        };

        if (dto.ImagePath is not null)
        {
            //delete old image
            var deleteImage = await _fileService.DeleteImageAsync(getId.ImagePath);

            if (deleteImage is false)
                throw new ImageNotFoundException();

            //upload new image
            string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
            storage.ImagePath = imagePath;
        }

        var result = await _repository.UpdateAsync(storageID, storage);

        return result > 0;
    }
}
