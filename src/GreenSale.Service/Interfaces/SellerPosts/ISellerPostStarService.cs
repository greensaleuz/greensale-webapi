using GreenSale.Application.Utils;
using GreenSale.Domain.Entites.BuyerPosts;
using GreenSale.Persistence.Dtos.SellerPostsDtos;

namespace GreenSale.Service.Interfaces.SellerPosts;

public interface ISellerPostStarService
{
    public Task<int> CreateAsync(SellerPostStarCreateDto dto);
    public Task<int> UpdateAsync(long Id, SellerPostStarUpdateDto dto);
    public Task<long> CountAsync();
    public Task<List<BuyerPostStars>> GetAllAsync(PaginationParams @params);
    public Task<BuyerPostStars> GetByIdAsync(long Id);
    public Task<int> DeleteAsync(long Id);
}
