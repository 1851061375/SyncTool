using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using TD.QLDL.Library.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using SixLabors.ImageSharp.Processing;

namespace SyncTool
{
    internal static class Utils
    {
        internal static string domain = @"https://ql-sdl.hanhchinhcong.net";

        internal static SixLabors.ImageSharp.Image NewResize(string url)
        {
            
            using (HttpClient client = new HttpClient())
            {
                byte[] imageBytes = client.GetByteArrayAsync(url).Result;
                var origin = SixLabors.ImageSharp.Image.Load(imageBytes);
                int newWidth = origin.Width < 1200 ? 1200 : origin.Width;
                int newHeight = origin.Height < 628 ? 628 : origin.Height;
                var resize = origin;
                resize.Mutate(x => x.Resize(newWidth, newHeight));
                return resize;
            }
        }
       
    }

    internal static class Logger
    {
        private static string filePath = "../../logs.txt";
        public static void Write(string content)
        {
            
            string log = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:s] ") + content;

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(log);
                }
            }
            else
            {
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

        public static List<string> Read()
        {
            List<string> lines = new List<string>();

            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath).ToList();
            }
            return lines;
        }
    }

    
}
