
using Wait.Abstract;
using Wait.Services.FileServices;

namespace Wait.Endpoint.ImageEndpoint;


public sealed class GetImageEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/image/{id}", async (IImageService imageService, string id, int? width, CancellationToken ct) =>
        {
            try
            {
                var image = await imageService.GetImageMetadataAsync(id, ct);
                var stream = await imageService.GetImageByIdAsync(id, width, ct);

                return Results.File(stream, image.MimeType);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}