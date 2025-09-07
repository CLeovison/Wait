using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Domain.Entities;
using Wait.Entities;

namespace Wait.Infrastructure.Mapping;


public static class DtoToEntitiesMapper
{
    public static UserDto ToUserDto(this Users users, IPasswordHasher<Users> passwordHasher)
    {
        return new UserDto
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Password = passwordHasher.HashPassword(new Users(), users.Password),
            Email = users.Email
        };
    }

    public static void ToUpdateDto(this Users users, UserDto userDto, IPasswordHasher<Users> passwordHasher)
    {
        users.FirstName = userDto.FirstName;
        users.LastName = userDto.LastName;
        users.Username = userDto.Username;
        if (string.IsNullOrWhiteSpace(userDto.Password))
        {
            users.Password = userDto.Password;
        }
        users.Email = userDto.Email;
        userDto.ModifiedAt = userDto.ModifiedAt;
    }

    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            ProductName = product.ProductName,
            Size = product.Size,
            Quantity = product.Quantity,
            Description = product.Description,
            CreatedAt = product.CreatedAt,
        };
    }

    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryName = category.CategoryName,
            CreatedAt = category.CreatedAt
        };
    }
}