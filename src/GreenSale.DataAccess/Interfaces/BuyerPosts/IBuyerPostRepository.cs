using GreenSale.DataAccess.Common;
using GreenSale.DataAccess.ViewModels.BuyerPosts;
using GreenSale.Domain.Entites.BuyerPosts;

namespace GreenSale.DataAccess.Interfaces.BuyerPosts
{
    public interface IBuyerPostRepository : IRepository<BuyerPost, BuyerPostViewModel>, ISearchable<BuyerPostViewModel>
    {}
}