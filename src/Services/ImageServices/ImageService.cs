using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Wait.Services.FileServices;

namespace Wait.Services.ImageServices;


// Define a class named ImageService that implements the IImageService interface.
public sealed class ImageService : IImageService
{
    // Define a fixed array of thumbnail widths (in pixels) that will be used to generate resized images.
    private static readonly int[] ThumbnailsWidth = [32, 64, 128, 256, 512, 1024];

    // Define which file extensions are allowed for uploaded images.
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];

    // Define which MIME types are allowed for uploaded images (extra validation beyond extension).
    private static readonly string[] AllowedMimeTypes = ["image/jpeg", "image/png", "image/gif"];

    // Method to check if an uploaded file is a valid image.
    public bool IsValidImage(IFormFile file)
    {
        // If the file has no content (size = 0), it's not valid.
        if (file.Length == 0)
        {
            return false;
        }

        // Extract the file extension (e.g., ".jpg") and convert it to lowercase for consistency.
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        // Return true only if BOTH the extension and MIME type are in the allowed lists.
        return AllowedExtensions.Contains(extension) && AllowedMimeTypes.Contains(file.ContentType);
    }

    // Method to save the original uploaded image to disk asynchronously.
    public async Task<string> SaveOriginalImageAsync(IFormFile file, string folderPath, string folderName)
    {
        // Validate the file before saving. If invalid, throw an exception.
        if (!IsValidImage(file))
        {
            throw new ArgumentException("Invalid image file", nameof(file));
        }

        // Combine the folder path and folder name into a full file path.
        var originalFilePath = Path.Combine(folderPath, folderName);

        // Ensure the directory exists (creates it if it doesn't).
        Directory.CreateDirectory(originalFilePath);

        // Open a file stream to write the uploaded file to disk.
        using var stream = new FileStream(originalFilePath, FileMode.Create);

        // Copy the uploaded file's content into the file stream asynchronously.
        await file.CopyToAsync(stream);

        // Return the path where the file was saved.
        return originalFilePath;
    }

    // Method to generate multiple thumbnails from an original image.
    public async Task<IEnumerable<string>> GenerateThumbnailAsync(
        string originalFilePath,          // Path of the original image
        string folderPath,                // Where to save thumbnails
        string fileNameWithoutExtension,  // Base name for thumbnails
        int[]? widths = null)             // Optional custom widths (defaults to ThumbnailsWidth)
    {
        // List to store the paths of all generated thumbnails.
        var thumbnailPaths = new List<string>();

        // Get the file extension of the original image (e.g., ".jpg").
        var extension = Path.GetExtension(originalFilePath);

        // If no widths were provided, use the default ThumbnailsWidth array.
        widths ??= ThumbnailsWidth;

        // Load the original image into memory asynchronously.
        using var image = await Image.LoadAsync(originalFilePath);

        // Loop through each width and generate a resized version of the image.
        foreach (var width in widths)
        {
            // Create a new filename for the thumbnail (e.g., "image_w128.jpg").
            var thumbnailFileName = $"{fileNameWithoutExtension}_w{width}{extension}";

            // Combine folder path and filename to get the full save path.
            var thumbnailPath = Path.Combine(folderPath, thumbnailFileName);

            // Clone the original image and resize it to the given width.
            // Height is set to 0 so it scales proportionally.
            var resizedImage = image.Clone(x => x.Resize(width, 0));

            // Save the resized image asynchronously to disk.
            await resizedImage.SaveAsync(thumbnailPath);

            // Add the saved thumbnail path to the list.
            thumbnailPaths.Add(thumbnailPath);
        }

        // Return all the generated thumbnail paths.
        return thumbnailPaths;
    }
}