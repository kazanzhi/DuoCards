using backend.Data;
using backend.Interfaces;
using backend.Modal;
using backend.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;
        private readonly TranslationService _translationService;

        public CardRepository(DataContext context, TranslationService translationService)
        {
            _context = context;
            _translationService = translationService;
        }
        public async Task<Card> CreateCards(CardDto cardDto)
        {
            string ruWord = _translationService.Translate(cardDto.EngWord);

            var newCard = new Card
            {
                ImgUrl= "",
                EngWord = cardDto.EngWord,
                RuWord = ruWord,
                ExampleOfUsage = cardDto.ExampleOfUsage
            };
            _context.Cards.Add(newCard); 

            await _context.SaveChangesAsync();

            return newCard;
        }

        public async Task<List<Card>> GetAllCards()
        {
            var cards = await _context.Cards.ToListAsync();
            return cards;
        }
    }
}
