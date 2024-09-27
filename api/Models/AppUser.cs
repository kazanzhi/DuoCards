using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
