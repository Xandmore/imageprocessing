using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Binarization
{
    public abstract class BinaryzatorBase
    {
        public BitmapImage Image { get; set; }

        public abstract void Process();

        // prooflink
        // https://stackoverflow.com/questions/26233781/detect-the-brightness-of-a-pixel-or-the-area-surrounding-it
        public static double GetBrightness(Color color)
        {
            return (0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B);
        }

        public double GetAverageBrightness(IEnumerable<Color> colors)
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
    }
}
