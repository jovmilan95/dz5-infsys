using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ5.Shapes
{
    public class CircleDetection: ShapeDetection
    {
        public override Image<Bgr, Byte> Detect(ColorDetection colorDetection, Image<Bgr, Byte> originalImage, int area)
        {
            var contours = GetContours(colorDetection.ImageInRange(originalImage));
            var image = originalImage.Clone();
            for (int i = 0; i < contours.Size; i++)
            {
                double perimeter = CvInvoke.ArcLength(contours[i], true);
                VectorOfPoint approx = new VectorOfPoint();
                CvInvoke.ApproxPolyDP(contours[i], approx, 0.04 * perimeter, true);
                if (approx.Size > 6 && GetArea(contours[i].ToArray().ToList()) <= area)
                { 
                    CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 0, 0), 2);
                }
            }

            return image;
        }
    }
}
