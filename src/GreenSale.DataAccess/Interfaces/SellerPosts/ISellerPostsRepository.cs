using GreenSale.Application.Utils;
using GreenSale.DataAccess.Common;
using GreenSale.DataAccess.ViewModels.SellerPosts;
using GreenSale.DataAccess.ViewModels.Storages;
using GreenSale.Domain.Entites.SelerPosts;

namespace GreenSale.DataAccess.Interfaces.SellerPosts
{
    public interface ISellerPostsRepository : IRepository<SellerPost, SellerPostViewModel>, ISearchable<SellerPostViewModel>
    {
        public Task<SellerPost> GetIdAsync(long postId);
        public Task<List<SellerPostViewModel>> GetAllByIdAsync(long userId, PaginationParams @params);
        public Task<List<SellerPostViewModel>> GetAllByIdAsync(long userId);
    }
}