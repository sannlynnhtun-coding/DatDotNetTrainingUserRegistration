using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Domain.Services;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly UserSessionService _userSessionService;

        public ProductController()
        {
            _db = new AppDbContext();
            _userSessionService = new UserSessionService();
        }

        [HttpGet]
        public IActionResult GetProducts([FromHeader] Guid userId, [FromHeader] Guid sessionId)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return Unauthorized("Invalid or expired session.");

            var lst = _db.TblProducts
                .Where(x => x.IsDelete == false)
                .ToList();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromHeader] Guid userId, [FromHeader] Guid sessionId, [FromBody] ProductCreateRequestDto request)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return Unauthorized("Invalid or expired session.");

            TblProduct item = new TblProduct
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                CreatedBy = userId.ToString(),
                CreatedDate = DateTime.Now,
            };

            _db.TblProducts.Add(item);
            int result = _db.SaveChanges();

            var model = new ProductCreateResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product created successfully." : "Failed to create product."
            };

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(int id, [FromHeader] Guid userId, [FromHeader] Guid sessionId, [FromBody] ProductUpdateRequestDto request)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return Unauthorized("Invalid or expired session.");

            var product = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            if (product is null)
                return NotFound();

            if (!string.IsNullOrEmpty(request.ProductName))
                product.ProductName = request.ProductName;

            if (request.Price > 0)
                product.Price = request.Price ?? 0;

            if (request.Quantity > 0)
                product.Quantity = request.Quantity ?? 0;

            product.ModifiedBy = userId.ToString();
            product.ModifiedDate = DateTime.Now;

            int result = _db.SaveChanges();

            var model = new ProductCreateResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product updated successfully." : "Failed to update product."
            };

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id, [FromHeader] Guid userId, [FromHeader] Guid sessionId)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return Unauthorized("Invalid or expired session.");

            var product = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            if (product is null)
                return NotFound();

            product.IsDelete = true;
            product.ModifiedBy = userId.ToString();
            product.ModifiedDate = DateTime.Now;

            int result = _db.SaveChanges();

            var model = new ProductCreateResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product deleted successfully." : "Failed to delete product."
            };

            return Ok(model);
        }
    }
}
