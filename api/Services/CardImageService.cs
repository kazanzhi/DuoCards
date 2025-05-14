using api.Interfaces;
using api.Models.Unsplash;

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
            using var client = _httpClientFactory.CreateClient();
            var apiKey = _configuration["Unsplash:ApiKey"];
            client.DefaultRequestHeaders.Add("Authorization", apiKey);
            var response = await client.GetAsync($"https://api.unsplash.com/photos/random?query={engWord}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unsplash API call failed. Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var responseData = await response.Content.ReadFromJsonAsync<UnsplashResponse>();

            if (responseData?.Urls?.Small == null)
            {
                throw new InvalidOperationException("Received an empty image URL from Unsplash.");
            }

            return responseData.Urls.Small;
        }
    }
}
