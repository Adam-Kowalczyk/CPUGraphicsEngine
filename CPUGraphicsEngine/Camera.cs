using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPUGraphicsEngine
{
    public class Camera
    {
        public Camera(Vector3 position, Vector3 target, Vector3 upVector)
        {
            Position = Vector<double>.Build.DenseOfArray(new double[] { position.X, position.Y, position.Z });
            Target = Vector<double>.Build.DenseOfArray(new double[] { target.X, target.Y, target.Z });
            UpVector = Vector<double>.Build.DenseOfArray(new double[] { upVector.X, upVector.Y, upVector.Z });

        }

        public Vector<double> Position { get; set; }

        public Vector<double> Target { get; set; }

        public Vector<double> UpVector { get; set; }
        public Vector<double> XAxis
        {
            get
            {
                return Helpers.CrossProduct(UpVector, ZAxis).Normalize(2);
            }
        }
        public Vector<double> YAxis
        {
            get
            {
                return Helpers.CrossProduct(ZAxis, XAxis).Normalize(2);
            }
        }
        public Vector<double> ZAxis
        {
            get
            {
                return Position.Subtract(Target).Normalize(2);
            }
        }

        public Matrix<double> ViewMatrix { 
            get
            {
                var xa = XAxis;
                var za = ZAxis;
                var ya = YAxis;
                var matrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {xa.At(0), ya.At(0), za.At(0), Position.At(0) },
                                        {xa.At(1), ya.At(1), za.At(1), Position.At(1) },
                                        {xa.At(2), ya.At(2), za.At(2), Position.At(2) },
                                        {0, 0, 0, 1 }});
                return matrix.Inverse();
            }
        }
    }
}
