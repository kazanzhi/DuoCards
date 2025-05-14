using api.Data;
using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;
        public CardRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Card> CreateCard(CardDto cardDto, string userId)
        {
            if (cardDto == null)
                throw new ArgumentNullException(nameof(cardDto));

            var createdCard = new Card
            {
                EngWord = cardDto.EngWord,
                RuWord = cardDto.RuWord,
                ExampleOfUsage = cardDto.ExampleOfUsage,
                ImgUrl = cardDto.ImageUrl,
                CardStatus = Enum.CardStatus.Learn,
                SuccessfulAttempts = 0,
                ReviewCount = 0,
                NextReviewDate = DateTime.UtcNow,
                AppUserId = userId
            };

            _context.Cards.Add(createdCard);
            await _context.SaveChangesAsync();

            return createdCard;
        }
    
        public async Task<List<Card>> GetAllCards(string userId)
        {
            var cards = await _context.Cards
                .Where(c => c.AppUserId == userId)
                .ToListAsync();

            return cards;
        }

        public async Task<bool> UpdateCard(CardDto cardDto, int id, string userId)
        {
            if (cardDto is null)
                return false;

            var existingCard = await _context.Cards
                .Where(c => c.Id == id && c.AppUserId == userId)
                .FirstOrDefaultAsync();
            
            if(existingCard != null)
            {
                existingCard.EngWord = cardDto.EngWord;
                existingCard.RuWord = cardDto.RuWord;
                existingCard.ExampleOfUsage = cardDto.ExampleOfUsage;
                existingCard.ImgUrl = cardDto.ImageUrl;

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Card> GetById(int id, string userId)
        {
            var card = await _context.Cards
                .Where(c => c.Id == id && c.AppUserId == userId)
                .FirstOrDefaultAsync();

            if (card == null)
                return null;

            return card;
        }
    }
}