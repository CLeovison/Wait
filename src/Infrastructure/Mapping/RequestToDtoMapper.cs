using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Request.CategoriesRequest;
using Wait.Contracts.Request.ProductRequest;
using Wait.Contracts.Request.UserRequest;
using Wait.Domain.Entities;
namespace Wait.Infrastructure.Mapping;



public static class RequestToDtoMapper
{
    /// <summary>
    /// Provides extension methods for mapping user-related request models to UserDto objects.
    /// Converts CreateUserRequest and UpdateUserRequest into DTO representations for downstream processing.
    /// </summary>

    public static UserDto ToCreateRequest(this CreateUserRequest req)
    {

        return new UserDto
        {
            FirstName = req.FirstName ?? string.Empty,
            LastName = req.LastName ?? string.Empty,
            Username = req.Username ?? string.Empty,
            Password = req.Password ?? string.Empty,
            ConfirmPassword = req.ConfirmPassword ?? string.Empty,
            Birthday = DateOnly.Parse(req.Birthday.ToString()),
            Email = req.Email ?? string.Empty
        };
    }
    public static UserDto ToRequestUpdate(this UpdateUserRequest req, IPasswordHasher<Users> passwordHasher)
    {
        return new UserDto
        {
            FirstName = req.FirstName ?? string.Empty,
            LastName = req.LastName ?? string.Empty,
            Username = req.Username ?? string.Empty,
            Password = passwordHasher.HashPassword(new Users(), req.Password ?? string.Empty),
            ConfirmPassword = req.ConfirmPassword ?? string.Empty,
            Birthday = DateOnly.Parse(req.Birthday.ToString()),
            Email = req.Email ?? string.Empty
        };
    }

    public static ProductDto ToCreateRequest(this CreateProductRequest req)
    {
        return new ProductDto
        {
            ProductName = req.ProductName ?? string.Empty,
            Price = req.Price,
            Description = req.Description,
            Size = req.Size,
            Color = req.Color,
            Image = req.Image,
            CategoryId = req.CategoryId,
            Quantity = req.Quantity,
            CategoryName = req.CategoryName,

        };
    }

    public static CategoryDto ToCreateCategory(this CreateCategoriesRequest req)
    {
        return new CategoryDto
        {
            CategoryName = req.CategoryName,
            CategoryDescription = req.CategoryDescription,

        };
    }
}
