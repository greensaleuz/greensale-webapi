using GreenSale.Application.Utils;
using GreenSale.Domain.Entites.BuyerPosts;
using GreenSale.Persistence.Dtos.BuyerPostsDto;
using static Dapper.SqlMapper;

namespace GreenSale.Service.Interfaces.BuyerPosts;

public interface IBuyerPostStarService
{
    public Task<int> CreateAsync(BuyerPostStarCreateDto dto);
    public Task<int> UpdateAsync(long Id, BuyerPostStarUpdateDto dto);
    public Task<long> CountAsync();
    public Task<List<BuyerPostStars>> GetAllAsync(PaginationParams @params);
    public Task<BuyerPostStars> GetByIdAsync(long Id);
    public Task<int> DeleteAsync(long Id);
}
