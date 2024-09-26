namespace api.Interfaces
{
    public interface ICardManagementService
    {
        Task<bool> HandleCorrectAnswer(int cardId);
        Task<bool> HandleIncorrectAnswer(int cardId);
    }
}
