using Microsoft.AspNetCore.Http;

namespace GreenSale.Persistence.Dtos.BuyerPostImageUpdateDtos;

public class BuyerPostImageDto
{
    public long BuyerPostImageId { get; set; }
    public long BuyerPostId { get; set; }
    public IFormFile? ImagePath { get; set; }
}
