using api.Enum;
using api.Interfaces;
using api.Models;
using api.Services;
using FluentAssertions;
using Moq;

namespace api.Tests.Services
{
    public class CardManagementServiceTests
    {
        private readonly Mock<ICardManagementRepository> _repositoryMock;
        private readonly CardManagementService _service;

        public CardManagementServiceTests()
        {
            _repositoryMock = new Mock<ICardManagementRepository>();
            _service = new CardManagementService(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleCorrectAnswer_ShouldPromoteToKnown_WhenTwoAttempts()
        {
            // Arrange
            var card = new Card
            {
                Id = 1,
                AppUserId = "user1",
                CardStatus = CardStatus.Learn,
                SuccessfulAttempts = 1,
                ReviewCount = 0
            };

            _repositoryMock.Setup(r => r.GetUserCard(1, "user1")).ReturnsAsync(card);
            _repositoryMock.Setup(r => r.UpdateCard(card, "user1")).ReturnsAsync(true);

            // Act
            var result = await _service.HandleCorrectAnswer(1, "user1");

            // Assert
            result.Should().BeTrue();
            card.CardStatus.Should().Be(CardStatus.Known);
            card.SuccessfulAttempts.Should().Be(0);
            card.ReviewCount.Should().Be(1);
            card.NextReviewDate.Should().BeAfter(DateTime.UtcNow);
        }

        [Fact]
        public async Task HandleCorrectAnswer_ShouldJustIncrementAttempts_WhenLessThanTwo()
        {
            // Arrange
            var card = new Card
            {
                Id = 2,
                AppUserId = "user2",
                CardStatus = CardStatus.Learn,
                SuccessfulAttempts = 0
            };

            _repositoryMock.Setup(r => r.GetUserCard(2, "user2")).ReturnsAsync(card);
            _repositoryMock.Setup(r => r.UpdateCard(card, "user2")).ReturnsAsync(true);

            // Act
            var result = await _service.HandleCorrectAnswer(2, "user2");

            // Assert
            result.Should().BeTrue();
            card.SuccessfulAttempts.Should().Be(1);
            card.CardStatus.Should().Be(CardStatus.Learn);
        }

        [Fact]
        public async Task HandleIncorrectAnswer_ShouldDecrementAttempts_WhenGreaterThanZero()
        {
            var card = new Card
            {
                Id = 3,
                AppUserId = "user3",
                SuccessfulAttempts = 2
            };

            _repositoryMock.Setup(r => r.GetUserCard(3, "user3")).ReturnsAsync(card);
            _repositoryMock.Setup(r => r.UpdateCard(card, "user3")).ReturnsAsync(true);

            var result = await _service.HandleIncorrectAnswer(3, "user3");

            result.Should().BeTrue();
            card.SuccessfulAttempts.Should().Be(1);
        }

        [Fact]
        public async Task HandleIncorrectAnswer_ShouldNotGoBelowZero()
        {
            var card = new Card
            {
                Id = 4,
                AppUserId = "user4",
                SuccessfulAttempts = 0
            };

            _repositoryMock.Setup(r => r.GetUserCard(4, "user4")).ReturnsAsync(card);
            _repositoryMock.Setup(r => r.UpdateCard(card, "user4")).ReturnsAsync(true);

            var result = await _service.HandleIncorrectAnswer(4, "user4");

            result.Should().BeTrue();
            card.SuccessfulAttempts.Should().Be(0);
        }

        [Fact]
        public async Task CheckCardStatus_ShouldPromoteToLearned_WhenKnownAndReviewedThreeTimes()
        {
            var card = new Card
            {
                CardStatus = CardStatus.Known,
                ReviewCount = 3,
                NextReviewDate = DateTime.UtcNow.AddSeconds(-1) // trigger time
            };

            await _service.CheckCardStatus(card);

            card.CardStatus.Should().Be(CardStatus.Learned);
            card.ReviewCount.Should().Be(0);
            card.SuccessfulAttempts.Should().Be(0);
        }

        [Fact]
        public async Task CheckCardStatus_ShouldPromoteToLearn_WhenKnownAndReviewCountLessThan3()
        {
            var card = new Card
            {
                CardStatus = CardStatus.Known,
                ReviewCount = 1,
                NextReviewDate = DateTime.UtcNow.AddSeconds(-1)
            };

            await _service.CheckCardStatus(card);

            card.CardStatus.Should().Be(CardStatus.Learn);
            card.SuccessfulAttempts.Should().Be(0);
        }

        [Fact]
        public async Task CheckCardStatus_ShouldResetToLearn_WhenLearnedAndDue()
        {
            var card = new Card
            {
                CardStatus = CardStatus.Learned,
                NextReviewDate = DateTime.UtcNow.AddSeconds(-1),
                SuccessfulAttempts = 2,
                ReviewCount = 2
            };

            await _service.CheckCardStatus(card);

            card.CardStatus.Should().Be(CardStatus.Learn);
            card.SuccessfulAttempts.Should().Be(0);
            card.ReviewCount.Should().Be(0);
        }

        [Fact]
        public async Task CheckAllCardsStatus_ShouldUpdateEachCard()
        {
            var card1 = new Card
            {
                CardStatus = CardStatus.Learned,
                NextReviewDate = DateTime.UtcNow.AddSeconds(-1),
                AppUserId = "u1"
            };

            var card2 = new Card
            {
                CardStatus = CardStatus.Known,
                ReviewCount = 3,
                NextReviewDate = DateTime.UtcNow.AddSeconds(-1),
                AppUserId = "u2"
            };

            var cards = new List<Card> { card1, card2 };

            _repositoryMock.Setup(r => r.GetAllCards()).ReturnsAsync(cards);
            _repositoryMock.Setup(r => r.UpdateCard(It.IsAny<Card>(), It.IsAny<string>())).ReturnsAsync(true);

            await _service.CheckAllCardsStatus();

            _repositoryMock.Verify(r => r.UpdateCard(It.IsAny<Card>(), It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
