using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPUGraphicsEngine
{
    public class SideTriangle
    {
        public SideTriangle(double x0, double y0, double z0, double x1, double y1, double z1, double x2, double y2, double z2)
        {
            Points = new List<CustomPoint>() { new CustomPoint(x0, y0, z0), new CustomPoint(x1, y1, z1), new CustomPoint(x2, y2, z2) };
        }
        public SideTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            Points = new List<CustomPoint>() { 
                new CustomPoint(v0.X, v0.Y, v0.Z),
                new CustomPoint(v1.X, v1.Y, v1.Z),
                new CustomPoint(v2.X, v2.Y, v2.Z), };
        }
        public SideTriangle(CustomPoint p1, CustomPoint p2, CustomPoint p3, Color? color = null)
        {
            Points = new List<CustomPoint>() { p1, p2, p3 };
            if (color != null)
                paintColor = color.Value;
        }

        public bool IsRender
        {
            get { return isRender; }
            set
            {
                foreach(var point in Points)
                {
                    point.IsRender = value;
                }
                isRender = value;
            }
        }
        bool isRender = false;
        public List<CustomPoint> Points { get; set; }
        public Color paintColor = Color.Chocolate;


        public Vector<double> Normal { get; set; }

        public Vector<double> ProcessedNormal { get; set; }

        public void Process(Matrix<double> modelMatrix)
        {
            if (!IsRender) return;
            if (Normal != null)
            {
                ProcessedNormal = modelMatrix.Inverse().TransposeThisAndMultiply(Normal).Normalize(2);
            }
            foreach (var point in Points)
            {
                point.Process(modelMatrix);
            }
        }

        public void DrawSide(DirectBitmap bitmap, int radius, double [,] zTable)
        {
            var convPoints = new List<Point>();
            var zPoints = new List<double>();

            foreach (var point in Points)
            {
                convPoints.Add(new Point((int)((point.X + 1) * radius), (int)((point.Y + 1) * radius)));
                zPoints.Add(point.Z);
            }

            Helpers.Fill(bitmap, convPoints, paintColor, zPoints, zTable);
        }

    }
}
