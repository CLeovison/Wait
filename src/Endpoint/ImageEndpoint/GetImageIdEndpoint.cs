
using Wait.Abstract;
using Wait.Services.FileServices;

namespace Wait.Endpoint.ImageEndpoint;


public sealed class GetImageEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/image/{id}", async (IImageService imageService, string id, string fileName, int? width, CancellationToken ct) =>
        {
            try
            {
                var request = await imageService.GetImageByIdAsync(id, fileName, width, ct);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}