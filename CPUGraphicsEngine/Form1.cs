using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUGraphicsEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DBitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            RenderEngine = Engine.CreateEngine(DBitmap);
            Cube = Shape.CreateCube();
            RenderEngine.Shapes.Add(Cube);
            var cam = new Camera(new Vector3(20f, 10f, 0f), new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            //cam.XAxis = new Vector3(0, 0, 1);
            //cam.YAxis = new Vector3(1, 0, 0);
            //cam.ZAxis = new Vector3(0, 1, 0);
            //cam.Position = new Vector3(0f, 0f, -8);
            RenderEngine.SelectedCamera = cam;
            pictureBox1.Image = DBitmap.Bitmap;
            var gfx = Graphics.FromImage(pictureBox1.Image);
            gfx.DrawLine(new Pen(Brushes.Red), 0, 0, pictureBox1.Width - 1, 0);
            gfx.DrawLine(new Pen(Brushes.Red), 0, 0, 0, pictureBox1.Height -1);
            gfx.DrawLine(new Pen(Brushes.Red), 0, pictureBox1.Height -1 , pictureBox1.Width - 1, pictureBox1.Height - 1);
            gfx.DrawLine(new Pen(Brushes.Red), pictureBox1.Width - 1 ,0, pictureBox1.Width - 1, pictureBox1.Height -1);
        }

        public DirectBitmap DBitmap;
        public Engine RenderEngine { get; set; }

        public Shape Cube;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cube.Rotate(Axis.X, Math.PI / 60);
            DBitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            RenderEngine.DBitmap = DBitmap;
            RenderEngine.Render();
            pictureBox1.Image = RenderEngine.DBitmap.Bitmap;
            var gfx = Graphics.FromImage(pictureBox1.Image);
            gfx.DrawLine(new Pen(Brushes.Red), 0, 0, pictureBox1.Width - 1, 0);
            gfx.DrawLine(new Pen(Brushes.Red), 0, 0, 0, pictureBox1.Height - 1);
            gfx.DrawLine(new Pen(Brushes.Red), 0, pictureBox1.Height - 1, pictureBox1.Width - 1, pictureBox1.Height - 1);
            gfx.DrawLine(new Pen(Brushes.Red), pictureBox1.Width - 1, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);

        }
    }
}
