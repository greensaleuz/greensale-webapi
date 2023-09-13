using GreenSale.Domain.Entites.SellerPosts;

namespace GreenSale.DataAccess.Interfaces.SellerPosts;

public interface ISellerPostStarRepository: IRepository<SellerPostStars, SellerPostStars>
{
    public Task<long> GetAllPostIdCountAsync(long id);
}
