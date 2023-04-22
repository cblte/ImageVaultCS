using System;
using System.IO;
using System.Runtime.InteropServices;
using CommandLine;
using Newtonsoft.Json;

namespace ImageVaultCS
{
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
                          Console.WriteLine($"Output folder '{outputFolder}' does not exist.");
                          return;
                      }

                      var config = new Config();

                      if (File.Exists(configFilePath))
                      {
                          config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFilePath));
                      }

                      // TODO: Implement gallery generation using inputFolder, outputFolder, and config
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

    class Config
    {
        public int ThumbnailSize { get; set; } = 200;
        public string Title { get; set; } = "My Gallery";
        public string Description { get; set; } = "A collection of my favorite photos.";
        public string Theme { get; set; } = "default";
        // TODO: Add more configurable options as needed
    }
}
