using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skinet.Entities;
using skinet.Interfaces;

namespace skinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(ICartService cartService) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Cart?>> GetCartById([FromRoute]string id)
        {
            var cart = await cartService.GetCartAsync(id);
            return cart ?? new Cart { Id = id };
        }
        [HttpPost]
        public async Task<ActionResult<Cart?>>UpdateCart([FromBody]Cart cart)
        {
            var updated=await cartService.SetCartAsync(cart);
            if (updated == null)
            {
                return BadRequest("Problem updating the cart");
            }
            return updated;
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCart([FromRoute]string id)
        {
            var result = await cartService.DeleteCart(id);
            if (result == null || result == false)
            {
                return BadRequest("Problem deleting the cart");
            }
            return Ok();

        }
    }
}
