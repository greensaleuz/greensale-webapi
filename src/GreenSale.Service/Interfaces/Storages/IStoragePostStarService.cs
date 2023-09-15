using GreenSale.Application.Utils;
using GreenSale.Domain.Entites.Storages;
using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Persistence.Dtos.StoragDtos;

namespace GreenSale.Service.Interfaces.Storages;

public interface IStoragePostStarService
{
    public Task<int> CreateAsync(StorageStarCreateDto dto);
    public Task<int> UpdateAsync(long PostId, StorageStarUpdateDto dto);
    public Task<long> CountAsync();
    public Task<List<StoragePostStars>> GetAllAsync(PaginationParams @params);
    public Task<StoragePostStars> GetByIdAsync(long Id);
    public Task<int> DeleteAsync(long Id);
    public Task<double> AvarageStarAsync(long postid);
    public Task<int> GetUserStarAsync(long postId);
}
