using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class BuggyController : BaseController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotDound()
        {
            return NotFound();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("unautorized")]
        public ActionResult GetUnautorized() 
        { 
            return Unauthorized();
        }

        [HttpGet("validation-error")]
        public ActionResult GetValidationError() 
        {
            ModelState.AddModelError("Propblem1", "This is the first propblem");
            return ValidationProblem();
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is server error");
        }
    }
}
