using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAllCards();
        Task<Card> GetById(int id);
        Task<bool> UpdateCard(CardDto cardDto, int id);
        Task<bool> UpdateCardAsync(Card card);
        Task<Card> CreateCard(CardDto card);
        Task<string> TranslateWord(string word);
    }
}