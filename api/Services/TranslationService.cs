﻿using Google.Cloud.Translation.V2;

namespace api.Services
{
    public class TranslationService
    {
        private readonly TranslationClient _translationClient;
        private readonly string _apiKey;
        public TranslationService(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("GoogleCloud:ApiKey");
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("GOOGLE_CLOUD_API_KEY environment variable is not set.");
            }

            _translationClient = TranslationClient.CreateFromApiKey(_apiKey);
        }
        public async Task<string> Translate(string inputText)
        {
            TranslationResult result = await Task.Run(() => _translationClient.TranslateText(inputText, LanguageCodes.Russian));
            return result.TranslatedText;
        }
    }
}
