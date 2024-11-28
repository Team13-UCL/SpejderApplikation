using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System.Windows.Media;
using System.Windows;
using System.Windows.Navigation;

namespace SpejderApplikation.DataHandler
{
    class ImageHandling
    {
        public async Task<byte[]> DownloadAndSaveImage(string pageUrl)
        {
            using HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(pageUrl);

            // en regular expression til at finde URL'en til billedet, vi bruger regex for at vi kan finde matchende tekst i en streng
            var match = Regex.Match(html, "<meta property=\"og:image\" content=\"(.*?)\"");

            if (!match.Success)
            {
                return null;
            }

            string imageUrl = match.Groups[1].Value; 

            // Download SVG image
            byte[] svgBytes = await client.GetByteArrayAsync(imageUrl);

            
            return svgBytes;
        }


        public DrawingImage LoadSvg(byte[] svgBytes)
        {
            //try
            //{
                // settings for wpf
                WpfDrawingSettings settings = new WpfDrawingSettings
                {
                    IncludeRuntime = true,
                    TextAsGeometry = true
                };

                // læser svg fil og konvertere til tegning
                using (var stream = new MemoryStream(svgBytes))
                {
                    FileSvgReader reader = new FileSvgReader(settings);
                    DrawingGroup drawing = reader.Read(stream);

                    return new DrawingImage(drawing);
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new InvalidOperationException($"Error displaying SVG: {ex.Message}");
            //}
        }

    }
}
