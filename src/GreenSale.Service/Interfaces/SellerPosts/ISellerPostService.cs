using GreenSale.Application.Utils;
using GreenSale.DataAccess.ViewModels.SellerPosts;
using GreenSale.Persistence.Dtos.SellerPostImageUpdateDtos;
using GreenSale.Persistence.Dtos.SellerPostsDtos;

namespace GreenSale.Service.Interfaces.SellerPosts;

public interface ISellerPostService
{
    public Task<bool> CreateAsync(SellerPostCreateDto dto);
    public Task<bool> DeleteAsync(long sellerId);
    public Task<bool> UpdateAsync(long sellerID, SellerPostUpdateDto dto);
    public Task<bool> ImageUpdateAsync(long postImageId, SellerPostImageUpdateDto dto);
    public Task<long> CountAsync();
    public Task<List<SellerPostViewModel>> GetAllAsync(PaginationParams @params);
    public Task<SellerPostViewModel> GetBYIdAsync(long sellerId);
}
