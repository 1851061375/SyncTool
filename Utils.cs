using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;

namespace SyncTool
{
    internal static class Utils
    {
        internal static Image ResizeImage(Image imgToResize, Size size)
        {
            // Get the image current width
            int sourceWidth = imgToResize.Width;
            // Get the image current height
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            // Calculate width and height with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            nPercent = Math.Min(nPercentW, nPercentH);
            // New Width and Height
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Image)b;
        }

        internal static Image GetImageFromUrl(string url)
        {
            WebClient client = new WebClient();
            byte[] imageData = client.DownloadData(url);
            Image image;
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }

    }

    internal static class Logger
    {
        private static string filePath = "../../../logs.txt";
        public static void Write(string content)
        {
            var log = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:s] ") + content;
            string existingContent = File.ReadAllText(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.WriteLine(log);
                if (!string.IsNullOrWhiteSpace(existingContent))
                {
                    sw.WriteLine(existingContent);
                }
            }
        }
    }
}
