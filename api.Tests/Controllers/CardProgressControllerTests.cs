using api.Controllers;
using api.Interfaces;
using api.Models;
using api.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api.Tests.Controllers
{
    public class CardProgressControllerTests
    {
        private readonly CardProgressController _cardProgressController;
        private readonly Mock<ICardManagementService> _cardManagementServiceMock;
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        public CardProgressControllerTests()
        {
            _userManagerMock = MockUserManager.CreateMockUserManager<AppUser>();
            _cardManagementServiceMock = new Mock<ICardManagementService>();

            _cardProgressController = new CardProgressController(
                _cardManagementServiceMock.Object,
                _userManagerMock.Object
            );

        }

        [Fact]
        public async Task CorrectAnswer_ShouldReturnOk_WhenAnswerAdded()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };
            var cardId = 999;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardProgressController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardManagementServiceMock.Setup(s => s.HandleCorrectAnswer(cardId, testUser.Id)).ReturnsAsync(true);

            //act
            var result = await _cardProgressController.CorrectAnswer(cardId);

            //assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task CorrectAnswer_ShouldReturnNoFound_WhenCardNotFound()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };
            var cardId = 999;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardProgressController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardManagementServiceMock.Setup(s => s.HandleCorrectAnswer(cardId, testUser.Id)).ReturnsAsync(false);

            //act
            var result = await _cardProgressController.CorrectAnswer(cardId);

            //assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task IncorrectAnswer_ShouldReturnOk_WhenAnswerAdded()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };
            var cardId = 999;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardProgressController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardManagementServiceMock.Setup(s => s.HandleIncorrectAnswer(cardId, testUser.Id)).ReturnsAsync(true);

            //act
            var result = await _cardProgressController.IncorrectAnswer(cardId);

            //assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task IncorrectAnswer_ShouldReturnNoFound_WhenCardNotFound()
        {
            //arrange
            var testUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com"
            };
            var cardId = 999;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUser.Id)
            }));

            _cardProgressController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(testUser);
            _cardManagementServiceMock.Setup(s => s.HandleIncorrectAnswer(cardId, testUser.Id)).ReturnsAsync(false);

            //act
            var result = await _cardProgressController.IncorrectAnswer(cardId);

            //assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
