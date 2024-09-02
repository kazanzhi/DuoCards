using backend.Modal;

namespace backend.Interfaces
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAllCards();
        //Task<Card> UpdateCard(Card card);
        //Task<Card> DeleteCard(Card card);
        Task<Card> CreateCards(CardDto cardDto);
    }
}
