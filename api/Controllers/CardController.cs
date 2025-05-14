using api.Constants;
using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardManagementService _cardManagementService;
        private readonly UserManager<AppUser> _userManager;
        

        public CardController(ICardRepository cardRepository, ICardManagementService cardManagementService, UserManager<AppUser> userManager)
        {
            _cardRepository = cardRepository;
            _cardManagementService = cardManagementService;
            _userManager = userManager;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("get")]
        public async Task<ActionResult<List<Card>>> GetAllCards()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null) 
                return Unauthorized();

            var cards = await _cardRepository.GetAllCards(user.Id);
            if(!cards.Any())
                return NoContent();

            return Ok(cards);
        }

        [Authorize(Roles = UserRoles.User)]
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

        [Authorize(Roles = UserRoles.User)]
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

        [Authorize(Roles = UserRoles.User)]
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
        

        [Authorize(Roles = UserRoles.User)]
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

        [Authorize(Roles = UserRoles.User)]
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
    }
}
