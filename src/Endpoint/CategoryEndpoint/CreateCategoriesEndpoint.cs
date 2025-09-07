using Wait.Abstract;
using Wait.Contracts.Request.CategoriesRequest;
using Wait.Infrastructure.Mapping;
using Wait.Services.Categories;

namespace Wait.Endpoint.CategoryEndpoint;

public sealed class CreateCategoriesEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/category", async (ICategoriesService categoriesService, CreateCategoriesRequest req, CancellationToken ct) =>
        {
            var mapCategory = req.ToCreateCategory();

            var request = await categoriesService.CreateCategoryAsync(mapCategory, ct);

            return Results.Created();
        });
    }
}