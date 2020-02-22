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
            this.Icon = new Icon(@"../../flashlightIcon.ico");

            InitializeComponent();
            DBitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            RenderEngine = Engine.CreateEngine(DBitmap);

            //shapes
            Cube = Shape.CreateCube();

            Sphere = Shape.CreateSphere(2, Color.Blue);
            Sphere.Translate(sphereX, 0, sphereZ);

            FlashLight = Shape.CreateFlashLight(15, 0.5, 0.2, 0.3, Color.Red);

            Wall = Shape.CreateCube();
            Wall.Scale(2, 1, 0.25);
            Wall.Translate(0, 0, wallDistance);
            Wall.Rotate(Axis.Y, Math.PI * wallRotation);

            RenderEngine.Shapes.Add(FlashLight);
            RenderEngine.Shapes.Add(Cube);
            RenderEngine.Shapes.Add(Sphere);
            RenderEngine.Shapes.Add(Wall);

            //lights
            GlobalLight = new Light()
            {
                IsSpotLight = false,
                Position = Helpers.BuildVector(5, 5, 0),
                IsOn = true
            };

            SpotLight = new Light()
            {
                IsSpotLight = true,
                Position = Helpers.BuildVector(0, 0, 0),
                IsOn = false,
                Direction = Helpers.BuildVector(0, 0, 1),
                P = 17
            };

            RenderEngine.Lights.Add(GlobalLight);
            RenderEngine.Lights.Add(SpotLight);
            //cameras
            GlobalCamera = new Camera(new Vector3(0f, 20f, 0f), new Vector3(0, 0, 0), new Vector3(0, 0, -1));
            BehindCamera = new Camera(new Vector3(0, (float)behindHeight, (float)(-behindDistance)), new Vector3(0, 0, (float)targetDistance), new Vector3(0, 0, -1));
            FollowingCamera = new Camera(new Vector3(0f, 10f, -10f), new Vector3(0, 0, 0), new Vector3(0, -1, 0));

            RenderEngine.SelectedCamera = GlobalCamera;

            pictureBox1.Image = DBitmap.Bitmap;
        }

        public DirectBitmap DBitmap;
        public Engine RenderEngine { get; set; }

        //shapes
        public Shape Cube;

        public Shape Sphere;

        public Shape FlashLight;

        public Shape Wall;

        //cameras
        public Camera GlobalCamera;

        public Camera BehindCamera;

        public Camera FollowingCamera;

        public double targetDistance = 7;

        public double behindDistance = 4;

        public double behindHeight = 2;

        //lights
        public Light GlobalLight;

        public Light SpotLight;

        //shapes positions
        public double cubeAngle = 0;
        public double cubeShift = 0;
        public double cubeDistance = 6;
        public double cubeRange = 4;
        public double cubeChange = 0.05;

        public double sphereX = 2;
        public double sphereZ = -3;

        public double flashAngle = 0; // <-1, 1>

        public double wallDistance = 6;
        public double wallRotation = -0.6;

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveCube();
            SetBehindCamera(flashAngle, behindDistance, targetDistance);
            SetFollowingCamera();
            RotateFlashLight(flashAngle);
            DBitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            RenderEngine.DBitmap = DBitmap;
            RenderEngine.Render();
            pictureBox1.Image = RenderEngine.DBitmap.Bitmap;

        }

        private void MoveCube()
        {
            Cube.ResetModel();
            Cube.Rotate(Axis.Y, cubeAngle);
            cubeAngle += Math.PI / 60;
            cubeAngle = cubeAngle % (2 * Math.PI);
            Cube.Translate(cubeShift, 0, cubeDistance);

            cubeShift = cubeShift + cubeChange;
            if (Math.Abs(cubeShift) >= cubeRange)
                cubeChange *= -1;
        }

        private void SetBehindCamera(double angel, double back, double target)
        {
            var xV = Math.Sin(angel * Math.PI);
            var zV = Math.Cos(angel * Math.PI);

            BehindCamera.Position = Helpers.BuildVector((-back * xV), behindHeight, (-back * zV));
            BehindCamera.Target = Helpers.BuildVector((target * xV), 0, (target * zV));
            BehindCamera.UpVector = Helpers.BuildVector(-xV, 0, -zV).Normalize(2);
        }

        private void RotateFlashLight(double angle)
        {
            var xV = Math.Sin(angle * Math.PI);
            var zV = Math.Cos(angle * Math.PI);

            FlashLight.ResetModel();
            FlashLight.Rotate(Axis.Y, Math.PI * angle);
            SpotLight.Direction = Helpers.BuildVector(xV, 0, zV);
        }

        private void SetFollowingCamera()
        {
            FollowingCamera.Target = Helpers.BuildVector(cubeShift, 0, cubeDistance);//0f, 10f, -10f
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

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            var bar = sender as TrackBar;
            var value = (bar.Value - 50) / 50.0;
            flashAngle = value;
        }

        private void globalCameraRbutton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    RenderEngine.SelectedCamera = GlobalCamera;
                }
            }
        }

        private void behindCameraRButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    RenderEngine.SelectedCamera = BehindCamera;
                }
            }
        }

        private void followingCamerarButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    RenderEngine.SelectedCamera = FollowingCamera;
                }
            }
        }

        private void globalLightCB_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if(cb != null)
            {
                GlobalLight.IsOn = cb.Checked;
            }
           
        }

        private void torchLightCB_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb != null)
            {
                SpotLight.IsOn = cb.Checked;
                if(cb.Checked)
                {
                    FlashLight.ChangeColor(Color.White, true);
                }
                else
                {
                    FlashLight.ChangeColor(FlashLight.PrimaryColor, false);
                }
            }
        }
    }
}
