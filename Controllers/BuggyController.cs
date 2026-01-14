using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skinet.DTOs;
using skinet.Entities;

namespace skinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet]
        [Route("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet]
        [Route("badrequest")]
        public IActionResult GetBadRquest()
        {
            return BadRequest("not a good request");
        }
        [HttpGet]
        [Route("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet]
        [Route("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("this is a server error");
        }
        [HttpPost]
        [Route("validationerror")]
        public IActionResult GetValidationError(CreateProductDto product)
        {
            return Ok();
        }


    }
}
