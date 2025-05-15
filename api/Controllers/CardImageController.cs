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
        [HttpGet("{word}")]
        public async Task<IActionResult> GetImage(string word)
        {
            try
            {
                var imageUrl = await _cardImageService.GetImageUrl(word);

                if (string.IsNullOrWhiteSpace(imageUrl))
                    return NotFound("No image found for the given word.");

                return Ok(imageUrl);
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Image service is unavailable.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
