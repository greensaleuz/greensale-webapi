using GreenSale.Application.Utils;
using GreenSale.DataAccess.Common;
using GreenSale.DataAccess.ViewModels.Storages;
using GreenSale.Domain.Entites.Storages;

namespace GreenSale.DataAccess.Interfaces.Storages
{
    public interface IStorageRepository : IRepository<Storage, StoragesViewModel>, ISearchable<StoragesViewModel>
    {
        public Task<List<StoragesViewModel>> GetAllByIdAsync(long userId, PaginationParams @params);
        public Task<List<StoragesViewModel>> GetAllByIdAsync(long userId);
    }
}