using api.Data;
using api.Dto;
using api.Interfaces;
using api.Models;
using api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Forms;
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

            var createdBook = await _context.Cards.ToListAsync();
            createdBook.Should().HaveCount(1);
            createdBook[0].EngWord.Should().Be("test");
            createdBook[0].RuWord.Should().Be("тест");
            createdBook[0].ExampleOfUsage.Should().Be("test");
            createdBook[0].ImgUrl.Should().Be("https://testurl");
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
        public async Task UpdateCard_SHouldReturnUpdatedCard()
        {
            //arrange


            //act


            //assert
        }

    }
}
