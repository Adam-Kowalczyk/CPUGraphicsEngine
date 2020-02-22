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

        public bool IsGlowing { get; set; } = false;

        public bool ChangeColor = false;
        public Vector<double> Normal 
        { 
            get 
            {
                var norm = Points[0].ProcessedNormal.Add(Points[1].ProcessedNormal).Add(Points[2].ProcessedNormal);
                return norm.Multiply(1.0/3);
            } 
        }

        public Vector<double> Position
        {
            get
            {
                var pos = Points[0].Vector.Add(Points[1].Vector).Add(Points[2].Vector);
                return pos.Multiply(1.0 / 3);
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

        public void DrawSide(DirectBitmap bitmap, Matrix<double> projectionMatrix, int radius, double [,] zTable, SurfaceInfo surface, List<Light> lights, ShadingMode shadingMode)
        {
            var pos = GetPositions();
            var norms = GetNormals();

            Process(projectionMatrix, false);
            var convPoints = new List<Point>();
            var zPoints = new List<double>();

            

            foreach (var point in Points)
            {
                convPoints.Add(new Point((int)((point.X + 1) * radius), (int)((point.Y + 1) * radius)));

                zPoints.Add(point.Z);
            }

            var sMode = shadingMode;
            if(IsGlowing)
            {
                sMode = ShadingMode.None;
            }

            Helpers.Fill(bitmap, convPoints, paintColor, zPoints, zTable, pos, norms, surface, lights, sMode); ;
        }

        public List<Vector<double>> GetPositions()
        {
            var pos = new List<Vector<double>>();
            foreach(var p in Points)
            {
                pos.Add(Helpers.BuildVector(p.X, p.Y, p.Z));
            }
            return pos;
        }

        public List<Vector<double>> GetNormals()
        {
            var pos = new List<Vector<double>>();
            foreach (var p in Points)
            {
                var normal = p.ProcessedNormal;
                pos.Add(Helpers.BuildVector(normal[0], normal[1], normal[2]).Normalize(2));
            }
            return pos;
        }
    }
}
