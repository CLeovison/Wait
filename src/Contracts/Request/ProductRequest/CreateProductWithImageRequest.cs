
using Wait.Contracts.Data;
using Wait.Entities;

namespace Wait.Contracts.Request.ProductRequest;


public record CreateProductWithImageRequest(ProductDto product, IFormFile file);