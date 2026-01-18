using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skinet.DTOs;
using skinet.Entities;
using skinet.Extension;
using System.Security.Claims;

namespace skinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<AppUser> signInManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };
            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem(ModelState);
            }
            return Ok();

        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return NoContent();
            }
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(
                    new
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Address= user.Address != null ? user.Address.toDto() : null
                    });
        }
        [HttpGet]
        public ActionResult GetAuthState()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            });
        }
        [Authorize]
        [HttpPost("address")]
        public async Task<ActionResult<Address>>CreateOrUpdateAddress(AddressDto addressDto)
        {
            var user =await signInManager.UserManager.GetUserByEmailWithAddress(User);
            if (user.Address == null)
            {
                user.Address = addressDto.toEntity();
            }
            else
            {
                user.Address.updateEntity(addressDto);
            }
            var result=await signInManager.UserManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return BadRequest("have problem with update address");
            }
            return Ok(user.Address.toDto());
        }
    }
}
