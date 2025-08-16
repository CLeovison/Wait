using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Domain.Entities;
using Wait.Entities;

namespace Wait.Infrastructure.Mapping;


public static class DtoToEntitiesMapper
{
    public static UserDto ToDto(this Users users, IPasswordHasher<Users> passwordHasher)
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

    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            ProductName = product.ProductName,
            ProductSize = product.ProductSize,
            Quantity = product.Quantity,
            Description = product.Description,
            CreatedAt = product.CreatedAt,
        };
    }

}