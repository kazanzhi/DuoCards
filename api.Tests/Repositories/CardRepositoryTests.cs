using api.Data;
using api.Dto;
using api.Interfaces;
using api.Models;
using api.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace api.Tests.Repositories
{
    public class CardRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ICardRepository _cardRepository;
        public CardRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _cardRepository = new CardRepository(_context);
        }

        [Fact]
        public async Task CreateCard_ShouldCreateCard()
        {
            //arrange
            var cardDto = new CardDto
            {
                EngWord = "test",
                RuWord = "тест",
                ExampleOfUsage = "test",
                ImageUrl = "https://testurl"
            };

            string userId = "testId";

            //act
            var result = await _cardRepository.CreateCard(cardDto, userId);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Card>();

            var cardInDb = await _context.Cards.ToListAsync();
            cardInDb.Should().HaveCount(1);
            cardInDb[0].EngWord.Should().Be("test");
            cardInDb[0].RuWord.Should().Be("тест");
            cardInDb[0].ExampleOfUsage.Should().Be("test");
            cardInDb[0].ImgUrl.Should().Be("https://testurl");
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
            var result = await _cardRepository.GetAllCards(userId);

            //assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<Card>>();
            result.Should().HaveCount(3);

            var books = await _context.Cards.ToListAsync();
            books.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetAllCards_ShouldReturnEmptyListOfCards_WhenNoCards()
        {
            //arrange
            string userId = "testId";

            //act
            var result = await _cardRepository.GetAllCards(userId);

            //assert
            result.Should().BeEmpty();
            result.Should().BeOfType<List<Card>>();
        }

        [Fact]
        public async Task UpdateCard_ShouldReturnTrue_WhenCardUpdated()
        {
            //arrange
            var userId = "testId";
            var card = new Card { EngWord = "test", RuWord = "тест", ExampleOfUsage = "test", ImgUrl = "https://testurl", AppUserId = userId };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            var cardDto = new CardDto
            {
                EngWord = "cup",
                RuWord = "чашка",
                ExampleOfUsage = "test cup",
                ImageUrl = "https://testcupurl"
            };

            //act
            var result = await _cardRepository.UpdateCard(cardDto, card.Id, userId);

            //assert
            result.Should().BeTrue();

            var cardResult = await _context.Cards.FirstAsync();
            cardResult.Should().NotBeNull();
            cardResult.EngWord.Should().Be("cup");
            cardResult.RuWord.Should().Be("чашка");
            cardResult.ExampleOfUsage.Should().Be("test cup");
        }

        [Fact]
        public async Task UpdateCard_SHouldReturnFalse_WhenCardNotFound()
        {
            //arrange
            var userId = "testId";
            var cardId = 999;
            var cardDto = new CardDto
            {
                EngWord = "cup",
                RuWord = "чашка",
                ExampleOfUsage = "test cup",
                ImageUrl = "https://testcupurl"
            };

            //act
            var result = await _cardRepository.UpdateCard(cardDto, cardId, userId);

            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetById_ShouldReturnCard()
        {
            //arrange
            var userId = "testId";
            var card = new Card { EngWord = "test", RuWord = "тест", ExampleOfUsage = "test", ImgUrl = "https://testurl", AppUserId = userId };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            //act
            var result = await _cardRepository.GetById(card.Id, userId);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Card>();
            result.EngWord.Should().Be("test");
            result.RuWord.Should().Be("тест");
            result.ExampleOfUsage.Should().Be("test");
            result.ImgUrl.Should().Be("https://testurl");
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenCardNotFound()
        {
            //arrange
            var cardId = 999;
            var userId = "testId";

            //act
            var result = await _cardRepository.GetById(cardId, userId);

            //assert
            result.Should().BeNull();
        }
    }
}
