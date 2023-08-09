﻿namespace GreenSale.DataAccess.ViewModels.Users
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool PhoneNuberConfirme { get; set; }
        public string Region { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}