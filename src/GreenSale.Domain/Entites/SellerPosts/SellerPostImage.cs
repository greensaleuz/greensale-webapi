namespace GreenSale.Domain.Entites.SellerPosts
{
    public class SellerPostImage : Auditable
    {
        public long SallerPostId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
