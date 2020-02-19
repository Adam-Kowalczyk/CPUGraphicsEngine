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
    public class Shape
    {

        public Matrix<double> ModelMatrix { get; set; } = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {1, 0, 0, 0 },
                                        {0, 1, 0, 0 },
                                        {0, 0, 1, 0 },
                                        {0, 0, 0, 1 }});
        public List<SideTriangle> SideTriangles { get; set; } = new List<SideTriangle>();

        public bool IsRender
        {
            get { return isRender; }
            set
            {
                foreach (var side in SideTriangles)
                {
                    side.IsRender = value;
                }
                isRender = value;
            }
        }
        bool isRender = false;

        public void Rotate(Axis axis, double angle)
        {
            Matrix<double> rotationMatrix;
            switch (axis)
            {
                case Axis.Z:
                    {
                        rotationMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {Math.Cos(angle), -Math.Sin(angle), 0, 0 },
                                        {Math.Sin(angle), Math.Cos(angle), 0, 0 },
                                        {0, 0, 1, 0 },
                                        {0, 0, 0, 1 }});
                    }
                    break;
                case Axis.Y:
                    {
                        rotationMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {Math.Cos(angle), 0, Math.Sin(angle), 0 },
                                        {0, 1, 0, 0 },
                                        {-Math.Sin(angle), 0, Math.Cos(angle), 0 },
                                        {0, 0, 0, 1 }});
                    }
                    break;
                case Axis.X:
                    {
                        rotationMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {1, 0, 0, 0 },
                                        {0, Math.Cos(angle), -Math.Sin(angle), 0 },
                                        {0, Math.Sin(angle), Math.Cos(angle), 0 },
                                        {0, 0, 0, 1 }});
                    }
                    break;
                default:
                    return;
            }

            ModelMatrix = rotationMatrix.Multiply(ModelMatrix);

        }

        public void ResetModel()
        {
            ModelMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {1, 0, 0, 0 },
                                        {0, 1, 0, 0 },
                                        {0, 0, 1, 0 },
                                        {0, 0, 0, 1 }});
        }

        public void Translate(double x, double y, double z)
        {
            var transltionMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {1, 0, 0, x },
                                        {0, 1, 0, y },
                                        {0, 0, 1, z },
                                        {0, 0, 0, 1 }});
            ModelMatrix = transltionMatrix.Multiply(ModelMatrix);
        }


        public static Shape CreateCube()
        {
            var shape = new Shape();
            Vector3[] vertices = {
                new Vector3 (-1, -1, -1),
                new Vector3 (1, -1, -1),
                new Vector3 (1, 1, -1),
                new Vector3 (-1, 1, -1),
                new Vector3 (-1, 1, 1),
                new Vector3 (1, 1, 1),
                new Vector3 (1, -1, 1),
                new Vector3 (-1, -1, 1),
            };

            Vector3[] normals =
            {
                new Vector3 (0, 0, -1),
                new Vector3 (0, 1, 0),
                new Vector3 (1, 0, 0),
                new Vector3 (-1, 0, 0),
                new Vector3 (0, 0, 1),
                new Vector3 (0, -1, 0),
            };

            int[] triangles = {
                0, 2, 1, //face front
	            0, 3, 2,
                2, 3, 4, //face top
	            2, 4, 5,
                1, 2, 5, //face right
	            1, 5, 6,
                0, 7, 4, //face left
	            0, 4, 3,
                5, 4, 7, //face back
	            5, 7, 6,
                0, 6, 7, //face bottom
	            0, 1, 6
            };

            Color color = GetRandomColor();
            for(int i =0; i < triangles.Length; i+=3)
            {
                //if (i % 6 == 0)
                    //color = GetRandomColor();
                shape.SideTriangles.Add(new SideTriangle(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]],
                    normals[i/6], normals[i / 6], normals[i / 6]) { 
                    paintColor  = color,
                });
            }
            return shape;
        }

        static Random randomizer = new Random();

        static Color GetRandomColor()
        {
            return Color.FromArgb(randomizer.Next(0, 255), randomizer.Next(0, 255), randomizer.Next(0, 255));
        }

    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
