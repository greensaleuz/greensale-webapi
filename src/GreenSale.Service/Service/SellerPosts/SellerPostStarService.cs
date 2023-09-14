using GreenSale.Application.Exceptions.SellerPosts;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.SellerPosts;
using GreenSale.Domain.Entites.SellerPosts;
using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.SellerPosts;

namespace GreenSale.Service.Service.SellerPosts;

public class SellerPostStarService : ISellerPostStarService
{
    public readonly ISellerPostStarRepository _sellerPostStarRepository;
    public readonly IIdentityService _identityService;

    public SellerPostStarService(ISellerPostStarRepository sellerPostStarRepository,
        IIdentityService identityService)
    {
        this._sellerPostStarRepository = sellerPostStarRepository;
        this._identityService = identityService;
    }

    public async Task<long> CountAsync()
        => await _sellerPostStarRepository.CountAsync();

    public async Task<int> CreateAsync(SellerPostStarCreateDto dto)
    {
        SellerPostStars stars = new SellerPostStars();
        stars.UserId = _identityService.Id;
        stars.PostId = dto.PostId;

        var post = await _sellerPostStarRepository.GetByIdAsync(dto.PostId);
        if (post.Id == 0)
        {
            throw new SellerPostsNotFoundException();
        }
        else
        {
            long Id = await GetIdAsync(stars.UserId, stars.PostId);

            if (Id == 0)
            {
                stars.Stars = dto.Stars;
                stars.CreatedAt = Helpers.TimeHelper.GetDateTime();
                stars.UpdatedAt = Helpers.TimeHelper.GetDateTime();

                var result = await _sellerPostStarRepository.CreateAsync(stars);

                return result;
            }
            else
            {
                var starsOld = await _sellerPostStarRepository.GetByIdAsync(Id);

                SellerPostStars starsNew = new SellerPostStars();
                starsNew.UserId = starsOld.UserId;
                starsNew.PostId = starsOld.PostId;
                starsNew.Stars = dto.Stars;
                starsNew.CreatedAt = starsOld.CreatedAt;
                starsNew.UpdatedAt = Helpers.TimeHelper.GetDateTime();

                var result = await _sellerPostStarRepository.UpdateAsync(Id, starsNew);

                return result;
            }
        }
    }

    public async Task<int> DeleteAsync(long postId)
    {
        long UserId = _identityService.Id;
        long Id = await GetIdAsync(UserId, postId);

        var result = await _sellerPostStarRepository.DeleteAsync(Id);

        return result;
    }

    public Task<List<SellerPostStars>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<SellerPostStars> GetByIdAsync(long Id)
    {
        long UserId = _identityService.Id;
        long RowId = await GetIdAsync(UserId, Id);

        var result = await _sellerPostStarRepository.GetByIdAsync(RowId);

        return result;
    }

    public async Task<int> UpdateAsync(long PostId, SellerPostStarUpdateDto dto)
    {
        long UserId = _identityService.Id;
        long Id = await GetIdAsync(UserId, PostId);

        var starsOld = await _sellerPostStarRepository.GetByIdAsync(Id);

        SellerPostStars starsNew = new SellerPostStars();
        starsNew.UserId = starsOld.UserId;
        starsNew.PostId = starsOld.PostId;
        starsNew.Stars = dto.Stars;
        starsNew.CreatedAt = starsOld.CreatedAt;
        starsNew.UpdatedAt = Helpers.TimeHelper.GetDateTime();

        var result = await _sellerPostStarRepository.UpdateAsync(Id, starsNew);

        return result;
    }

    public async Task<long> GetIdAsync(long userid, long postid)
        => await _sellerPostStarRepository.GetIdAsync(userid, postid);
}
