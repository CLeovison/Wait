using Wait.Abstract;
using Wait.Services.FileServices;

namespace Wait.Endpoint.ImageEndpoint;


public sealed class GetImageByObjectKeyEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/image/{*objectKey}", async (IImageService imageService, string objectKey, int? width, CancellationToken ct) =>
{
    try
    {
        var image = await imageService.GetImageObjectKeyAsync(objectKey, ct);
        var stream = await imageService.GetImageStreamAsync(image, width, ct);

        return Results.File(stream, image.MimeType);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

    }
}