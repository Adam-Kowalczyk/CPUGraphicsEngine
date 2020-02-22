using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUGraphicsEngine
{
    public class Light
    {
        //todo: process it!
        public Vector<double> Position { get; set; }

        public Vector<double> ProcessedPosition { get; set; }

        public Vector<double> Direction { get; set; }

        public Vector<double> ProcessedDirection { get; set; }

        public bool IsSpotLight { get; set; }

        public int P { get; set; }

        public bool IsOn { get; set; } = false;

        public Vector<double> CalculateLVector(Vector<double> vertexPosition)
        {
            return ProcessedPosition.Subtract(vertexPosition).Normalize(2);
        }

        public void Process(Matrix<double> matrix)
        {
            var pos = Helpers.BuildVector(Position[0], Position[1], Position[2], 1);
            var calc = matrix.Multiply(pos);
            ProcessedPosition = Helpers.BuildVector(calc[0], calc[1], calc[2]);

            if(Direction != null)
            {
                var direct = Helpers.BuildVector(Direction[0], Direction[1], Direction[2],
                    -(Direction[0] * Position[0] + Direction[1] * Position[1] + Direction[2] * Position[2]));
                var pDirect = matrix.Inverse().TransposeThisAndMultiply(direct);
                ProcessedDirection = Helpers.BuildVector(pDirect[0], pDirect[1], pDirect[2]).Normalize(2);
            }

        }
    }
}
