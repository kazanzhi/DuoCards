using api.Constants;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;
        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{engWord}")]
        public async Task<IActionResult> Translate(string engWord)
        {
            try
            {
                var translatedWord = await _translationService.Translate(engWord);

                if (string.IsNullOrEmpty(translatedWord))
                {
                    return NotFound("Translation not found.");
                }

                return Ok(translatedWord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while translating.", error = ex.Message });
            }
        }
    }
}
