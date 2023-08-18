using GreenSale.Application.Exceptions;
using GreenSale.Application.Exceptions.BuyerPosts;
using GreenSale.Application.Exceptions.SellerPosts;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.BuyerPosts;
using GreenSale.DataAccess.ViewModels.BuyerPosts;
using GreenSale.Domain.Entites.BuyerPosts;
using GreenSale.Persistence.Dtos.BuyerPostImageUpdateDtos;
using GreenSale.Persistence.Dtos.BuyerPostsDto;
using GreenSale.Service.Helpers;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.BuyerPosts;
using GreenSale.Service.Interfaces.Common;

namespace GreenSale.Service.Service.BuyerPosts;

public class BuyerPostService : IBuyerPostService
{
    private readonly IBuyerPostRepository _postRepository;
    private readonly IPaginator _paginator;
    private readonly IFileService _fileService;
    private readonly IBuyerPostImageRepository _imageRepository;
    private readonly IIdentityService _identity;

    public BuyerPostService(
        IBuyerPostRepository postRepository,
        IPaginator paginator,
        IFileService fileService,
        IBuyerPostImageRepository imageRepository,
        IIdentityService identity)
    {
        this._postRepository = postRepository;
        this._paginator = paginator;
        this._fileService = fileService;
        this._imageRepository = imageRepository;
        this._identity = identity;
    }

    public async Task<long> CountAsync()
    {
        var DbResult = await _postRepository.CountAsync();

        return DbResult;
    }

    public async Task<bool> CreateAsync(BuyerPostCreateDto dto)
    {
        BuyerPost buyerPost = new BuyerPost()
        {
            UserId = _identity.Id,
            CategoryID = dto.CategoryID,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Capacity = dto.Capacity,
            CapacityMeasure = dto.CapacityMeasure,
            Type = dto.Type,
            Region = dto.Region,
            Address = dto.Address,
            District = dto.District,
            PhoneNumber = dto.PhoneNumber,
            Status = Domain.Enums.BuyerPosts.BuyerPostEnum.New,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime(),
        };

        var DbResult = await _postRepository.CreateAsync(buyerPost);

        if (DbResult > 0)
        {
            foreach (var item in dto.ImagePath)
            {
                var img = await _fileService.UploadImageAsync(item);

                BuyerPostImage BuyerPostImage = new BuyerPostImage()
                {
                    BuyerpostId = DbResult,
                    ImagePath = img,
                    CreatedAt = TimeHelper.GetDateTime(),
                    UpdatedAt = TimeHelper.GetDateTime(),
                };

                var DbImgResult = await _imageRepository.CreateAsync(BuyerPostImage);
            }

            return true;
        }

        return false;
    }

    public async Task<bool> DeleteAsync(long buyerId)
    {
        var DbFound = await _postRepository.GetByIdAsync(buyerId);

        if (DbFound.Id == 0)
            throw new BuyerPostNotFoundException();

        var DbResult = await _postRepository.DeleteAsync(buyerId);

        return DbResult > 0;
    }

    public async Task<List<BuyerPostViewModel>> GetAllAsync(PaginationParams @params)
    {
        var DbResult = await _postRepository.GetAllAsync(@params);
        var DBCount = await _postRepository.CountAsync();
        _paginator.Paginate(DBCount, @params);

        return DbResult;
    }

    public async Task<BuyerPostViewModel> GetBYIdAsync(long buyerId)
    {
        var DbFound = await _postRepository.GetByIdAsync(buyerId);

        if (DbFound.Id == 0)
            throw new BuyerPostNotFoundException();

        return DbFound;
    }

    public async Task<bool> ImageUpdateAsync(BuyerPostImageDto dto)
    {
        var DbFoundImg = await _imageRepository.GetByIdAsync(dto.BuyerPostImageId);

        if (DbFoundImg is null)
            throw new ImageNotFoundException();

        var RootDEl = await _fileService.DeleteImageAsync(DbFoundImg.ImagePath);
        var img = await _fileService.UploadImageAsync(dto.ImagePath);

        BuyerPostImage buyerPostImage = new BuyerPostImage()
        {
            BuyerpostId = dto.BuyerPostId,
            ImagePath = img,
        };

        var DbResult = await _imageRepository.UpdateAsync(dto.BuyerPostImageId, buyerPostImage);

        return DbResult > 0;
    }

    public async Task<bool> UpdateAsync(long buyerID, BuyerPostUpdateDto dto)
    {
        var DbFound = await _postRepository.GetByIdAsync(buyerID);

        if (DbFound.Id == 0)
            throw new SellerPostsNotFoundException();

        BuyerPost buyerPost = new BuyerPost()
        {
            UserId = _identity.Id,
            CategoryID = dto.CategoryID,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Capacity = dto.Capacity,
            CapacityMeasure = dto.CapacityMeasure,
            Type = dto.Type,
            Region = dto.Region,
            Address = dto.Address,
            District = dto.District,
            PhoneNumber = dto.PhoneNumber,
            Status = dto.Status,
            UpdatedAt = TimeHelper.GetDateTime(),
        };

        var DbResult = await _postRepository.UpdateAsync(buyerID, buyerPost);

        if (DbResult > 0)
            return true;

        return false;
    }
}
