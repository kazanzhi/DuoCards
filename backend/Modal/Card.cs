using backend.Enum;
using System.Text.Json.Serialization;

namespace backend.Modal
{
    public class Card
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string EngWord { get; set; } = string.Empty;
        public string RuWord { get; set; } = string.Empty;
        public string ExampleOfUsage { get; set; } = string.Empty;
        public WordLearningStatus Status { get; set; } = WordLearningStatus.ToLearn;
        public DateTime KnownTime { get; set; }
        public DateTime LearnedTime { get; set; }
    }
}
