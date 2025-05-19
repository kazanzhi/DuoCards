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
        private readonly UserManager<AppUser> _userManager;
        public CardController(ICardRepository cardRepository, UserManager<AppUser> userManager)
        {
            _cardRepository = cardRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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
        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
                return Unauthorized();

            var card = await _cardRepository.CreateCard(cardDto, user.Id);

            return CreatedAtAction(nameof(GetById), new { id = card.Id }, card);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard([FromBody] CardDto cardDto, int cardId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var updatedCard = await _cardRepository.UpdateCard(cardDto, cardId, user.Id);
            if (!updatedCard)
                return NotFound();

            return NoContent();
        }
    }
}
