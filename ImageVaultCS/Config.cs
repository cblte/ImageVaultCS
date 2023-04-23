namespace ImageVaultCS;
class Config
{
    public int ThumbnailSize { get; set; } = 300;
    public string Title { get; set; } = "My Gallery";
    public string Description { get; set; } = "A collection of my favorite photos.";
    public string Theme { get; set; } = "default";
    // TODO: Add more configurable options as needed
}