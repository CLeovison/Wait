using Wait.Abstract;
using Wait.Services.FileServices;

namespace Wait.Endpoint.ImageEndpoint;


public sealed class DeleteImageEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/image/{*objectKey}", async (string objectKey, IImageService imageService, CancellationToken ct) =>
        {
            try
            {
                await imageService.DeleteImageAsync(objectKey, ct);
                
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.NotFound();
            }
        });
    }
}