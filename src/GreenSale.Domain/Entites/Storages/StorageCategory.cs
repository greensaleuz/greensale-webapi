﻿namespace GreenSale.Domain.Entites.Storages
{
    public class StorageCategory : Auditable
    {
        public long CategoryId { get; set; }
        public long StorageId { get; set; }
    }
}
