using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skinet.Data;
using skinet.Entities;
using skinet.Interfaces;
using skinet.RequestHelpers;
using skinet.Specifications;

namespace skinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product>repo) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec=new ProductSpecification(productSpecParams);
            return await CreatePageResult(repo, spec, productSpecParams.PageIndex, productSpecParams.PageSize);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            repo.Add(product);
            if (!await repo.SaveChangesAsync())
            {
                return BadRequest("Failed to create product");
            }
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult>UpdateProduct(int id, Product product)
        {
            if (!repo.Existed(id))
            {
                return NotFound();
            }
            repo.Update(product);
            if(!await repo.SaveChangesAsync())
            {
                return BadRequest("Failed to update product");
            }
            return NoContent();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            repo.Delete(product);
            if (!await repo.SaveChangesAsync())
            {
                return BadRequest("Failed to delete product");
            }
            return NoContent();
        }
        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec=new BrandListSpecification();
            return Ok(await repo.GetAllWithSpec(spec));
        }
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repo.GetAllWithSpec(spec));
        }

    }
}
