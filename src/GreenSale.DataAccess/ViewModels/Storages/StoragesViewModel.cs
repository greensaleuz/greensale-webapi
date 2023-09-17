﻿namespace GreenSale.DataAccess.ViewModels.Storages
{
    public class StoragesViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string StorageName { get; set; } = string.Empty;
        public string StorageCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double AddressLatitude { get; set; }
        public double AddressLongitude { get; set; }
        public string Info { get; set; } = string.Empty;
        public double AverageStars { get; set; }
        public int UserStars { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}