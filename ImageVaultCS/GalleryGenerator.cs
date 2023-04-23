using System.Linq;
using System;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageVaultCS;

class GalleryGenerator
{
    private readonly string _inputFolder;
    private readonly string _outputFolder;
    private readonly int _thumbnailSize;
    private readonly string _title;

    public GalleryGenerator(string inputFolder, string outputFolder, int thumbnailSize, string title)
    {
        _inputFolder = inputFolder;
        _outputFolder = outputFolder;
        _thumbnailSize = thumbnailSize;
        _title = title;
    }

    public void GenerateGallery()
    {
        // Get all image files from the iput folder
        List<string> imagePaths = Directory.GetFiles(_inputFolder, "*.*", SearchOption.TopDirectoryOnly)
            .Where(file => file.ToLower().EndsWith(".jpg") ||
                            file.ToLower().EndsWith(".jpeg") ||
                            file.ToLower().EndsWith(".png") ||
                            file.ToLower().EndsWith(".gif"))
            .ToList();
        GenerateThumbnailsAndCopy(imagePaths);

        // Generate the HTML for the thumbnail gallery
        string thumbnailTableHtml = GenerateThumbnailTable(imagePaths);

        // Create the HTML file
        string htmlFilePath = Path.Combine(_outputFolder, "index.html");
        using (StreamWriter writer = new StreamWriter(htmlFilePath))
        {
            // Write the HTML header
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("<link rel=\"stylesheet\" href=\"https://unpkg.com/spectre.css/dist/spectre.min.css\">");
            writer.WriteLine("<link rel=\"stylesheet\" href=\"https://unpkg.com/spectre.css/dist/spectre-exp.min.css\">");
            writer.WriteLine("<link rel=\"stylesheet\" href=\"https://unpkg.com/spectre.css/dist/spectre-icons.min.css\">");
            writer.WriteLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            writer.WriteLine($"<title>{_title}</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine($"<h1>{_title}</h1>");

            // Write the thumbnail gallery HTML
            writer.WriteLine(thumbnailTableHtml);

            // Write the HTML footer
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
    }

    private void GenerateThumbnailsAndCopy(List<string> imagepPaths)
    {
        List<string> thumbnailPaths = new List<string>();
        foreach (var imagePath in imagepPaths)
        {
            string targetPath = Path.Combine(_outputFolder, Path.GetFileName(imagePath));
            string thumbnailPath = Path.Combine(_outputFolder, Path.GetFileNameWithoutExtension(imagePath) + "_thumb.jpg");
            // create the image, resize and save it at target location   
            var image = Image.Load(imagePath);
            image.Mutate(x => x.Resize(_thumbnailSize, 0));
            image.SaveAsJpeg(thumbnailPath);
            image.Dispose();
            // finally copy the original file
            File.Copy(imagePath, targetPath, true);

        }
    }

    private string GenerateThumbnailTable(List<string> imagePaths)
    {
        StringBuilder sb = new StringBuilder();
        // Start the table
        sb.AppendLine("<table style=\"width:100%;\">");
        for (int i = 0; i < imagePaths.Count; i += 3)
        {

            sb.AppendLine("<tr>");
            // Loop through the next three images
            for (int j = i; j < i + 3; j++)
            {
                // If we have run out of images, add an empty table cell
                if (j >= imagePaths.Count)
                {
                    sb.AppendLine("<td></td>");
                }
                else
                {
                    string thumbnailPath = Path.GetFileNameWithoutExtension(imagePaths[j]) + "_thumb.jpg";
                    string targetPath = Path.GetFileName(imagePaths[j]);

                    sb.AppendLine("<td style=\"text-align:center;\">");
                    // Add Link around the thumbnail
                    sb.AppendLine($"<a href=\"{targetPath}\" target=\"_blank\">");
                    // Add an image tag for the thumbnail
                    sb.AppendLine($"<img src=\"{thumbnailPath}\" alt=\"Thumbnail of {targetPath}\">");
                    sb.AppendLine("</a>");
                    sb.AppendLine("</td>");
                }
            }
            // End the row
            sb.AppendLine("</tr>");
        }
        sb.AppendLine("</table>");
        // Return the generated HTML
        return sb.ToString();
    }
}