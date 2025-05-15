using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface ICardManagementRepository
    {
        Task<List<Card>> GetAllCards();
        Task<Card> GetUserCard(int id, string userId);
        Task<bool> UpdateCard(Card card, string userId);
    }
}
