using api.Enum;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class CardManagementService : ICardManagementService
    {
        private readonly ICardManagementRepository _cardManagementRepository;

        public CardManagementService(ICardManagementRepository cardManagementRepository)
        {
            _cardManagementRepository = cardManagementRepository;
        }
        public async Task<bool> HandleCorrectAnswer(int cardId, string userId)
        {
            var card = await _cardManagementRepository.GetUserCard(cardId, userId);

            if(card != null)
            {
                card.SuccessfulAttempts++;

                if (card.SuccessfulAttempts >= 2 && card.CardStatus == CardStatus.Learn)
                {
                    PromoteToKnownStatus(card);
                }

                await CheckCardStatus(card);

                return await _cardManagementRepository.UpdateCard(card, userId);
            }
            return false;
        }

        public async Task<bool> HandleIncorrectAnswer(int cardId, string userId)
        {
            var card = await _cardManagementRepository.GetUserCard(cardId, userId);
            if(card != null)
            {
                if (card.SuccessfulAttempts > 0)
                {
                    card.SuccessfulAttempts--;
                }

                await CheckCardStatus(card);
                return await _cardManagementRepository.UpdateCard(card, userId);
            }

            return false;
        }

        public async Task CheckAllCardsStatus()
        {
            var allCards = await _cardManagementRepository.GetAllCards();

            foreach (var card in allCards)
            {
                await CheckCardStatus(card);
                await _cardManagementRepository.UpdateCard(card, card.AppUserId);
            }
        }

        public async Task CheckCardStatus(Card card)
        {
            if (card.CardStatus == CardStatus.Known && DateTime.Now >= card.NextReviewDate)
            {
                if (card.ReviewCount >= 3)
                {
                    PromoteToLearnedStatus(card);
                }
                else
                {
                    PromoteToLearnStatus(card);
                }
            }
            else if (card.CardStatus == CardStatus.Learned && DateTime.Now >= card.NextReviewDate)
            {
                ResetToLearnStatus(card);
            }
        }

        private void PromoteToKnownStatus(Card card)
        {
            card.CardStatus = CardStatus.Known;
            card.SuccessfulAttempts = 0;
            card.NextReviewDate = DateTime.Now.AddMinutes(5);
            card.ReviewCount++;
        }

        private void PromoteToLearnedStatus(Card card)
        {
            card.CardStatus = CardStatus.Learned;
            card.NextReviewDate = DateTime.Now.AddMinutes(10);
            card.SuccessfulAttempts = 0;
            card.ReviewCount = 0;
        }

        private void PromoteToLearnStatus(Card card)
        {
            card.CardStatus = CardStatus.Learn;
            card.SuccessfulAttempts = 0;
        }

        private void ResetToLearnStatus(Card card)
        {
            card.CardStatus = CardStatus.Learn;
            card.SuccessfulAttempts = 0;
            card.ReviewCount = 0;
        }
    }
}