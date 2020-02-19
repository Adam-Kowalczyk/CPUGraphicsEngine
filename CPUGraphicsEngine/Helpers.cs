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
    public static class Helpers
    {
        public static void Fill(DirectBitmap bitmap, List<Point> points, Color color, List<double> zs, double[,] Ztable,
            List<Vector<double>> positions, List<Vector<double>> normals, SurfaceInfo surface, List<Light> lights, ShadingMode shadingMode = ShadingMode.Flat)
        {
            var maxY = points.Max(x => x.Y);
            var minY = points.Min(x => x.Y);
            var etTable = new List<EdgeStruct>[maxY - minY + 1];
            for (int i = 0; i < points.Count; i++)
            {
                var p1 = points[i];
                var p2 = points[i + 1 == points.Count ? 0 : i + 1];
                if (p1.Y == p2.Y) continue;
                if (p1.Y > p2.Y)
                {
                    var tmp = p2;
                    p2 = p1;
                    p1 = tmp;
                }
                //double slp;
                var str = new EdgeStruct { YMax = p2.Y, X = p1.Y < p2.Y ? p1.X : p2.X, Slope = (double)(p1.X - p2.X) / (p1.Y - p2.Y) };
                if (etTable[p1.Y - minY] == null)
                {
                    etTable[p1.Y - minY] = new List<EdgeStruct>();
                }
                etTable[p1.Y - minY].Add(str);
            }

            var aetTable = new List<EdgeStruct>();
            int y = minY;

            Color shadingCol = Color.White;

            if (shadingMode == ShadingMode.Flat)
            {
                var middle = positions[0].Add(positions[1]).Add(positions[2]).Multiply(1.0 / 3);
                var sideNormal = normals[0].Add(normals[1]).Add(normals[2]).Normalize(2);
                shadingCol = PhongeIllumination(middle, sideNormal, Color2ColorInfo(color), surface, lights);

            }

            var firstColor = Color.Blue;
            var secondColor = Color.Red;
            var thirdColor = Color.Green;

            if (shadingMode == ShadingMode.Gouraud)
            {
                firstColor = PhongeIllumination(positions[0], normals[0], Color2ColorInfo(color), surface, lights);
                secondColor = PhongeIllumination(positions[1], normals[1], Color2ColorInfo(color), surface, lights);
                thirdColor = PhongeIllumination(positions[2], normals[2], Color2ColorInfo(color), surface, lights);
            }

            while (y <= maxY)
            {
                if (etTable[y - minY] != null)
                {
                    var tempList = etTable[y - minY];
                    //foreach (var edge in tempList)
                    //{
                    //    //if(edge.Slope < 0)
                    //        edge.X = edge.X - (edge.YMax - y) * edge.Slope;
                    //}

                    aetTable.AddRange(tempList);
                }

                aetTable = aetTable.OrderBy(x => x.X).ToList();
                for (int i = 0; i < aetTable.Count; i += 2)
                {
                    if (aetTable.Count - i == 1)
                    {
                        continue;
                    }
                    var first = aetTable[i];
                    var second = aetTable[i + 1];

                    for (int j = (int)(first.X); j < second.X; j++)
                    {
                        if (!(j >= 0 && j < bitmap.Width && y >= 0 && y < bitmap.Height)) continue;
                        //var w0 = ((float)(points[1].Y - points[2].Y) * (j - points[2].X) + (float)(points[2].X - points[1].X) * (y - points[2].Y)) / ((float)(points[1].Y - points[2].Y) * (points[0].X - points[2].X) + (float)(points[2].X - points[1].X) * (points[0].Y - points[2].Y));
                        //var w1 = ((float)(points[2].Y - points[0].Y) * (j - points[2].X) + (float)(points[0].X - points[2].X) * (y - points[2].Y)) / ((float)(points[1].Y - points[2].Y) * (points[0].X - points[2].X) + (float)(points[2].X - points[1].X) * (points[0].Y - points[2].Y));

                        //var w2 = 1 - w0 - w1;

                        var w = GetBaricentricRatio(j, y, points[0], points[1], points[2]);

                        if (w.Item1 < 0 || w.Item2 < 0 || w.Item3 < 0) continue; //error in filling algorithm?

                        var z = zs[0] * w.Item1 + zs[1] * w.Item2 + zs[2] * w.Item3;

                        if (z < Ztable[j, y])
                        {
                            Ztable[j, y] = z;

                            if (shadingMode == ShadingMode.Gouraud)
                            {
                                var gR = firstColor.R * w.Item1 + secondColor.R * w.Item2 + thirdColor.R * w.Item3;
                                var gG = firstColor.G * w.Item1 + secondColor.G * w.Item2 + thirdColor.G * w.Item3;
                                var gB = firstColor.B * w.Item1 + secondColor.B * w.Item2 + thirdColor.B * w.Item3;

                                shadingCol = Color.FromArgb((int)gR, (int)gG, (int)gB);
                            }

                            if(shadingMode == ShadingMode.Phong)
                            {
                                var normal = normals[0] * w.Item1 + normals[1] * w.Item2 + normals[2] * w.Item3;
                                normal = normal.Normalize(2);
                                var position = positions[0] * w.Item1 + positions[1] * w.Item2 + positions[2] * w.Item3;
                                shadingCol = PhongeIllumination(position, normal, Color2ColorInfo(color), surface, lights);
                            }

                            bitmap.SetPixel(j, y, shadingCol);
                        }

                        //pixels[j, y] = (cr, cg, cb);
                    }
                }
                foreach (var edge in aetTable.ToList())
                {
                    if (edge.YMax == y + 1)
                    {
                        aetTable.Remove(edge);
                    }
                    else
                    {
                        edge.X += edge.Slope;
                    }
                }
                y++;
            }
        }

        public static Vector<double> CrossProduct(Vector<double> a, Vector<double> b)
        {
            return Vector<double>.Build.DenseOfArray(new double[] {
                a.At(1) * b.At(2) - a.At(2) * b.At(1),
                a.At(2) * b.At(0) - a.At(0) * b.At(2),
                a.At(0) * b.At(1) - a.At(1) * b.At(0),
            });
        }

        public static Vector<double> BuildNormal(Vector3 nv, Vector3 v)
        {
            var normal = Vector<double>.Build.DenseOfArray(new double[] { nv.X, nv.Y, nv.Z, -(nv.X * v.X + nv.Y * v.Y + nv.Z * v.Z) });
            return normal;
        }

        public static Color PhongeIllumination(Vector<double> position, Vector<double> normal,
            ColorInfo colorInfo, SurfaceInfo surface, List<Light> lights)
        {
            ColorInfo outcome = new ColorInfo { R = 0, G = 0, B = 0 };


            //ambient
            outcome.R += colorInfo.R * surface.Ka;
            outcome.G += colorInfo.G * surface.Ka;
            outcome.B += colorInfo.B * surface.Ka;

            foreach (var light in lights)
            {
                //diffuse
                var l = light.CalculateLVector(position);
                var lightNormalAngle = normal.DotProduct(l);
                var diffuseR = surface.Kd * lightNormalAngle;
                if (lightNormalAngle < 0) continue;
                outcome.R += colorInfo.R * diffuseR;
                outcome.G += colorInfo.G * diffuseR;
                outcome.B += colorInfo.B * diffuseR;

                //sepcular
                var v = (-position).Normalize(2);

                var r = 2 * lightNormalAngle * normal - l;
                r = r.Normalize(2);

                var cameraRAngle = v.DotProduct(r);

                var specularR = surface.Ks * Math.Pow(Math.Cos(cameraRAngle), surface.N_shiny);

                outcome.R += colorInfo.R * specularR;
                outcome.G += colorInfo.G * specularR;
                outcome.B += colorInfo.B * specularR;
            }

            if (outcome.R > 1) outcome.R = 1;
            if (outcome.G > 1) outcome.G = 1;
            if (outcome.B > 1) outcome.B = 1;

            return ColorInfo2Color(outcome);
        }

        public static ColorInfo Color2ColorInfo(Color color)
        {
            return new ColorInfo { R = color.R / 255.0, G = color.G / 255.0, B = color.B / 255.0 };
        }

        public static Color ColorInfo2Color(ColorInfo color)
        {
            return Color.FromArgb((int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));
        }

        public static Vector<double> BuildVector(double x, double y, double z, double? n = null)
        {
            if (n == null)
            {
                return Vector<double>.Build.DenseOfArray(new double[] { x, y, z });
            }
            else
            {
                return Vector<double>.Build.DenseOfArray(new double[] { x, y, z, n.Value });
            }
        }

        public static (double, double, double) GetBaricentricRatio(int pX, int pY, Point a, Point b, Point c)
        {
            double den = 1.0 / ((b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y)); //to można liczyć przed!
            double first = ((b.Y - c.Y) * (pX - c.X) + (c.X - b.X) * (pY - c.Y)) * den;
            double second = ((c.Y - a.Y) * (pX - c.X) + (a.X - c.X) * (pY - c.Y)) * den;
            double third = 1 - first - second;
            return (first, second, third);
        }
    }
    public class EdgeStruct
    {
        public int YMax;
        public double X;
        public double Slope;
    }

    public struct ColorInfo
    {
        public double R;
        public double G;
        public double B;

    }

    public struct SurfaceInfo
    {
        public double Ka;
        public double Kd;
        public double Ks;
        public int N_shiny;
    }

    public enum ShadingMode
    {
        Flat,
        Gouraud,
        Phong,
    }

}
