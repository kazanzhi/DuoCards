using api.Constants;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardProgressController : Controller
    {
        private readonly ICardManagementService _cardManagementService;
        private readonly UserManager<AppUser> _userManager;

        public CardProgressController(ICardManagementService cardManagementService, UserManager<AppUser> userManager)
        {
            _cardManagementService = cardManagementService;
            _userManager = userManager;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost("{id}/correct")]
        public async Task<IActionResult> CorrectAnswer(int cardId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var result = await _cardManagementService.HandleCorrectAnswer(cardId, user.Id);
            if (result)
                return Ok();

            return NotFound();
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost("{id}/incorrect")]
        public async Task<IActionResult> IncorrectAnswer(int cardId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var result = await _cardManagementService.HandleIncorrectAnswer(cardId, user.Id);
            if (result)
                return Ok();

            return NotFound();
        }
    }
}
