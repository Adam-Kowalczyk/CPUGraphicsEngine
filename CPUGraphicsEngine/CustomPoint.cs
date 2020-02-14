using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUGraphicsEngine
{
    public class CustomPoint
    {
        public CustomPoint(double x, double y, double z)
        {
            xInit = x;
            yInit = y;
            zInit = z;
        }

        public bool IsRender
        {
            get { return isRender; }
            set
            {
                if(value && !isRender)
                {
                    xRender = xInit;
                    yRender = yInit;
                    zRender = zInit;

                }
                isRender = value;
            }
        }
        bool isRender = false;
        public double X {
            get
            {
                if (IsRender)
                {
                    return xRender;
                }
                else
                {
                    return xInit;
                }
            }
        }
        double xInit;
        double xRender;
        public double Y
        {
            get
            {
                if (IsRender)
                {
                    return yRender;
                }
                else
                {
                    return yInit;
                }
            }
        }
        double yInit;
        double yRender;
        public double Z
        {
            get
            {
                if (IsRender)
                {
                    return zRender;
                }
                else
                {
                    return zInit;
                }
            }
        }
        double zInit;
        double zRender;
        public Vector<double> Vector
        {
            get
            {
                return Vector<double>.Build.DenseOfArray(new double[] { X, Y, Z, 1 });
            }
        }

        public Vector<double> Normal { get; set; }

        public Vector<double> ProcessedNormal { get; set; }

        public void Process(Matrix<double> matrix, bool useNormals = true)
        {
            if (!IsRender)
                return;

            var outcome = matrix.Multiply(Vector);
            outcome = outcome.Multiply(1 / outcome.At(3));
            xRender = outcome.At(0);
            yRender = outcome.At(1);
            zRender = outcome.At(2);

            if(useNormals && Normal!= null)
            {
                var pNormal = matrix.Inverse().TransposeThisAndMultiply(Normal).Normalize(2);
                ProcessedNormal = pNormal;
            }

        }
    }
}
