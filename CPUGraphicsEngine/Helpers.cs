using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUGraphicsEngine
{
    public static class Helpers
    {
        public static void Fill(DirectBitmap bitmap, List<Point> points, Color color, List<double> zs, double[,] Ztable)
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
                        var w0 = ((float)(points[1].Y - points[2].Y) * (j - points[2].X) + (float)(points[2].X - points[1].X) * (y - points[2].Y)) / ((float)(points[1].Y - points[2].Y) * (points[0].X - points[2].X) + (float)(points[2].X - points[1].X) * (points[0].Y - points[2].Y));
                        var w1 = ((float)(points[2].Y - points[0].Y) * (j - points[2].X) + (float)(points[0].X - points[2].X) * (y - points[2].Y)) / ((float)(points[1].Y - points[2].Y) * (points[0].X - points[2].X) + (float)(points[2].X - points[1].X) * (points[0].Y - points[2].Y));

                        var w2 = 1 - w0 - w1;

                        var z = zs[0] * w0 + zs[1] * w1 + zs[2] * w2;

                        if (z > Ztable[j, y])
                        {
                            Ztable[j, y] = z;
                            bitmap.SetPixel(j, y, color);
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


    }
    public class EdgeStruct
    {
        public int YMax;
        public double X;
        public double Slope;
    }
}
