using DatDotNetTrainingUserRegistration.Domain.Features.Product;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts([FromHeader] Guid userId, [FromHeader] Guid sessionId)
        {
            var result = _productService.GetProducts(userId, sessionId);
            if (!result.IsSuccess) return Unauthorized(result);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromHeader] Guid userId, [FromHeader] Guid sessionId, [FromBody] ProductCreateRequestDto request)
        {
            var result = _productService.CreateProduct(userId, sessionId, request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(int id, [FromHeader] Guid userId, [FromHeader] Guid sessionId, [FromBody] ProductUpdateRequestDto request)
        {
            var result = _productService.UpdateProduct(id, userId, sessionId, request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id, [FromHeader] Guid userId, [FromHeader] Guid sessionId)
        {
            var result = _productService.DeleteProduct(id, userId, sessionId);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
