using Wait.Abstract;
using Wait.Services.FileServices;

namespace Wait.Endpoint.ImageEndpoint;

public sealed class DeleteImageEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/image/{*objectKey}", async (
            string objectKey,
            IImageService imageService,
            CancellationToken ct) =>
        {
            try
            {
                var request = await imageService.DeleteImageAsync(objectKey, ct);

                if (request)
                {
                    return Results.NoContent();
                }
                else
                {
                    return Results.NotFound();
                }

            }
            catch (FileNotFoundException ex)
            {

                return Results.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });
    }
}