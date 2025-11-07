using Wait.Abstract;
using Wait.Services.FileServices;

namespace Wait.Endpoint.ImageEndpoint;


public sealed class DeleteImageEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/image/{id}", async (string id, IImageService imageService, CancellationToken ct) =>
        {
            await imageService.DeleteImageAsync(id, ct);

            return TypedResults.NoContent();
        });
    }
}