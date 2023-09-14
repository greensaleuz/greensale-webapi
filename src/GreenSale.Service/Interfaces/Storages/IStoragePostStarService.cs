using GreenSale.Application.Utils;
using GreenSale.Domain.Entites.Storages;
using GreenSale.Persistence.Dtos.SellerPostsDtos;

namespace GreenSale.Service.Interfaces.Storages;

public interface IStoragePostStarService
{
    public Task<int> CreateAsync(SellerPostStarCreateDto dto);
    public Task<int> UpdateAsync(long Id, SellerPostStarUpdateDto dto);
    public Task<long> CountAsync();
    public Task<List<StoragePostStars>> GetAllAsync(PaginationParams @params);
    public Task<StoragePostStars> GetByIdAsync(long Id);
    public Task<int> DeleteAsync(long Id);
}
