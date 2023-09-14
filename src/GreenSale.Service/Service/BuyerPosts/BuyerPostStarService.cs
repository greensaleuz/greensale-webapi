using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.BuyerPosts;
using GreenSale.DataAccess.Repositories.BuyerPosts;
using GreenSale.Domain.Entites.BuyerPosts;
using GreenSale.Persistence.Dtos.BuyerPostsDto;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.BuyerPosts;
using System.Diagnostics.Eventing.Reader;

namespace GreenSale.Service.Service.BuyerPosts;

public class BuyerPostStarService : IBuyerPostStarService
{
    public readonly IBuyerPostStarRepository _buyerPostStarRepository;
    public readonly IIdentityService _identityService;

    public BuyerPostStarService(IBuyerPostStarRepository buyerPostStarRepository,
        IIdentityService identityService)
    {
        this._buyerPostStarRepository = buyerPostStarRepository;
        this._identityService = identityService;
    }

    public async Task<long> CountAsync()
        => await _buyerPostStarRepository.CountAsync();

    public async Task<int> CreateAsync(BuyerPostStarCreateDto dto)
    {
        BuyerPostStars stars = new BuyerPostStars();
        stars.UserId = _identityService.Id;
        stars.PostId =dto.PostId;

        long Id = await GetIdAsync(stars.UserId, stars.PostId);

        if (Id == 0)
        {
            stars.Stars = dto.Stars;
            stars.CreatedAt = Helpers.TimeHelper.GetDateTime();
            stars.UpdatedAt = Helpers.TimeHelper.GetDateTime();

            var result = await _buyerPostStarRepository.CreateAsync(stars);

            return result;
        }
        else
        {
            var starsOld = await _buyerPostStarRepository.GetByIdAsync(Id);

            BuyerPostStars starsNew = new BuyerPostStars();
            starsNew.UserId = starsOld.UserId;
            starsNew.PostId = starsOld.PostId;
            starsNew.Stars = dto.Stars;
            starsNew.CreatedAt = starsOld.CreatedAt;
            starsNew.UpdatedAt = Helpers.TimeHelper.GetDateTime();

            var result = await _buyerPostStarRepository.UpdateAsync(Id, starsNew);

            return result;
        }
    }

    public async Task<int> DeleteAsync(long postId)
    {
        long UserId=_identityService.Id;
        long Id = await GetIdAsync(UserId, postId);

        var result = await _buyerPostStarRepository.DeleteAsync(Id);

        return result;
    }

    public Task<List<BuyerPostStars>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<BuyerPostStars> GetByIdAsync(long Id)
    {
        long UserId = _identityService.Id;
        long RowId = await GetIdAsync(UserId, Id);

        var result = await _buyerPostStarRepository.GetByIdAsync(RowId);

        return result;
    }

    public async Task<int> UpdateAsync(long PostId, BuyerPostStarUpdateDto dto)
    {
        long UserId=_identityService.Id;
        long Id=await GetIdAsync(UserId, PostId);

        var starsOld = await _buyerPostStarRepository.GetByIdAsync(Id);

        BuyerPostStars starsNew = new BuyerPostStars();
        starsNew.UserId = starsOld.UserId;
        starsNew.PostId = starsOld.PostId;
        starsNew.Stars = dto.Stars;
        starsNew.CreatedAt = starsOld.CreatedAt;
        starsNew.UpdatedAt = Helpers.TimeHelper.GetDateTime();

        var result =await _buyerPostStarRepository.UpdateAsync(Id,starsNew);

        return result;
    }

    public async Task<long> GetIdAsync(long userid, long postid)
        => await _buyerPostStarRepository.GetIdAsync(userid, postid);
}
