using GreenSale.Domain.Entites.BuyerPosts;
using GreenSale.Domain.Enums.BuyerPosts;

namespace GreenSale.DataAccess.ViewModels.BuyerPosts
{
    public class BuyerPostViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Capacity { get; set; }
        public string CapacityMeasure { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public BuyerPostEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BuyerPostImage> BuyerPostsImages { get; set; }
    }
}