using api.Controllers;
using api.Dto;
using api.Interfaces;
using api.Models;
using api.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace api.Tests.Controllers
{
    public class CardControllerTests
    {
        private readonly CardController _cardController;
        private readonly Mock<ICardRepository> _cardRepositoryMock;
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        public CardControllerTests()
        {
            _userManagerMock = MockUserManager.CreateMockUserManager<AppUser>();
            _cardRepositoryMock = new Mock<ICardRepository>();

            _cardController = new CardController
            (
                _cardRepositoryMock.Object,
                _userManagerMock.Object
            );
        }

        [Fact]
        public async Task GetAllCards_ShouldReturnOk_WithListOfCards()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };

            var cards = new List<Card>
            {
                new Card { EngWord = "test", RuWord = "тест", ExampleOfUsage = "test", ImgUrl = "https://testurl" },
                new Card { EngWord = "test1", RuWord = "тест1", ExampleOfUsage = "test1", ImgUrl = "https://testurl1" },
                new Card { EngWord = "test2", RuWord = "тест2", ExampleOfUsage = "test2", ImgUrl = "https://testurl2"}
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardRepositoryMock.Setup(repo => repo.GetAllCards(testUser.Id)).ReturnsAsync(cards);

            //act
            var result = await _cardController.GetAllCards();

            //assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(cards);
        }

        [Fact]
        public async Task GetAllCards_ShouldReturnNoContent_WhenNoBooks()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardRepositoryMock.Setup(repo => repo.GetAllCards(testUser.Id)).ReturnsAsync(new List<Card>());

            //act
            var result = await _cardController.GetAllCards();

            //assert
            var okResult = result as NoContentResult;
            okResult.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WithCard()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };

            var card = new Card { Id = 999, EngWord = "test", RuWord = "тест", ExampleOfUsage = "test", ImgUrl = "https://testurl" };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardRepositoryMock.Setup(repo => repo.GetById(card.Id, testUser.Id)).ReturnsAsync(card);

            //act
            var result = await _cardController.GetById(card.Id);

            //assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(card);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenCardNotFound()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };

            var cardId = 99;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardRepositoryMock.Setup(repo => repo.GetById(cardId, testUser.Id)).ReturnsAsync((Card?)null);

            //act
            var result = await _cardController.GetById(cardId);

            //assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateCard_ShouldReturnCreatedAtAction()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };

            var cardDto = new CardDto
            {
                EngWord = "test",
                RuWord = "тест",
                ExampleOfUsage = "",
                ImageUrl = "https://testimage"
            };

            var card = new Card
            {
                EngWord = "test",
                RuWord = "тест",
                ExampleOfUsage = "",
                ImgUrl = "https://testimage"
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardRepositoryMock.Setup(repo => repo.CreateCard(cardDto, testUser.Id)).ReturnsAsync(card);

            //act
            var result = await _cardController.CreateCard(cardDto);

            //assert
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.ActionName.Should().Be(nameof(_cardController.GetById));

            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.Value.Should().BeEquivalentTo(card);
        }

        [Fact]
        public async Task UpdateCard_ShouldReturnNoContent_WhenCardUpdated()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };

            var cardDto = new CardDto
            {
                EngWord = "test",
                RuWord = "тест",
                ExampleOfUsage = "",
                ImageUrl = "https://testimage"
            };
            var cardId = 999;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardRepositoryMock.Setup(repo => repo.UpdateCard(cardDto, cardId, testUser.Id)).ReturnsAsync(true);

            //act
            var result = await _cardController.UpdateCard(cardDto, cardId);

            //assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateCard_ShouldReturnNotFound_WhenCardNotFound()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };

            var cardDto = new CardDto
            {
                EngWord = "test",
                RuWord = "тест",
                ExampleOfUsage = "",
                ImageUrl = "https://testimage"
            };
            var cardId = 999;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardRepositoryMock.Setup(repo => repo.UpdateCard(cardDto, cardId, testUser.Id)).ReturnsAsync(false);

            //act
            var result = await _cardController.UpdateCard(cardDto, cardId);

            //assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
