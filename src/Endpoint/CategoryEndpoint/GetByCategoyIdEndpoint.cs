using Wait.Abstract;
using Wait.Services.Categories;

namespace Wait.Endpoint.CategoryEndpoint;

public sealed class GetCategoryByIdEndpoint : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/category/{id}", async (Guid id, ICategoriesService categoriesService, CancellationToken ct) =>
        {
            var getCategory = await categoriesService.GetCategoryByIdAsync(id, ct);

            return TypedResults.Ok(getCategory);
        });
    }
}