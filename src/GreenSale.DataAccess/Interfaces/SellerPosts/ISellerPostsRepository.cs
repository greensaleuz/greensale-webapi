using GreenSale.DataAccess.Common;
using GreenSale.DataAccess.ViewModels.SellerPosts;
using GreenSale.Domain.Entites.SelerPosts;

namespace GreenSale.DataAccess.Interfaces.SellerPosts
{
    public interface ISellerPostsRepository : IRepository<SellerPost, SellerPostViewModel>, ISearchable<SellerPostViewModel>
    {}
}