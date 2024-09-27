using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CardManagementRepository : ICardManagementRepository
    {
        private readonly DataContext _context;

        public CardManagementRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Card>> GetAllCards()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task<Card> GetUserCard(int id, string userId)
        {
            var card = await _context.Cards
                .FirstOrDefaultAsync(c => c.Id == id && c.AppUserId == userId);

            return card;
        }

        public async Task<bool> UpdateCard(Card card, string userId)
        {
            var existingCard = await _context.Cards.FirstOrDefaultAsync(c => c.Id == card.Id && c.AppUserId == userId);

            if (existingCard == null)
                return false;

            existingCard.EngWord = card.EngWord;
            existingCard.RuWord = card.RuWord;
            existingCard.ExampleOfUsage = card.ExampleOfUsage;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CorrectAnswer(int id, string userId)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
                return false;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IncorrectAnswer(int id, string userId)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
                return false;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
