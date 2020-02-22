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

        public Color PrimaryColor { get; set; }
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

        public void Scale(double x, double y, double z)
        {
            var scaleMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                                        {x, 0, 0, 0 },
                                        {0, y, 0, 0 },
                                        {0, 0, z, 0 },
                                        {0, 0, 0, 1 }});
            ModelMatrix = scaleMatrix.Multiply(ModelMatrix);
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
            for (int i = 0; i < triangles.Length; i += 3)
            {
                shape.SideTriangles.Add(new SideTriangle(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]],
                    normals[i / 6], normals[i / 6], normals[i / 6])
                {
                    paintColor = color,
                });
            }
            return shape;
        }

        static Random randomizer = new Random();

        static Color GetRandomColor()
        {
            return Color.FromArgb(randomizer.Next(0, 255), randomizer.Next(0, 255), randomizer.Next(0, 255));
        }

        public static Shape CreateSphere(int recurtionLevel, Color color)
        {
            var factory = new IcoSphereFactory();
            var faces = factory.Create(recurtionLevel);
            var sphere = new Shape();
            foreach (var face in faces)
            {
                var side = new SideTriangle(face.V1, face.V2, face.V3, Vector3.Normalize(face.V1), Vector3.Normalize(face.V2), Vector3.Normalize(face.V3))
                {
                    paintColor = color
                };
                sphere.SideTriangles.Add(side);
            }
            return sphere;
        }


        public static Shape CreateFlashLight(int devisionNumber, double headLength, double handleRadius, double headRadius, Color color)
        {
            var flash = new Shape() { PrimaryColor = color };
            var angleDifference = 2 * Math.PI / devisionNumber;
            double end = -1;
            double connector = 1 - headLength;
            double beginning = 1;
            for (int i = 0; i < devisionNumber; i++)
            {
                var firstAngle = angleDifference * i - Math.PI;
                var secondAngle = angleDifference * (i + 1) - Math.PI;

                var firstY = handleRadius * Math.Cos(firstAngle);
                var firstX = handleRadius * Math.Sin(firstAngle);

                var secondY = handleRadius * Math.Cos(secondAngle);
                var secondX = handleRadius * Math.Sin(secondAngle);

                var endSide = new SideTriangle(new Vector3(0, 0, (float)end),
                    new Vector3((float)firstX, (float)firstY, (float)end),
                    new Vector3((float)secondX, (float)secondY, (float)end),
                    new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1))
                { paintColor = color };
                flash.SideTriangles.Add(endSide);

                var longSide1 = new SideTriangle(new Vector3((float)firstX, (float)firstY, (float)end),
                    new Vector3((float)secondX, (float)secondY, (float)end),
                    new Vector3((float)firstX, (float)firstY, (float)connector),
                    Vector3.Normalize(new Vector3((float)firstX, (float)firstY, 0)),
                    Vector3.Normalize(new Vector3((float)secondX, (float)secondY, 0)),
                    Vector3.Normalize(new Vector3((float)firstX, (float)firstY, 0)))
                { paintColor = color };
                flash.SideTriangles.Add(longSide1);

                var longSide2 = new SideTriangle(new Vector3((float)secondX, (float)secondY, (float)end),
                    new Vector3((float)secondX, (float)secondY, (float)connector),
                    new Vector3((float)firstX, (float)firstY, (float)connector),
                    Vector3.Normalize(new Vector3((float)secondX, (float)secondY, 0)),
                    Vector3.Normalize(new Vector3((float)secondX, (float)secondY, 0)),
                    Vector3.Normalize(new Vector3((float)firstX, (float)firstY, 0)))
                { paintColor = color };
                flash.SideTriangles.Add(longSide2);

                var endY1 = headRadius * Math.Cos(firstAngle);
                var endX1 = headRadius * Math.Sin(firstAngle);

                var endY2 = headRadius * Math.Cos(secondAngle);
                var endX2 = headRadius * Math.Sin(secondAngle);

                var heightDiff = headRadius - handleRadius;

                var zS = heightDiff / headLength * headRadius;

                var shortSide1 = new SideTriangle(new Vector3((float)firstX, (float)firstY, (float)connector),
                    new Vector3((float)secondX, (float)secondY, (float)connector),
                    new Vector3((float)endX1, (float)endY1, (float)beginning),
                    Vector3.Normalize(new Vector3((float)endX1, (float)endY1, -(float)zS)),
                    Vector3.Normalize(new Vector3((float)endX2, (float)endY2, -(float)zS)),
                    Vector3.Normalize(new Vector3((float)endX1, (float)endY1, -(float)zS)))
                { paintColor = color };
                flash.SideTriangles.Add(shortSide1);

                var shortSide2 = new SideTriangle(new Vector3((float)secondX, (float)secondY, (float)connector),
                    new Vector3((float)endX2, (float)endY2, (float)beginning),
                    new Vector3((float)endX1, (float)endY1, (float)beginning),
                     Vector3.Normalize(new Vector3((float)endX2, (float)endY2, -(float)zS)),
                    Vector3.Normalize(new Vector3((float)endX2, (float)endY2, -(float)zS)),
                    Vector3.Normalize(new Vector3((float)endX1, (float)endY1, -(float)zS)))
                { paintColor = color };
                flash.SideTriangles.Add(shortSide2);

                var opening = new SideTriangle(new Vector3(0, 0, (float)beginning),
                    new Vector3((float)endX2, (float)endY2, (float)beginning),
                    new Vector3((float)endX1, (float)endY1, (float)beginning),
                    new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1))
                { paintColor = color, ChangeColor = true };
                flash.SideTriangles.Add(opening);
            }
            return flash;

        }

        public void ChangeColor(Color color, bool setGlow)
        {
            foreach(var side in SideTriangles)
            {
                if(side.ChangeColor)
                {
                    side.paintColor = color;
                    side.IsGlowing = setGlow;
                }
            }
        }

    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
