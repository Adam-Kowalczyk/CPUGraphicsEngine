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
            //Cube = Shape.CreateCube();
            Cube = Shape.CreateSphere(1, Color.Blue);

            RenderEngine.Shapes.Add(Cube);
            var cam = new Camera(new Vector3(20f, 10f, 0f), new Vector3(0, 0, 0), new Vector3(0, 0, 1));

            var light = new Light() { Color = Color.White, IsSpotLight = false, 
                Position = Vector<double>.Build.DenseOfArray(new double[]{ 10, 5 , 5 })};
            //cam.XAxis = new Vector3(0, 0, 1);
            //cam.YAxis = new Vector3(1, 0, 0);
            //cam.ZAxis = new Vector3(0, 1, 0);
            //cam.Position = new Vector3(0f, 0f, -8);
            RenderEngine.SelectedCamera = cam;
            RenderEngine.Lights.Add(light);
            pictureBox1.Image = DBitmap.Bitmap;
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

        }

        private void fovTrackBar_Scroll(object sender, EventArgs e)
        {
            var bar = sender as TrackBar;
            fovLabel.Text = "Fov: " + bar.Value.ToString();
            RenderEngine.Fov = bar.Value;
        }

        private void ambientTrackBar_Scroll(object sender, EventArgs e)
        {
            var bar = sender as TrackBar;
            var value = bar.Value / 100.0;
            ambientLabel.Text = "Ambient: " + value.ToString();
            RenderEngine.Ka = value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var bar = sender as TrackBar;
            var value = bar.Value / 100.0;
            diffuseLabel.Text = "Diffuse: " + value.ToString();
            RenderEngine.Kd = value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            var bar = sender as TrackBar;
            var value = bar.Value / 100.0;
            specularLabel.Text = "Specular: " + value.ToString();
            RenderEngine.Ks = value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            var bar = sender as TrackBar;
            var value = bar.Value;
            nShinyLabel.Text = "Shininess: " + value.ToString();
            RenderEngine.N_shiny = value;
        }

        private void flatRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    RenderEngine.ShadingMode = ShadingMode.Flat;
                }
            }
        }

        private void gouraudRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    RenderEngine.ShadingMode = ShadingMode.Gouraud;
                }
            }
        }

        private void phongRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    RenderEngine.ShadingMode = ShadingMode.Phong;
                }
            }
        }
    }
}
