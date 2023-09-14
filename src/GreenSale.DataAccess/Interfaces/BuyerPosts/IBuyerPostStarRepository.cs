
using GreenSale.Domain.Entites.BuyerPosts;

namespace GreenSale.DataAccess.Interfaces.BuyerPosts;

public interface IBuyerPostStarRepository : IRepository<BuyerPostStars,BuyerPostStars>
{
    public Task<List<long>> GetAllStarsPostIdCountAsync(long postid);
    public Task<long> GetIdAsync(long userid, long postid);
}
