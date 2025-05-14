namespace api.Interfaces
{
    public interface ITranslationService
    {
        Task<string> Translate(string engWord);
    }
}
