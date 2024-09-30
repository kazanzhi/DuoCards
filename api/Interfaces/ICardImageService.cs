namespace api.Interfaces
{
    public interface ICardImageService
    {
        Task<string> GetImageUrl(string engWord);
    }
}
