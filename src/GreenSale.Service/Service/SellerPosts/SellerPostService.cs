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
    private readonly string SELLERPOSTIMAGES = "SellPostImages";

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
                var img = await _fileservice.UploadImageAsync(item, SELLERPOSTIMAGES);

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

    public async Task<bool> DeleteImageIdAsync(long ImageId)
    {
        var DbFound = await _imageRepository.GetByIdAsync(ImageId);

        if (DbFound.Id == 0)
            throw new ImageNotFoundException();
        await _fileservice.DeleteImageAsync(DbFound.ImagePath);
        var Dbresult = await _imageRepository.DeleteAsync(ImageId);

        return Dbresult > 0;
    }

    public async Task<List<SellerPostViewModel>> GetAllAsync(PaginationParams @params)
    {
        var DbResult = await _repository.GetAllAsync(@params);
        var dBim = await _imageRepository.GetFirstAllAsync();

        List<SellerPostViewModel> Result = new List<SellerPostViewModel>();

        foreach (var item in DbResult)
        {
            item.PostImages = new List<SellerPostImage>();

            foreach (var img in dBim)
            {
                if (img.SellerPostId == item.Id)
                {
                    item.PostImages.Add(img);
                    dBim.RemoveAt(0);
                    break;
                }
            }

            Result.Add(item);
        }

        var DBCount = await _repository.CountAsync();
        _paginator.Paginate(DBCount, @params);

        return Result;
    }

    public async Task<List<SellerPostViewModel>> GetAllByIdAsync(long userId, PaginationParams @params)
    {
        var DbResult = await _repository.GetAllByIdAsync(userId, @params);
        var dBim = await _imageRepository.GetFirstAllAsync();

        List<SellerPostViewModel> Result = new List<SellerPostViewModel>();

        foreach (var item in DbResult)
        {
            item.PostImages = new List<SellerPostImage>();

            foreach (var img in dBim)
            {
                if (img.SellerPostId == item.Id)
                {
                    item.PostImages.Add(img);
                    dBim.RemoveAt(0);
                    break;
                }
            }

            Result.Add(item);
        }

        var DBCount = await _repository.CountAsync();
        _paginator.Paginate(DBCount, @params);

        return Result;
    }

    public async Task<SellerPostViewModel> GetBYIdAsync(long sellerId)
    {
        var item = await _repository.GetByIdAsync(sellerId);
        var dBim = await _imageRepository.GetByIdAllAsync(sellerId);

        if (item.Id == 0)
            throw new SellerPostsNotFoundException();

        item.PostImages = new List<SellerPostImage>();

        foreach (var img in dBim)
        {
            if (img.SellerPostId == item.Id)
            {
                item.PostImages.Add(img);
            }
        }

        return item;
    }

    public async Task<bool> ImageUpdateAsync(long posrImageId, SellerPostImageUpdateDto dto)
    {
        var DbFoundImg = await _imageRepository.GetByIdAsync(posrImageId);

        if (DbFoundImg.Id == 0)
            throw new ImageNotFoundException();

        await _fileservice.DeleteImageAsync(DbFoundImg.ImagePath);
        var img = await _fileservice.UploadImageAsync(dto.ImagePath, SELLERPOSTIMAGES);


        DbFoundImg.ImagePath = img;
        DbFoundImg.UpdatedAt = TimeHelper.GetDateTime();

        var DbResult = await _imageRepository.UpdateAsync(posrImageId, DbFoundImg);

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
            Status = DbFound.Status,
            CreatedAt = DbFound.CreatedAt,
            UpdatedAt = TimeHelper.GetDateTime(),
        };

        var DbResult = await _repository.UpdateAsync(sellerID, sellerPost);
        if (DbResult > 0)
            return true;

        return false;
    }

    public async Task<bool> UpdateStatusAsync(long postId, SellerPostStatusUpdateDto dto)
    {
        var DbFound = await _repository.GetIdAsync(postId);

        if (DbFound.Id == 0)
            throw new SellerPostsNotFoundException();

        DbFound.Status = dto.PostStatus;
        DbFound.UpdatedAt = TimeHelper.GetDateTime();

        var DbResult = await _repository.UpdateAsync(postId, DbFound);

        if (DbResult > 0)
            return true;

        return false;
    }
}
