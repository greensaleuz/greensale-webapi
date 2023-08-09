using GreenSale.Application.Utils;
using GreenSale.DataAccess.ViewModels.BuyerPosts;
using GreenSale.Domain.Entites.Categories;
using GreenSale.Persistence.Dtos.BuyerPostsDto;
using GreenSale.Persistence.Dtos.CategoryDtos;

namespace GreenSale.Service.Interfaces.BuyerPosts;

public interface IBuyerPostService
{
    public Task<bool> CreateAsync(BuyerPostCreateDto dto);
    public Task<bool> DeleteAsync(long buyerId);
    public Task<bool> UpdateAsync(long buyerID, BuyerPostUpdateDto dto);
    public Task<long> CountAsync();
    public Task<List<BuyerPostViewModel>> GetAllAsync(PaginationParams @params);
    public Task<BuyerPostViewModel> GetBYIdAsync(long buyerId);
}
