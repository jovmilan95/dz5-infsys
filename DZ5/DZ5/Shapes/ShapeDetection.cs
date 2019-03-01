using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ5.Shapes
{
    public enum Shape { Triangle, Rectangle, Hexagon, Circle, AllShapes }

    public class ShapeDetection
    {
        Shape shape;

        public ShapeDetection(Shape s)
        {
            shape = s;
        }

        public VectorOfVectorOfPoint GetContours(Image<Gray, Byte> image)
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat m = new Mat();

            CvInvoke.FindContours(image, contours, m, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            return contours;
        }

        public int GetArea(List<Point> points)
        {
            points.Add(points[0]);
            return Math.Abs(points.Take(points.Count - 1)
                .Select((p, i) => (p.X * points[i + 1].Y) - (p.Y * points[i + 1].X)).Sum() / 2);
        }

        public Image<Bgr, Byte> Detect(ColorDetection colorDetection, Image<Bgr, Byte> originalImage, int area)
        {
            var contours = GetContours(colorDetection.ImageInRange(originalImage));
            var image = originalImage.Clone();
            for (int i = 0; i < contours.Size; i++)
            {
                double perimeter = CvInvoke.ArcLength(contours[i], true);
                VectorOfPoint approx = new VectorOfPoint();
                CvInvoke.ApproxPolyDP(contours[i], approx, 0.04 * perimeter, true);

                switch (shape)
                {
                    case Shape.Triangle:
                        {
                            if (approx.Size == 3 && GetArea(contours[i].ToArray().ToList()) <= area)
                                CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 0, 0), 2);
                        }
                        break;
                    case Shape.Rectangle:
                        {
                            if (approx.Size == 4 && GetArea(contours[i].ToArray().ToList()) <= area)
                                CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 0, 0), 2);
                        }
                        break;
                    case Shape.Hexagon:
                        {
                            if (approx.Size == 6 && GetArea(contours[i].ToArray().ToList()) <= area)
                                CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 0, 0), 2);
                        }
                        break;
                    case Shape.Circle:
                        {
                            if (approx.Size > 6 && GetArea(contours[i].ToArray().ToList()) <= area)
                                CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 0, 0), 2);
                        }
                        break;
                    case Shape.AllShapes:
                        {
                            CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 0, 0), 2);
                        }
                        break;
                }
            }
            return image;
        }
    }
}
