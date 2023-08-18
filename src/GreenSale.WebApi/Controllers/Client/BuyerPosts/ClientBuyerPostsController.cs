using GreenSale.Persistence.Dtos.BuyerPostsDto;
using GreenSale.Persistence.Validators.BuyerPosts;
using GreenSale.Service.Interfaces.BuyerPosts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.BuyerPosts
{
    [Route("api/client/buyerposts")]
    [ApiController]
    public class ClientBuyerPostsController : BaseClientController
    {
        private IBuyerPostService _service;
        public readonly int maxPage = 30;
        
        public ClientBuyerPostsController(IBuyerPostService service)
        {
            this._service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] BuyerPostCreateDto dto)
        {
            var validator = new BuyerPostCreateValidator();
            var isValidator = validator.Validate(dto);

            if (isValidator.IsValid)
            {
                return Ok(await _service.CreateAsync(dto));
            }

            return BadRequest(isValidator.Errors);
        }

        [HttpPut("buyerpostid")]
        public async Task<IActionResult> UpdateAsync([FromForm] BuyerPostUpdateDto dto, long buyerpostid)
        {
            var validator = new BuyerPostUpdateValidator();
            var isValidator= validator.Validate(dto);
            if (isValidator.IsValid)
            {
                return Ok(await _service.UpdateAsync(buyerpostid,dto));
            }
           
            return BadRequest(isValidator.Errors);
        }

        [HttpDelete("buyerpostid")]
        public async Task<IActionResult> DeleteAsync(long buyerpostid)
            => Ok( await _service.DeleteAsync(buyerpostid));

    }
}
