using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ5
{
    public class ColorDetection
    {
        // 0 - grayscale
        // 1 - red
        // 2 - blue
        // 3 - green
        int boja;

        public ColorDetection(int b)
        {
            boja = b;
        }

        public Image<Gray, Byte> ImageInRange(Image<Bgr, Byte> image)
        {
            if (boja == 1)
                return image.InRange(new Bgr(0, 0, 175), new Bgr(100, 100, 256));
            if (boja == 2)
                return image.InRange(new Bgr(175, 0, 0), new Bgr(256, 100, 100));
            if (boja == 3)
                return image.InRange(new Bgr(0, 175, 0), new Bgr(100, 256, 100));

            return image.Convert<Gray, byte>().ThresholdBinaryInv(new Gray(230), new Gray(255));
        }
    }
}
