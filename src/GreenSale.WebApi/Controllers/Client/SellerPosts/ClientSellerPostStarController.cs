using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Service.Interfaces.SellerPosts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.SellerPosts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientSellerPostStarController : BaseClientController
    {
        private readonly ISellerPostStarService _sellerPostStarService;
        public ClientSellerPostStarController(ISellerPostStarService sellerPostStarService)
        {
            _sellerPostStarService = sellerPostStarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(SellerPostStarCreateDto dto)
            => Ok(await _sellerPostStarService.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long postid, SellerPostStarUpdateDto dto)
            => Ok(await _sellerPostStarService.UpdateAsync(postid, dto));
    }
}
