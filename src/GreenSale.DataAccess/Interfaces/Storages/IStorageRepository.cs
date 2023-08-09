using GreenSale.DataAccess.Common;
using GreenSale.DataAccess.ViewModels.Storages;
using GreenSale.Domain.Entites.Storages;

namespace GreenSale.DataAccess.Interfaces.Storages
{
    public interface IStorageRepository : IRepository<Storage, StoragesViewModel>, ISearchable<StoragesViewModel>
    {}
}