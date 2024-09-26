using api.Dto;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardManagementService _cardManagementService;
        public CardController(ICardRepository cardRepository, ICardManagementService cardManagementService)
        {
            _cardRepository = cardRepository;
            _cardManagementService = cardManagementService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<Card>>> GetAllCards()
        {
            var cards = await _cardRepository.GetAllCards();
            if(cards.Count == 0 || cards == null)
                return NoContent();

            return Ok(cards);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Card>> GetById(int id)
        {
            var card = await _cardRepository.GetById(id);
            if(card == null)
                return NoContent();

            return Ok(card);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Card>> CreateCard([FromBody] CardDto cardDto)
        {
            var card = await _cardRepository.CreateCard(cardDto);
            if (card == null)
                return BadRequest("Card could not be created.");

            return Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Card>> UpdateCard([FromBody] CardDto cardDto, int id)
        {
            var updatedCard = await _cardRepository.UpdateCard(cardDto, id);
            if (updatedCard == false)
                return NotFound();
            
            return Ok(updatedCard);
        }

        [HttpGet("translate/{engWord}")]
        public async Task<ActionResult<string>> Translate(string engWord)
        {
            var translatedWord = await _cardRepository.TranslateWord(engWord);
            if (string.IsNullOrEmpty(translatedWord))
                return NotFound();

            return Ok(translatedWord);
        }

        [HttpPost("{id}/correct")]
        public async Task<ActionResult> CorrectAnswer(int id)
        {
            var result = await _cardManagementService.HandleCorrectAnswer(id);
            if(result)
                return Ok();

            return BadRequest();
        }

        [HttpPost("{id}/incorrect")]
        public async Task<ActionResult> IncorrectAnswer(int id)
        {
            var result = await _cardManagementService.HandleIncorrectAnswer(id);
            if(result)
                return Ok();

            return BadRequest();
        }
    }
}
