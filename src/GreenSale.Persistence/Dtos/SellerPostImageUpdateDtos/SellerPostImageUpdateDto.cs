using Microsoft.AspNetCore.Http;

namespace GreenSale.Persistence.Dtos.SellerPostImageUpdateDtos;

public class SellerPostImageUpdateDto
{
    public long SellerPostImageId { get; set; }
    public long SellerPostId { get; set; }
    public IFormFile? ImagePath { get; set; }
}
