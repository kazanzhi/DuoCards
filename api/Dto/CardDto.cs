using api.Enum;
using System.Text.Json.Serialization;

namespace api.Dto
{
    public class CardDto
    {
        public string RuWord { get; set; }
        public string EngWord { get; set; }
        public string ExampleOfUsage { get; set; }
        public string ImageUrl { get; set; }
    }
}
