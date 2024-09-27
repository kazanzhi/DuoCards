using api.Enum;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string RuWord { get; set; }
        public string EngWord{ get; set; }
        public string ExampleOfUsage { get; set; }
        public CardStatus CardStatus { get; set; } //enum for to learn, known, learned
        public string ImgUrl { get; set; }
        public int SuccessfulAttempts { get; set; } //count of correct answers
        public int ReviewCount { get; set; } //count of times when card was in known
        public DateTime NextReviewDate { get; set; } //the card was last reviewed
        public string AppUserId { get; set; }

        [JsonIgnore]
        public AppUser AppUser { get; set; }
    }
}