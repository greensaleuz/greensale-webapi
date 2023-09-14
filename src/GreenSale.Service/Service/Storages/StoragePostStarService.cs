using GreenSale.Application.Exceptions.Storages;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.Domain.Entites.Storages;
using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Storages;

namespace GreenSale.Service.Service.Storages;

public class StoragePostStarService : IStoragePostStarService
{
    public readonly IStorageStarRepository _storageStarRepository;
    public readonly IStorageRepository _storageRepository;
    public readonly IIdentityService _identityService;

    public StoragePostStarService(IStorageStarRepository storageStarRepository,
        IStorageRepository storageRepository,
        IIdentityService identityService)
    {
        this._storageStarRepository = storageStarRepository;
        this._storageRepository = storageRepository;
        this._identityService = identityService;
    }

    public async Task<long> CountAsync()
        => await _storageStarRepository.CountAsync();

    public async Task<int> CreateAsync(StorageStarCreateDto dto)
    {
        StoragePostStars stars = new StoragePostStars();
        stars.UserId = _identityService.Id;
        stars.PostId = dto.PostId;

        var post = await _storageRepository.GetByIdAsync(dto.PostId);
        if (post.Id == 0)
        {
            throw new StorageNotFoundException();
        }
        else
        {
            long Id = await GetIdAsync(stars.UserId, stars.PostId);

            if (Id == 0)
            {
                stars.Stars = dto.Stars;
                stars.CreatedAt = Helpers.TimeHelper.GetDateTime();
                stars.UpdatedAt = Helpers.TimeHelper.GetDateTime();

                var result = await _storageStarRepository.CreateAsync(stars);

                return result;
            }
            else
            {
                var starsOld = await _storageStarRepository.GetByIdAsync(Id);

                StoragePostStars starsNew = new StoragePostStars();
                starsNew.UserId = starsOld.UserId;
                starsNew.PostId = starsOld.PostId;
                starsNew.Stars = dto.Stars;
                starsNew.CreatedAt = starsOld.CreatedAt;
                starsNew.UpdatedAt = Helpers.TimeHelper.GetDateTime();

                var result = await _storageStarRepository.UpdateAsync(Id, starsNew);

                return result;
            }
        }
    }

    public async Task<int> DeleteAsync(long postId)
    {
        long UserId = _identityService.Id;
        long Id = await GetIdAsync(UserId, postId);

        var result = await _storageStarRepository.DeleteAsync(Id);

        return result;
    }

    public Task<List<StoragePostStars>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<StoragePostStars> GetByIdAsync(long Id)
    {
        long UserId = _identityService.Id;
        long RowId = await GetIdAsync(UserId, Id);

        var result = await _storageStarRepository.GetByIdAsync(RowId);

        return result;
    }

    public async Task<int> UpdateAsync(long PostId, StorageStarUpdateDto dto)
    {
        long UserId = _identityService.Id;
        long Id = await GetIdAsync(UserId, PostId);

        var starsOld = await _storageStarRepository.GetByIdAsync(Id);

        StoragePostStars starsNew = new StoragePostStars();
        starsNew.UserId = starsOld.UserId;
        starsNew.PostId = starsOld.PostId;
        starsNew.Stars = dto.Stars;
        starsNew.CreatedAt = starsOld.CreatedAt;
        starsNew.UpdatedAt = Helpers.TimeHelper.GetDateTime();

        var result = await _storageStarRepository.UpdateAsync(Id, starsNew);

        return result;
    }

    public async Task<long> GetIdAsync(long userid, long postid)
        => await _storageStarRepository.GetIdAsync(userid, postid);
}
