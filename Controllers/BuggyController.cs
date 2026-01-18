using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skinet.DTOs;
using skinet.Entities;
using System.Security.Claims;

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
        [HttpGet("secret")]
        [Authorize]
        public IActionResult GetSecret()
        {
            var name=User.FindFirst(ClaimTypes.Name)?.Value;
            var id=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok($"hello {name} with id : {id}");
        }


    }
}
