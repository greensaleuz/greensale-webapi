using GreenSale.Application.Exceptions;
using GreenSale.Application.Exceptions.SellerPosts;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.SellerPosts;
using GreenSale.DataAccess.ViewModels.SellerPosts;
using GreenSale.Domain.Entites.SelerPosts;
using GreenSale.Domain.Entites.SellerPosts;
using GreenSale.Persistence.Dtos.SellerPostImageUpdateDtos;
using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Service.Helpers;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.SellerPosts;

namespace GreenSale.Service.Service.SellerPosts;

public class SellerPostService : ISellerPostService
{
    private readonly ISellerPostImageRepository _imageRepository;
    private readonly IFileService _fileservice;
    private readonly IPaginator _paginator;
    private readonly IIdentityService _identity;
    private readonly ISellerPostsRepository _repository;

    public SellerPostService(
        ISellerPostsRepository repository,
        IIdentityService identity,
        IPaginator paginator,
        IFileService fileService,
        ISellerPostImageRepository imageRepository)
    {
        this._imageRepository = imageRepository;
        this._fileservice = fileService;
        this._paginator = paginator;
        this._identity = identity;
        this._repository = repository;
    }
    public async Task<long> CountAsync()
    {
        var DbResult = await _repository.CountAsync();

        return DbResult;
    }

    public async Task<bool> CreateAsync(SellerPostCreateDto dto)
    {
        SellerPost sellerPost = new SellerPost()
        {
            UserId = _identity.Id,
            CategoryId = dto.CategoryId,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Capacity = dto.Capacity,
            CapacityMeasure = dto.CapacityMeasure,
            Type = dto.Type,
            Region = dto.Region,
            District = dto.District,
            PhoneNumber = dto.PhoneNumber,
            Status = Domain.Enums.SellerPosts.SellerPostEnum.Nosold,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime(),
        };

        var DbResult = await _repository.CreateAsync(sellerPost);

        if (DbResult > 0)
        {
            foreach (var item in dto.ImagePath)
            {
                var img = await _fileservice.UploadImageAsync(item);

                SellerPostImage sellerPostImage = new SellerPostImage()
                {
                    SellerPostId = DbResult,
                    ImagePath = img,
                    CreatedAt = TimeHelper.GetDateTime(),
                    UpdatedAt = TimeHelper.GetDateTime(),
                };

                var DbImgResult = await _imageRepository.CreateAsync(sellerPostImage);
            }

            return true;
        }

        return false;
    }

    public async Task<bool> DeleteAsync(long sellerId)
    {
        var DbFound = await _repository.GetByIdAsync(sellerId);

        if (DbFound.Id == 0)
            throw new SellerPostsNotFoundException();

        var Dbresult = await _repository.DeleteAsync(sellerId);

        return Dbresult > 0;
    }

    public async Task<List<SellerPostViewModel>> GetAllAsync(PaginationParams @params)
    {
        var DbResult = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return DbResult;
    }

    public async Task<SellerPostViewModel> GetBYIdAsync(long sellerId)
    {
        var DbFound = await _repository.GetByIdAsync(sellerId);

        if (DbFound.Id == 0)
            throw new SellerPostsNotFoundException();

        return DbFound;
    }

    public async Task<bool> ImageUpdateAsync(SellerPostImageUpdateDto dto)
    {
        var DbFoundImg = await _imageRepository.GetByIdAsync(dto.SellerPostImageId);

        if (DbFoundImg.Id == 0)
            throw new ImageNotFoundException();

        var RootDEl = await _fileservice.DeleteImageAsync(DbFoundImg.ImagePath);
        var img = await _fileservice.UploadImageAsync(dto.ImagePath);

        SellerPostImage sellerPostImage = new SellerPostImage()
        {
            SellerPostId = dto.SellerPostId,
            ImagePath = img,
            UpdatedAt = TimeHelper.GetDateTime(),
            CreatedAt = DbFoundImg.CreatedAt
        };

        sellerPostImage.UpdatedAt = TimeHelper.GetDateTime();
        var DbResult = await _imageRepository.UpdateAsync(dto.SellerPostImageId, sellerPostImage);

        return DbResult > 0;
    }

    public async Task<bool> UpdateAsync(long sellerID, SellerPostUpdateDto dto)
    {
        var DbFound = await _repository.GetByIdAsync(sellerID);

        if (DbFound.Id == 0)
            throw new SellerPostsNotFoundException();

        SellerPost sellerPost = new SellerPost()
        {
            UserId = _identity.Id,
            CategoryId = dto.CategoryId,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Capacity = dto.Capacity,
            CapacityMeasure = dto.CapacityMeasure,
            Type = dto.Type,
            Region = dto.Region,
            District = dto.District,
            PhoneNumber = dto.PhoneNumber,
            Status = Domain.Enums.SellerPosts.SellerPostEnum.Nosold,
            UpdatedAt = TimeHelper.GetDateTime(),
        };

        var DbResult = await _repository.UpdateAsync(sellerID, sellerPost);
        if (DbResult > 0)
            return true;

        return false;
    }
}
