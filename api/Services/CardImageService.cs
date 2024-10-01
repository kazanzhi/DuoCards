using api.Interfaces;
using api.Models.Unsplash;
using Microsoft.OpenApi.Validations;
using System.Data;

namespace api.Services
{
    public class CardImageService : ICardImageService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CardImageService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }
        public async Task<string> GetImageUrl(string engWord)
        {
            var client = _httpClientFactory.CreateClient();
            var apiKey = _configuration["Unplash:ApiKey"];
            client.DefaultRequestHeaders.Add("Authorization", apiKey);
            var response = await client.GetAsync($"https://api.unsplash.com/photos/random?query={engWord}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to get image.");
            }

            var responseData = await response.Content.ReadFromJsonAsync<UnsplashResponse>();

            return responseData?.Urls?.Small ?? string.Empty;
        }
    }
}
