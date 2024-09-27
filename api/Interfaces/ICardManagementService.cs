using api.Models;

namespace api.Interfaces
{
    public interface ICardManagementService
    {
        Task<bool> HandleCorrectAnswer(int cardId, string userId);
        Task<bool> HandleIncorrectAnswer(int cardId, string userId);
        Task CheckAllCardsStatus();
    }
}
