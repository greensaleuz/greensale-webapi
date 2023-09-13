
using GreenSale.Domain.Entites.BuyerPosts;

namespace GreenSale.DataAccess.Interfaces.BuyerPosts;

public interface IBuyerPostStarRepository : IRepository<BuyerPostStars,BuyerPostStars>
{
    public Task<long> GetAllPostIdCountAsync(long id);
}
