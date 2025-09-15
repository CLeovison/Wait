using Wait.Abstract;
using Wait.Services.Categories;

namespace Wait.Endpoint.CategoryEndpoint;


public sealed class DeleteCategoryEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/categories/{id}", async (Guid id, ICategoriesService categoriesService, CancellationToken ct) =>
        {
            var deleteCategory = await categoriesService.DeleteCategoryAsync(id, ct);

            return deleteCategory;
        });
    }
}