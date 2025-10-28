using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Domain.Services;
using DatDotNetTrainingUserRegistration.Dtos;

namespace DatDotNetTrainingUserRegistration.Domain.Features.Product
{
    public class ProductService
    {
        private readonly AppDbContext _db;
        private readonly UserSessionService _userSessionService;

        public ProductService(AppDbContext db, UserSessionService userSessionService)
        {
            _db = db;
            _userSessionService = userSessionService;
        }

        public ProductResponseDto GetProducts(Guid userId, Guid sessionId)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return new ProductResponseDto { IsSuccess = false, Message = "Invalid or expired session." };

            var lst = _db.TblProducts
                .Where(x => !x.IsDelete)
                .Select(x => new ProductDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate,
                    IsDelete = x.IsDelete
                })
                .ToList();

            return new ProductResponseDto
            {
                IsSuccess = true,
                Message = lst.Any() ? "Product list retrieved successfully." : "No products found.",
                Products = lst
            };
        }

        public ProductResponseDto CreateProduct(Guid userId, Guid sessionId, ProductCreateRequestDto request)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return new ProductResponseDto { IsSuccess = false, Message = "Invalid or expired session." };

            var item = new TblProduct
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                CreatedBy = userId.ToString(),
                CreatedDate = DateTime.Now,
                IsDelete = false
            };

            _db.TblProducts.Add(item);
            int result = _db.SaveChanges();

            return new ProductResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product created successfully." : "Failed to create product."
            };
        }

        public ProductResponseDto UpdateProduct(int id, Guid userId, Guid sessionId, ProductUpdateRequestDto request)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return new ProductResponseDto { IsSuccess = false, Message = "Invalid or expired session." };

            var product = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            if (product is null)
                return new ProductResponseDto { IsSuccess = false, Message = "Product not found." };

            if (!string.IsNullOrEmpty(request.ProductName))
                product.ProductName = request.ProductName;

            if (request.Price.HasValue && request.Price > 0)
                product.Price = request.Price.Value;

            if (request.Quantity.HasValue && request.Quantity > 0)
                product.Quantity = request.Quantity.Value;

            product.ModifiedBy = userId.ToString();
            product.ModifiedDate = DateTime.Now;

            int result = _db.SaveChanges();

            return new ProductResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product updated successfully." : "Failed to update product."
            };
        }

        public ProductResponseDto DeleteProduct(int id, Guid userId, Guid sessionId)
        {
            if (!_userSessionService.IsSessionValid(userId, sessionId))
                return new ProductResponseDto { IsSuccess = false, Message = "Invalid or expired session." };

            var product = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            if (product is null)
                return new ProductResponseDto { IsSuccess = false, Message = "Product not found." };

            product.IsDelete = true;
            product.ModifiedBy = userId.ToString();
            product.ModifiedDate = DateTime.Now;

            int result = _db.SaveChanges();

            return new ProductResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product deleted successfully." : "Failed to delete product."
            };
        }
    }
}
