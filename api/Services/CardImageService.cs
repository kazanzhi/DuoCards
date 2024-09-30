using api.Interfaces;
using api.Models.Unsplash;
using Microsoft.OpenApi.Validations;
using System.Data;

namespace api.Services
{
    public class CardImageService : ICardImageService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CardImageService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetImageUrl(string engWord)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Client-ID SlF-DFziPzpfBCnhuZorQPkkf1_jFfeoelOyJtanttI");
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
