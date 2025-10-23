
using Microsoft.AspNetCore.Mvc;
using Wait.Abstract;
using Wait.Services.FileServices;

namespace Wait.Endpoint.ImageEndpoint;


public sealed class UploadImageEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/upload", async ([FromServices] IImageService imageService,
    [FromForm] IFormFile file,
    CancellationToken ct) =>
        {
            try
            {
                if (file is null)
                {
                    return TypedResults.BadRequest("No uploaded image");
                }

                if (!imageService.IsValidImage(file))
                {
                    return TypedResults.BadRequest("Invalid image file. Only JPG, PNG, and GIF formats are allowed.");

                }
                var upload = await imageService.UploadImageAsync(file, ct);
                return TypedResults.Ok(upload);
            }
            catch (Exception)
            {
                return Results.Problem("An error occurred while uploading the image.");

            }

        })
        .DisableAntiforgery();
    }

}