using api.Models.Unsplash;
using api.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net.Http.Json;
using System.Net;

namespace api.Tests.Services
{
    public class CardImageServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly CardImageService _service;
        public CardImageServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.unsplash.com/")
            };

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(cfg => cfg["Unsplash:ApiKey"]).Returns("Client-ID fake-api-key");

            _service = new CardImageService(_httpClientFactoryMock.Object, _configurationMock.Object);
        }

        private void SetupMockSendAsync(HttpResponseMessage responseMessage)
        {
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);
        }

        [Fact]
        public async Task GetImageUrl_ShouldReturnUrl_WhenResponseIsValid()
        {
            // Arrange
            var unsplashResponse = new UnsplashResponse
            {
                Urls = new Urls { Small = "https://image.com/photo.jpg" }
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(unsplashResponse)
            };

            SetupMockSendAsync(responseMessage);

            // Act
            var result = await _service.GetImageUrl("cat");

            // Assert
            result.Should().Be("https://image.com/photo.jpg");
        }

        [Fact]
        public async Task GetImageUrl_ShouldThrow_WhenApiFails()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            SetupMockSendAsync(responseMessage);

            // Act & Assert
            var act = async () => await _service.GetImageUrl("dog");
            await act.Should().ThrowAsync<HttpRequestException>();
        }

        [Fact]
        public async Task GetImageUrl_ShouldThrow_WhenResponseHasNoUrl()
        {
            // Arrange
            var unsplashResponse = new UnsplashResponse
            {
                Urls = null
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(unsplashResponse)
            };

            SetupMockSendAsync(responseMessage);

            // Act & Assert
            var act = async () => await _service.GetImageUrl("horse");
            await act.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
