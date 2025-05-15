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
    public class CardImageControllerTests
    {
        private readonly CardImageController _cardImageController;
        private readonly Mock<ICardImageService> _cardImageServiceMock;
        public CardImageControllerTests()
        {
            _cardImageServiceMock = new Mock<ICardImageService>();

            _cardImageController = new CardImageController(_cardImageServiceMock.Object);
        }

        [Fact]
        public async Task GetImage_ShouldReturnOk_WithImageUrl()
        {
            //arrange
            var word = "cat";
            var url = "https://prettycoolimageofcat";

            _cardImageServiceMock.Setup(c => c.GetImageUrl(word)).ReturnsAsync(url);

            //act
            var result = await _cardImageController.GetImage(word);

            //assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(url);
        }

        [Fact]
        public async Task GetImage_ShouldReturnNoFound_WhenImageUrlIsNullOrEmpty()
        {
            //arrange
            var word = "cat";
            var url = "https://prettycoolimageofcat";

            _cardImageServiceMock.Setup(c => c.GetImageUrl(word)).ReturnsAsync(String.Empty);

            //act
            var result = await _cardImageController.GetImage(word);

            //assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().Be("No image found for the given word.");
        }

        [Fact]
        public async Task GetImage_ShouldReturn503_WithHttpRequestExceptionThrown()
        {
            // Arrange
            _cardImageServiceMock.Setup(s => s.GetImageUrl("cat"))
                                 .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _cardImageController.GetImage("cat");

            // Assert
            result.Should().BeOfType<ObjectResult>()
                  .Which.StatusCode.Should().Be(503);
        }

        [Fact]
        public async Task GetImage_ShouldReturnNotFound_WhenInvalidOperationExceptionThrown()
        {
            // Arrange
            _cardImageServiceMock.Setup(s => s.GetImageUrl("cat"))
                                 .ThrowsAsync(new InvalidOperationException("Image not found."));

            // Act
            var result = await _cardImageController.GetImage("cat");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.Value.Should().Be("Image not found.");
        }

        [Fact]
        public async Task GetImage_ShouldReturn500_WhenGenericExceptionThrown()
        {
            // Arrange
            _cardImageServiceMock.Setup(s => s.GetImageUrl("cat"))
                                 .ThrowsAsync(new Exception("Unexpected."));

            // Act
            var result = await _cardImageController.GetImage("cat");

            // Assert
            result.Should().BeOfType<ObjectResult>()
                  .Which.StatusCode.Should().Be(500);
        }
    }
}
