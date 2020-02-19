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
        public Color Color { get; set; }

        //todo: process it!
        public Vector<double> Position { get; set; }

        public Vector<double> ProcessedPosition { get; set; }

        public bool IsSpotLight { get; set; }

        public Vector<double> CalculateLVector(Vector<double> vertexPosition)
        {
            return ProcessedPosition.Subtract(vertexPosition).Normalize(2);
        }

        public void Process(Matrix<double> matrix)
        {
            var pos = Helpers.BuildVector(Position[0], Position[1], Position[2], 1);
            var calc = matrix.Multiply(pos);
            ProcessedPosition = Helpers.BuildVector(calc[0], calc[1], calc[2]);
        }
    }
}
