using backend.Interfaces;
using backend.Modal;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class CardController : BaseController
    {
        private readonly ICardRepository _cardRepository;

        public CardController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var cards = await _cardRepository.GetAllCards();
            return Ok(cards);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CardDto card)
        {
            var newCard = await _cardRepository.CreateCards(card);
            return Ok("Succesed");
        }
    }
}
