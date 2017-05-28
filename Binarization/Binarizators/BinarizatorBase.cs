using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace Binarization
{
    public abstract class BinaryzatorBase
    {
        public double Threshold { get; set; }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                FinalImage = new WriteableBitmap(_image);
            }
        }
        public WriteableBitmap FinalImage { get; set; }


        protected static PixelColor WhiteColor { get; } = new PixelColor() { Red = 255, Green = 255, Blue = 255 };
        protected static PixelColor BlackColor { get; } = new PixelColor() { Red = 0, Green = 0, Blue = 0 };

        public abstract void Process();

        // prooflink
        // https://stackoverflow.com/questions/26233781/detect-the-brightness-of-a-pixel-or-the-area-surrounding-it
        public static double GetBrightness(Color color)
        {
            return (0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B);
        }

        public static double GetBrightness(PixelColor color)
        {
            return (0.2126 * color.Red + 0.7152 * color.Green + 0.0722 * color.Blue);
        }

        public static void PutPixels(WriteableBitmap bitmap, PixelColor[,] pixels, int x, int y)
        {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, x, y);
        }

        public static void PutPixels(WriteableBitmap bitmap, PixelColor pixel, int x, int y)
        {
            var pixels = new PixelColor[1,1];
            pixels[0, 0] = pixel;
            bitmap.WritePixels(new Int32Rect(0, 0, 1, 1), pixels, 1 * 4, x, y);
        }

        public static PixelColor[,] GetPixels(BitmapSource source)
        {
            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

            int width = source.PixelWidth;
            int height = source.PixelHeight;
            PixelColor[,] result = new PixelColor[width, height];

            CopyPixels(source, result, width * 4, 0);
            //source.CopyPixels(result, width * 4, 0);
            return result;
        }

        public static double GetAverageBrightness(IEnumerable<Color> colors)
        {
            int count = 0;
            double sumBrightness = 0;

            foreach (var color in colors)
            {
                count++;
                sumBrightness += GetBrightness(color);
            }

            return sumBrightness / count;
        }

        public string GetImageSize()
        {
            return $"{Image.PixelWidth};{Image.PixelHeight}";
        }


        public static void CopyPixels(BitmapSource source, PixelColor[,] pixels, int stride, int offset)
        {
            var height = source.PixelHeight;
            var width = source.PixelWidth;
            var pixelBytes = new byte[height * width * 4];
            source.CopyPixels(pixelBytes, stride, offset);
            int y0 = offset / width;
            int x0 = offset - width * y0;
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    pixels[x + x0, y + y0] = new PixelColor
                    {
                        Blue = pixelBytes[(y * width + x) * 4 + 0],
                        Green = pixelBytes[(y * width + x) * 4 + 1],
                        Red = pixelBytes[(y * width + x) * 4 + 2],
                        Alpha = pixelBytes[(y * width + x) * 4 + 3],
                    };
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PixelColor
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }
}
