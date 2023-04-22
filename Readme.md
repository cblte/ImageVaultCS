# ImageVaultCS

ImageVaultCS is a console application that generates a static image gallery.
It takes a folder containing images as input and generates a static site with
a thumbnail gallery and individual pages for each image. The output folder
can be specified as a command line argument.

## Features

* Generates a static image gallery with thumbnails and individual pages for each image
* Supports input folders with a variety of image formats

## Planned Features

* Allows customizations of gallery layout and appearance

## Installation

1. Install the latest version of the .NET SDK
2. Clone the repository to your local machine
3. Build the solution using `dotnet build`
4. Run the application using `dotnet ImageVaultCS.dll`

## Usage
        
    dotnet ImageVaultCS.dll -i [input folder] -o [output folder]

Example:
     
    dotnet ImageVaultCS.dll -i ./images -o ./output

The above command will generate a static site containing an image gallery with
thumbnails and individual pages for each image in the "images" subfolder of your
current folder. The output will be saved in the "output" subfolder of your
current folder. If the output-Folder does not exists, it will be created.

## Customization

Future: ImageVaultCS supports customization of the gallery layout and appearance through
configuration files. The default configuration can be found in the "config.json" file.

## License

This project is licensed under the MIT License.
See the [LICENSE](https://chat.openai.com/LICENSE) file for details.

## Contributions

Contributions to this project are welcome.
To contribute, please fork the repository and submit a pull request.