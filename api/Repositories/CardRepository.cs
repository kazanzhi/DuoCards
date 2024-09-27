using api.Data;
using api.Dto;
using api.Interfaces;
using api.Models;
using api.Services;
using Google.Apis.Translate.v2;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;
        private readonly TranslationService _translationService;
        public CardRepository(DataContext context, TranslationService translateService)
        {
            _translationService = translateService;
            _context = context;
        }

        public async Task<Card> CreateCard(CardDto cardDto, string userId)  //done
        {
            var createdCard = new Card
            {
                EngWord = cardDto.EngWord,
                RuWord = cardDto.RuWord,
                ExampleOfUsage = cardDto.ExampleOfUsage,
                ImgUrl = "https://i.natgeofe.com/k/6496b566-0510-4e92-84e8-7a0cf04aa505/red-fox-portrait.jpg?w=1084.125&h=721.875",
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
    
        public async Task<List<Card>> GetAllCards(string userId)    //done
        {
            var cards = await _context.Cards
                .Where(c => c.AppUserId == userId)
                .ToListAsync();

            return cards;
        }

        public async Task<bool> UpdateCard(CardDto cardDto, int id, string userId)  //done
        {
            var updatedCard = await _context.Cards
                .Where(c => c.Id == id && c.AppUserId == userId)
                .FirstOrDefaultAsync();
            
            if(updatedCard != null)
            {
                updatedCard.EngWord = cardDto.EngWord;
                updatedCard.RuWord = cardDto.RuWord;
                updatedCard.ExampleOfUsage = cardDto.ExampleOfUsage;


                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Card> GetById(int id, string userId)  //done
        {
            var card = await _context.Cards
                .Where(c => c.Id == id && c.AppUserId == userId)
                .FirstOrDefaultAsync();

            if (card == null)
                return null;

            return card;
        }

        public async Task<string> TranslateWord(string word)    //done
        {
            var translatedWord = await _translationService.Translate(word);
            return translatedWord;
        }
    }
}