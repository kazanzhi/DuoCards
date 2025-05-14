using api.Interfaces;
using System.Text.Json;

namespace api.Services
{
    public class TranslationService : ITranslationService
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task<string> Translate(string text)
        {
            try
            {
                var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=ru&dt=t&q={Uri.EscapeDataString(text)}";
                var response = await client.GetStringAsync(url);

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("Empty response from translation API.");
                }

                var parsed = JsonSerializer.Deserialize<JsonElement>(response);

                if (parsed.ValueKind != JsonValueKind.Array || parsed[0].ValueKind != JsonValueKind.Array || parsed[0][0].ValueKind != JsonValueKind.Array)
                {
                    throw new Exception("Unexpected response format.");
                }

                var translated = parsed[0][0][0].GetString();
                return translated ?? throw new Exception("Translation not found in response.");
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request failed to the translation API.", e);
            }
            catch (JsonException e)
            {
                throw new Exception("Failed to parse response from translation API.", e);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while translating.", e);
            }
        }
    }
}
