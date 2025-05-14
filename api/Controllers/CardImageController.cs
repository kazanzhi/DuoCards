using api.Constants;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardImageController : ControllerBase
    {
        private readonly ICardImageService _cardImageService;
        public CardImageController(ICardImageService cardImageService)
        {
            _cardImageService = cardImageService;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("get-image/{word}")]
        public async Task<IActionResult> GetImageForWord(string word)
        {
            try
            {
                var imgUrl = await _cardImageService.GetImageUrl(word);
                return Ok(imgUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
