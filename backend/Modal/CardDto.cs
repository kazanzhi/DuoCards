using backend.Enum;
using System.Text.Json.Serialization;

namespace backend.Modal
{
    public class CardDto
    {
        public string EngWord { get; set; } = string.Empty;
        public string ExampleOfUsage { get; set; } = string.Empty;
    }
}
