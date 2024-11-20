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

namespace SpejderApplikation.DataHandler
{
    class ImageHandling
    {
        public async Task<string> GetOgImageUrl(string pageUrl)
        {
            using HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(pageUrl);

            // Regex to find the og:image meta tag
            var match = Regex.Match(html, "<meta property=\"og:image\" content=\"(.*?)\"");
            return match.Success ? match.Groups[1].Value : null;
        }

        public async Task<string> DownloadSvgAsync(string imageUrl)
        {
            using HttpClient client = new HttpClient();
            byte[] svgBytes = await client.GetByteArrayAsync(imageUrl);

            // Create a file in the same folder as the application
            string fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            await File.WriteAllBytesAsync(filePath, svgBytes);
            return filePath;
        }

        public DrawingImage LoadSvg(string filePath)
        {
            try
            {
                // Create settings for SVG rendering
                WpfDrawingSettings settings = new WpfDrawingSettings
                {
                    IncludeRuntime = true,
                    TextAsGeometry = true
                };

                // Read the SVG file and convert it to a Drawing
                FileSvgReader reader = new FileSvgReader(settings);
                DrawingGroup drawing = reader.Read(filePath);

                // Convert to DrawingImage
                return new DrawingImage(drawing);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error displaying SVG: {ex.Message}");
            }
        }
        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("manglende hjemmeside");
                return;
            }

            try
            {
                // Extract the image URL from the website
                string imageUrl = await vm.GetOgImageUrl(url);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    MessageBox.Show("kunne ikke finde billedet");
                    return;
                }

                // Download the SVG image
                string filePath = await vm.DownloadSvgAsync(imageUrl);

                // Display the SVG image
                //DownloadedImage.Source = vm.LoadSvg(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
