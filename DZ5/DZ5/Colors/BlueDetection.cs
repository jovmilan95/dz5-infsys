using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ5
{
    public class BlueDetection : ColorDetection
    {
        public Image<Gray, Byte> ImageInRange(Image<Bgr, Byte> image)
        {
            return image.InRange(new Bgr(175, 0, 0), new Bgr(256, 100, 100));
        }
    }
}
