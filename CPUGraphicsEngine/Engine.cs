using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUGraphicsEngine
{
    public class Engine
    {

        public Engine(DirectBitmap bitmap)
        {
            DBitmap = bitmap;
            Aspect = (double)dBitmap.Height / dBitmap.Width;
        }
        public DirectBitmap DBitmap
        {
            get
            {
                return dBitmap;
            }
            set
            {
                if (dBitmap != null)
                    dBitmap.Dispose();
                dBitmap = value;
                Aspect = (double)dBitmap.Height / dBitmap.Width;
            }
        }
        DirectBitmap dBitmap;
        public Camera SelectedCamera { get; set; }

        public double Near { get; set; }
        public double Far { get; set; }
        public double Fov { get; set; }

        public double Aspect { get; set; }

        public Matrix<double> ProjectionMatrix
        {
            get
            {
                var e = 1 / Math.Tan(Fov * Math.PI / 360);
                return Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {e, 0, 0, 0 },
                                        {0, e/Aspect, 0, 0 },
                                        {0, 0, -(Far + Near)/(Far - Near), -(2 * Far * Near)/(Far - Near) },
                                        {0, 0, -1, 0 }});
            }
        }

        public List<Shape> Shapes { get; set; } = new List<Shape>();

        double[,] zTable;

        void ResetZTable()
        {
            for(int i = 0; i < dBitmap.Height; i++)
            {
                for (int j = 0; j < dBitmap.Width; j++)
                {
                    zTable[j, i] = double.MinValue; 
                }
            }
        }

        public void Render()
        {
            zTable = new double[DBitmap.Width, DBitmap.Height];
            ResetZTable();
            var pMatrix = ProjectionMatrix;
            var vMatrix = SelectedCamera.ViewMatrix;
            var v = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 1 });
            foreach (var shape in Shapes)
            {
                shape.IsRender = true;
                var mMatrix = shape.ModelMatrix;
                var vmMatrix = vMatrix.Multiply(mMatrix);
                foreach (var side in shape.SideTriangles)
                {                    
                    side.Process(vmMatrix);
                    var n = side.Normal;
                    if (n[0]*v[0] + n[1]*v[1] + n[2]*v[2] <= 0)
                    {
                        side.Process(pMatrix, false);
                        side.DrawSide(DBitmap, DBitmap.Width / 2, zTable);
                    }
                }
                shape.IsRender = false;
            }
        }

        public static Engine CreateEngine(DirectBitmap bitmap)
        {
            var engine = new Engine(bitmap);
            engine.Near = 1;
            engine.Far = 100;
            engine.Fov = 45;
            return engine;
        }
    }
}
