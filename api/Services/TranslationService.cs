using api.Interfaces;
using System.Text.Json;

namespace api.Services
{
    public class TranslationService : ITranslationService
    {
        public async Task<string> Translate(string text)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=ru&dt=t&q={Uri.EscapeDataString(text)}";

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            var parsed = JsonSerializer.Deserialize<JsonElement>(response);
            var translated = parsed[0][0][0].GetString();

            return translated;
        }
    }
}
