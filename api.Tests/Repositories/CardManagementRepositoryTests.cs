using api.Data;
using api.Interfaces;
using api.Models;
using api.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace api.Tests.Repositories
{
    public class CardManagementRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ICardManagementRepository _cardManagementRepository;
        public CardManagementRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _cardManagementRepository = new CardManagementRepository(_context);
        }

        [Fact]
        public async Task GetAllCards_ShouldReturnListOfCards()
        {
            //arrange
            string userId = "testId";

            var cards = new List<Card>
            {
                new Card { EngWord = "test", RuWord = "тест", ExampleOfUsage = "test", ImgUrl = "https://testurl", AppUserId = userId },
                new Card { EngWord = "test1", RuWord = "тест1", ExampleOfUsage = "test1", ImgUrl = "https://testurl1", AppUserId = userId },
                new Card { EngWord = "test2", RuWord = "тест2", ExampleOfUsage = "test2", ImgUrl = "https://testurl2", AppUserId = userId }
            };

            _context.Cards.AddRange(cards);
            await _context.SaveChangesAsync();

            //act
            var result = await _cardManagementRepository.GetAllCards();

            //assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<Card>>();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetAllCards_ShouldReturnEmptyList_WhenNoCards()
        {
            //arrange

            //act
            var result = await _cardManagementRepository.GetAllCards();

            //assert
            result.Should().BeEmpty();
            result.Should().BeOfType<List<Card>>();
        }

        [Fact]
        public async Task GetUserCard_ShouldReturnUserCard()
        {
            //arrange
            var userId = "testId";
            var card = new Card { EngWord = "test", RuWord = "тест", ExampleOfUsage = "test", ImgUrl = "https://testurl", AppUserId = userId };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //act
            var result = await _cardManagementRepository.GetUserCard(card.Id, userId);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Card>();
            result.EngWord.Should().Be("test");
            result.RuWord.Should().Be("тест");
            result.ExampleOfUsage.Should().Be("test");
            result.ImgUrl.Should().Be("https://testurl");
        }

        [Fact]
        public async Task GetUserCard_ShouldReturnNull_WhenCardNotFound()
        {
            //arrange
            var cardId = 999;
            var userId = "testId";

            //act
            var result = await _cardManagementRepository.GetUserCard(cardId, userId);

            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateCard_ShouldReturnTrue_WhenCardUpdated()
        {
            // Arrange
            var userId = "testId";
            var card = new Card
            {
                EngWord = "test",
                RuWord = "тест",
                ExampleOfUsage = "test",
                ImgUrl = "https://testurl",
                AppUserId = userId
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            var updatedCard = new Card
            {
                Id = card.Id,
                EngWord = card.EngWord,
                RuWord = card.RuWord,
                ExampleOfUsage = "test ",
                ImgUrl = "https://testurl",
                AppUserId = userId
            };

            // Act
            var result = await _cardManagementRepository.UpdateCard(updatedCard, userId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateCard_SHouldReturnFalse_WhenCardNotFound()
        {
            //arrange
            var userId = "testId";
            var card = new Card
            {
                EngWord = "cup",
                RuWord = "чашка",
                ExampleOfUsage = "test cup",
                ImgUrl = "https://testcupurl"
            };

            //act
            var result = await _cardManagementRepository.UpdateCard(card, userId);

            //assert
            result.Should().BeFalse();
        }
    }
}
