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
        [HttpGet("translate/{engWord}")]
        public async Task<ActionResult<string>> Translate(string engWord)
        {
            var translatedWord = await _translationService.Translate(engWord);
            if (string.IsNullOrEmpty(translatedWord))
                return NotFound();

            return Ok(translatedWord);
        }
    }
}
