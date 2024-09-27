using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAllCards(string userId);    //done
        Task<Card> GetById(int id, string userId);  //done
        Task<bool> UpdateCard(CardDto cardDto, int id, string userId);  //done
        Task<Card> CreateCard(CardDto card, string userID); //done
        Task<string> TranslateWord(string word);    //done
    }
}