using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Binarization
{
    // thresholding называется пороговая бинаризация
    public class BinaryzatorType1 : BinaryzatorBase
    {
        public override void Process()
        {
            // https://stackoverflow.com/questions/1176910/finding-specific-pixel-colors-of-a-bitmapimage            

            var pixels = GetPixels(Image);

            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    if (GetBrightness(pixels[i, j]) > Threshold)
                    {
                        PutPixels(FinalImage, WhiteColor, i, j);
                    }
                    else
                    {
                        PutPixels(FinalImage, BlackColor, i, j);
                    }
                }
            }
        }
    }
}
