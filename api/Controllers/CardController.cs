using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardManagementService _cardManagementService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICardImageService _cardImageService;

        public CardController(ICardRepository cardRepository, ICardManagementService cardManagementService, UserManager<AppUser> userManager, ICardImageService cardImageService)
        {
            _cardRepository = cardRepository;
            _cardManagementService = cardManagementService;
            _userManager = userManager;
            _cardImageService = cardImageService;
        }

        [Authorize]
        [HttpGet("get")]
        public async Task<ActionResult<List<Card>>> GetAllCards()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null) 
                return Unauthorized();

            var cards = await _cardRepository.GetAllCards(user.Id);
            if(cards == null)
                return NoContent();

            return Ok(cards);
        }

        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Card>> GetById(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var card = await _cardRepository.GetById(id, user.Id);
            if(card == null)
                return NotFound();

            return Ok(card);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<Card>> CreateCard([FromBody] CardDto cardDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
                return Unauthorized();

            var card = await _cardRepository.CreateCard(cardDto, user.Id);
            if (card == null)
                return BadRequest("Card could not be created.");

            return Ok();
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Card>> UpdateCard([FromBody] CardDto cardDto, int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var updatedCard = await _cardRepository.UpdateCard(cardDto, id, user.Id);
            if (!updatedCard)
                return NotFound();

            return Ok();
        }
        
        [Authorize]
        [HttpGet("translate/{engWord}")]
        public async Task<ActionResult<string>> Translate(string engWord)
        {
            var translatedWord = await _cardRepository.TranslateWord(engWord);
            if (string.IsNullOrEmpty(translatedWord))
                return NotFound();

            return Ok(translatedWord);
        }

        [Authorize]
        [HttpPost("{id}/correct")]
        public async Task<ActionResult> CorrectAnswer(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var result = await _cardManagementService.HandleCorrectAnswer(id, user.Id);
            if(result)
                return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpPost("{id}/incorrect")]
        public async Task<ActionResult> IncorrectAnswer(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var result = await _cardManagementService.HandleIncorrectAnswer(id, user.Id);
            if(result)
                return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpGet("get-image/{word}")]
        public async Task<IActionResult> GetImageForWord(string word)
        {
            try
            {
                var imgUrl = await _cardImageService.GetImageUrl(word);
                return Ok(imgUrl);
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 
    }
}
