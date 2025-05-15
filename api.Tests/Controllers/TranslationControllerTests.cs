using api.Controllers;
using api.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Tests.Controllers
{
    public class TranslationControllerTests
    {
        private readonly Mock<ITranslationService> _translationServiceMock;
        private readonly TranslationController _controller;

        public TranslationControllerTests()
        {
            _translationServiceMock = new Mock<ITranslationService>();
            _controller = new TranslationController(_translationServiceMock.Object);
        }

        [Fact]
        public async Task Translate_ShouldReturnOk_WhenTranslationExists()
        {
            // Arrange
            var translated = "кошка";
            _translationServiceMock.Setup(s => s.Translate("cat"))
                .ReturnsAsync(translated);

            // Act
            var result = await _controller.Translate("cat");

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(translated);
        }

        [Fact]
        public async Task Translate_ShouldReturnNotFound_WhenTranslationIsNull()
        {
            // Arrange
            _translationServiceMock.Setup(s => s.Translate("dog"))
                .ReturnsAsync((string?)null);

            // Act
            var result = await _controller.Translate("dog");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().Be("Translation not found.");
        }

        [Fact]
        public async Task Translate_ShouldReturnNotFound_WhenTranslationIsEmpty()
        {
            // Arrange
            _translationServiceMock.Setup(s => s.Translate("mouse"))
                .ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.Translate("mouse");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().Be("Translation not found.");
        }

        [Fact]
        public async Task Translate_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            _translationServiceMock.Setup(s => s.Translate("error"))
                .ThrowsAsync(new Exception("Some internal error"));

            // Act
            var result = await _controller.Translate("error");

            // Assert
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.ToString().Should().Contain("An error occurred while translating.");
            objectResult.Value.ToString().Should().Contain("Some internal error");
        }
    }
}
