using CommandLine;
using Newtonsoft.Json;

namespace ImageVaultCS;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(o =>
              {
                  var inputFolder = o.InputFolder;
                  var outputFolder = o.OutputFolder;
                  var configFilePath = o.ConfigFilePath;

                  if (!Directory.Exists(inputFolder))
                  {
                      Console.WriteLine($"Input folder '{inputFolder}' does not exist.");
                      return;
                  }

                  if (!Directory.Exists(outputFolder))
                  {
                      try
                      {
                          Console.WriteLine($"Output folder '{outputFolder}' does not exist. Trying to creating it.");
                          // Create the output folder if it doesn't exist
                          Directory.CreateDirectory(outputFolder);
                      }
                      catch (UnauthorizedAccessException ex)
                      {
                          // If the user doesn't have write permissions, throw an exception
                          Console.WriteLine($"Cannot create directory '{outputFolder}' because the user doesn't have write permissions.");
                          Console.WriteLine($"Exception: {ex}");
                          return;
                      }
                  }

                  var config = new Config();

                  if (File.Exists(configFilePath))
                  {
                      config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFilePath));
                  }

                  GalleryGenerator gg = new GalleryGenerator(inputFolder, outputFolder, config.ThumbnailSize, config.Title);
                  gg.GenerateGallery();
              })
              .WithNotParsed(errors =>
              {
                  // Handle command line parsing errors
                  foreach (var error in errors)
                  {
                      Console.WriteLine(error.ToString());
                  }
              });
    }
}


class Options
{
    [Option('i', "input", Required = true, HelpText = "The input folder containing images.")]
    public string InputFolder { get; set; } = "";

    [Option('o', "output", Required = true, HelpText = "The output folder for the generated static site.")]
    public string OutputFolder { get; set; } = "";

    [Option('c', "config", Required = false, Default = "config.json", HelpText = "The configuration file path.")]
    public string ConfigFilePath { get; set; } = "";
}
