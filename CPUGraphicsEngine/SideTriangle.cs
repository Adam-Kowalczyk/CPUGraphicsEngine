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
        public SideTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 nv0, Vector3 nv1, Vector3 nv2)
        {
            Points = new List<CustomPoint>() { 
                new CustomPoint(v0.X, v0.Y, v0.Z) { Normal = Helpers.BuildNormal(nv0, v0) },
                new CustomPoint(v1.X, v1.Y, v1.Z) { Normal = Helpers.BuildNormal(nv1, v1) },
                new CustomPoint(v2.X, v2.Y, v2.Z) { Normal = Helpers.BuildNormal(nv2, v2) } };
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


        public Vector<double> Normal 
        { 
            get 
            {
                //var norm = Points[0].Normal.Add(Points[1].Normal).Add(Points[2].Normal);
                var norm = Points[0].ProcessedNormal.Add(Points[1].ProcessedNormal).Add(Points[2].ProcessedNormal);
                return norm.Multiply(1.0/3);
                //return Points[0].ProcessedNormal.Add(Points[1].ProcessedNormal).Add(Points[2].ProcessedNormal);
            } 
        }


        public void Process(Matrix<double> modelMatrix, bool useNormals = true)
        {
            if (!IsRender) return;

            foreach (var point in Points)
            {
                point.Process(modelMatrix, useNormals);
            }

        }

        public void DrawSide(DirectBitmap bitmap, int radius, double [,] zTable)
        {
            var convPoints = new List<Point>();
            var zPoints = new List<double>();

            var x = Normal;

            foreach (var point in Points)
            {
                convPoints.Add(new Point((int)((point.X + 1) * radius), (int)((point.Y + 1) * radius)));
                zPoints.Add(point.Z);
            }

            Helpers.Fill(bitmap, convPoints, paintColor, zPoints, zTable);
        }

    }
}
