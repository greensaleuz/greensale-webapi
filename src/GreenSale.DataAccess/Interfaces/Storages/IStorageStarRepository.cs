﻿using GreenSale.Domain.Entites.Storages;

namespace GreenSale.DataAccess.Interfaces.Storages;

public interface IStorageStarRepository : IRepository<StoragePostStars,StoragePostStars>
{
    public Task<long> GetAllPostIdCountAsync(long id);
    public Task<long> GetIdAsync(long userid, long postid);
}
