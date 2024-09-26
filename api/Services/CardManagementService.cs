using api.Enum;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class CardManagementService : ICardManagementService
    {
        private readonly ICardRepository _cardRepository;

        public CardManagementService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<bool> HandleCorrectAnswer(int cardId)
        {
            var card = await _cardRepository.GetById(cardId);

            if(card != null)
            {
                card.SuccessfulAttempts++;

                if (card.SuccessfulAttempts >= 2 && card.CardStatus == CardStatus.ToLeaern)
                {
                    MoveToKnown(card);
                }

                await CheckCardStatus(card);

                return await _cardRepository.UpdateCardAsync(card);
            }
            return false;
        }

        public async Task<bool> HandleIncorrectAnswer(int cardId)
        {
            var card = await _cardRepository.GetById(cardId);
            if(card != null)
            {
                if (card.SuccessfulAttempts > 0)
                {
                    card.SuccessfulAttempts--;
                }

                await CheckCardStatus(card);
                return await _cardRepository.UpdateCardAsync(card);
            }

            return false;
        }

        public async Task CheckAllCardsStatus()
        {
            var allCards = await _cardRepository.GetAllCards();

            foreach (var card in allCards)
            {
                await CheckCardStatus(card);
                await _cardRepository.UpdateCardAsync(card);
            }
        }

        public async Task CheckCardStatus(Card card)
        {
            if (card.CardStatus == CardStatus.Known && DateTime.Now >= card.NextReviewDate)
            {
                if (card.ReviewCount >= 3)
                {
                    MoveToLearned(card);
                }
                else
                {
                    ResetToToLearn(card);
                }
            }
            else if (card.CardStatus == CardStatus.Learned && DateTime.Now >= card.NextReviewDate)
            {
                ResetToToLearn(card);
            }
        }
        private void MoveToKnown(Card card)
        {
            card.CardStatus = CardStatus.Known;
            card.ReviewCount++;
            card.SuccessfulAttempts = 0;
            card.NextReviewDate = DateTime.Now.AddHours(6);
        }

        private void MoveToLearned(Card card)
        {
            card.CardStatus = CardStatus.Learned;
            card.NextReviewDate = DateTime.Now.AddHours(24);
        }

        private void ResetToToLearn(Card card)
        {
            card.CardStatus = CardStatus.ToLeaern;
            card.ReviewCount = 0;
            card.SuccessfulAttempts = 0;
        }
    }
}
