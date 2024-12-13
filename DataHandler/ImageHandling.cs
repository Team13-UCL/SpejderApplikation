using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Media;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;

namespace SpejderApplikation.DataHandler
{
    class ImageHandling
    {
        public async Task<byte[]> DownloadAndSaveImage(string pageUrl)
        {
            using HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(pageUrl); // get the HTML of the page

            // a regular expression to find the image URL, we use regex because the image URL is in the meta tag
            var match = Regex.Match(html, "<meta property=\"og:image\" content=\"(.*?)\"");

            if (!match.Success) // if the regex didn't find the image URL           
            {
                return null;
            }

            string imageUrl = match.Groups[1].Value; // get the image URL

            // Download SVG image
            byte[] svgBytes = await client.GetByteArrayAsync(imageUrl); // download the image


            return svgBytes; // return the image as byte array
        }


        public DrawingImage LoadSvg(byte[] svgBytes)
        {
            WpfDrawingSettings settings = new WpfDrawingSettings // settings for sharpvector wpf
            {
                IncludeRuntime = true,
                TextAsGeometry = true
            };

            // reads the svg image and converts it to a DrawingImage
            using (var stream = new MemoryStream(svgBytes))
            {
                FileSvgReader reader = new FileSvgReader(settings);
                DrawingGroup drawing = reader.Read(stream);

                return new DrawingImage(drawing); // return the DrawingImage
            }

        }

    }
}
