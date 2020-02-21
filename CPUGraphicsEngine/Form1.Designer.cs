namespace CPUGraphicsEngine
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.phongRadioButton = new System.Windows.Forms.RadioButton();
            this.gouraudRadioButton = new System.Windows.Forms.RadioButton();
            this.flatRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.nShinyLabel = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.specularLabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.diffuseLabel = new System.Windows.Forms.Label();
            this.ambientTrackBar = new System.Windows.Forms.TrackBar();
            this.ambientLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fovLabel = new System.Windows.Forms.Label();
            this.fovTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flashLightRotationLabel = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.globalCameraRbutton = new System.Windows.Forms.RadioButton();
            this.behindCameraRButton = new System.Windows.Forms.RadioButton();
            this.followingCamerarButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ambientTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(849, 542);
            this.splitContainer1.SplitterDistance = 666;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(666, 542);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.phongRadioButton);
            this.groupBox3.Controls.Add(this.gouraudRadioButton);
            this.groupBox3.Controls.Add(this.flatRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(2, 316);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 108);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shading mode";
            // 
            // phongRadioButton
            // 
            this.phongRadioButton.AutoSize = true;
            this.phongRadioButton.Location = new System.Drawing.Point(7, 76);
            this.phongRadioButton.Name = "phongRadioButton";
            this.phongRadioButton.Size = new System.Drawing.Size(70, 21);
            this.phongRadioButton.TabIndex = 2;
            this.phongRadioButton.Text = "Phong";
            this.phongRadioButton.UseVisualStyleBackColor = true;
            this.phongRadioButton.CheckedChanged += new System.EventHandler(this.phongRadioButton_CheckedChanged);
            // 
            // gouraudRadioButton
            // 
            this.gouraudRadioButton.AutoSize = true;
            this.gouraudRadioButton.Location = new System.Drawing.Point(7, 49);
            this.gouraudRadioButton.Name = "gouraudRadioButton";
            this.gouraudRadioButton.Size = new System.Drawing.Size(85, 21);
            this.gouraudRadioButton.TabIndex = 1;
            this.gouraudRadioButton.Text = "Gouraud";
            this.gouraudRadioButton.UseVisualStyleBackColor = true;
            this.gouraudRadioButton.CheckedChanged += new System.EventHandler(this.gouraudRadioButton_CheckedChanged);
            // 
            // flatRadioButton
            // 
            this.flatRadioButton.AutoSize = true;
            this.flatRadioButton.Checked = true;
            this.flatRadioButton.Location = new System.Drawing.Point(7, 22);
            this.flatRadioButton.Name = "flatRadioButton";
            this.flatRadioButton.Size = new System.Drawing.Size(52, 21);
            this.flatRadioButton.TabIndex = 0;
            this.flatRadioButton.TabStop = true;
            this.flatRadioButton.Text = "Flat";
            this.flatRadioButton.UseVisualStyleBackColor = true;
            this.flatRadioButton.CheckedChanged += new System.EventHandler(this.flatRadioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.trackBar3);
            this.groupBox2.Controls.Add(this.nShinyLabel);
            this.groupBox2.Controls.Add(this.trackBar2);
            this.groupBox2.Controls.Add(this.specularLabel);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.diffuseLabel);
            this.groupBox2.Controls.Add(this.ambientTrackBar);
            this.groupBox2.Controls.Add(this.ambientLabel);
            this.groupBox2.Location = new System.Drawing.Point(2, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 164);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Surface Settings";
            // 
            // trackBar3
            // 
            this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar3.AutoSize = false;
            this.trackBar3.Location = new System.Drawing.Point(55, 111);
            this.trackBar3.Minimum = 1;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(119, 37);
            this.trackBar3.TabIndex = 7;
            this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar3.Value = 5;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // nShinyLabel
            // 
            this.nShinyLabel.AutoSize = true;
            this.nShinyLabel.Location = new System.Drawing.Point(2, 111);
            this.nShinyLabel.Name = "nShinyLabel";
            this.nShinyLabel.Size = new System.Drawing.Size(85, 17);
            this.nShinyLabel.TabIndex = 6;
            this.nShinyLabel.Text = "Shininess: 5";
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.AutoSize = false;
            this.trackBar2.Location = new System.Drawing.Point(55, 78);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(119, 30);
            this.trackBar2.TabIndex = 5;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2.Value = 20;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // specularLabel
            // 
            this.specularLabel.AutoSize = true;
            this.specularLabel.Location = new System.Drawing.Point(2, 78);
            this.specularLabel.Name = "specularLabel";
            this.specularLabel.Size = new System.Drawing.Size(92, 17);
            this.specularLabel.TabIndex = 4;
            this.specularLabel.Text = "Specular: 0,2";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(55, 52);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(119, 29);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // diffuseLabel
            // 
            this.diffuseLabel.AutoSize = true;
            this.diffuseLabel.Location = new System.Drawing.Point(2, 52);
            this.diffuseLabel.Name = "diffuseLabel";
            this.diffuseLabel.Size = new System.Drawing.Size(68, 17);
            this.diffuseLabel.TabIndex = 2;
            this.diffuseLabel.Text = "Diffuse: 1";
            // 
            // ambientTrackBar
            // 
            this.ambientTrackBar.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ambientTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ambientTrackBar.AutoSize = false;
            this.ambientTrackBar.Location = new System.Drawing.Point(55, 21);
            this.ambientTrackBar.Maximum = 100;
            this.ambientTrackBar.Name = "ambientTrackBar";
            this.ambientTrackBar.Size = new System.Drawing.Size(119, 28);
            this.ambientTrackBar.TabIndex = 1;
            this.ambientTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ambientTrackBar.Value = 30;
            this.ambientTrackBar.Scroll += new System.EventHandler(this.ambientTrackBar_Scroll);
            // 
            // ambientLabel
            // 
            this.ambientLabel.AutoSize = true;
            this.ambientLabel.Location = new System.Drawing.Point(2, 22);
            this.ambientLabel.Name = "ambientLabel";
            this.ambientLabel.Size = new System.Drawing.Size(87, 17);
            this.ambientLabel.TabIndex = 0;
            this.ambientLabel.Text = "Ambient: 0,3";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.followingCamerarButton);
            this.groupBox1.Controls.Add(this.behindCameraRButton);
            this.groupBox1.Controls.Add(this.globalCameraRbutton);
            this.groupBox1.Controls.Add(this.fovLabel);
            this.groupBox1.Controls.Add(this.fovTrackBar);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 137);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera Settings";
            // 
            // fovLabel
            // 
            this.fovLabel.AutoSize = true;
            this.fovLabel.Location = new System.Drawing.Point(2, 22);
            this.fovLabel.Name = "fovLabel";
            this.fovLabel.Size = new System.Drawing.Size(55, 17);
            this.fovLabel.TabIndex = 1;
            this.fovLabel.Text = "Fov: 45";
            // 
            // fovTrackBar
            // 
            this.fovTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fovTrackBar.AutoSize = false;
            this.fovTrackBar.Location = new System.Drawing.Point(46, 21);
            this.fovTrackBar.Maximum = 90;
            this.fovTrackBar.Minimum = 20;
            this.fovTrackBar.Name = "fovTrackBar";
            this.fovTrackBar.Size = new System.Drawing.Size(128, 28);
            this.fovTrackBar.TabIndex = 0;
            this.fovTrackBar.TickFrequency = 15;
            this.fovTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.fovTrackBar.Value = 45;
            this.fovTrackBar.Scroll += new System.EventHandler(this.fovTrackBar_Scroll);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.trackBar4);
            this.groupBox4.Controls.Add(this.flashLightRotationLabel);
            this.groupBox4.Location = new System.Drawing.Point(2, 430);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(174, 100);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Scene Modifiers";
            // 
            // flashLightRotationLabel
            // 
            this.flashLightRotationLabel.AutoSize = true;
            this.flashLightRotationLabel.Location = new System.Drawing.Point(5, 22);
            this.flashLightRotationLabel.Name = "flashLightRotationLabel";
            this.flashLightRotationLabel.Size = new System.Drawing.Size(101, 17);
            this.flashLightRotationLabel.TabIndex = 0;
            this.flashLightRotationLabel.Text = "Torch rotation:";
            // 
            // trackBar4
            // 
            this.trackBar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar4.AutoSize = false;
            this.trackBar4.Location = new System.Drawing.Point(55, 21);
            this.trackBar4.Maximum = 100;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(119, 30);
            this.trackBar4.TabIndex = 1;
            this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar4.Value = 50;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // globalCameraRbutton
            // 
            this.globalCameraRbutton.AutoSize = true;
            this.globalCameraRbutton.Checked = true;
            this.globalCameraRbutton.Location = new System.Drawing.Point(5, 42);
            this.globalCameraRbutton.Name = "globalCameraRbutton";
            this.globalCameraRbutton.Size = new System.Drawing.Size(121, 21);
            this.globalCameraRbutton.TabIndex = 2;
            this.globalCameraRbutton.TabStop = true;
            this.globalCameraRbutton.Text = "Global camera";
            this.globalCameraRbutton.UseVisualStyleBackColor = true;
            this.globalCameraRbutton.CheckedChanged += new System.EventHandler(this.globalCameraRbutton_CheckedChanged);
            // 
            // behindCameraRButton
            // 
            this.behindCameraRButton.AutoSize = true;
            this.behindCameraRButton.Location = new System.Drawing.Point(5, 69);
            this.behindCameraRButton.Name = "behindCameraRButton";
            this.behindCameraRButton.Size = new System.Drawing.Size(117, 21);
            this.behindCameraRButton.TabIndex = 3;
            this.behindCameraRButton.Text = "Torch camera";
            this.behindCameraRButton.UseVisualStyleBackColor = true;
            this.behindCameraRButton.CheckedChanged += new System.EventHandler(this.behindCameraRButton_CheckedChanged);
            // 
            // followingCamerarButton
            // 
            this.followingCamerarButton.AutoSize = true;
            this.followingCamerarButton.Location = new System.Drawing.Point(5, 96);
            this.followingCamerarButton.Name = "followingCamerarButton";
            this.followingCamerarButton.Size = new System.Drawing.Size(138, 21);
            this.followingCamerarButton.TabIndex = 4;
            this.followingCamerarButton.TabStop = true;
            this.followingCamerarButton.Text = "Following camera";
            this.followingCamerarButton.UseVisualStyleBackColor = true;
            this.followingCamerarButton.CheckedChanged += new System.EventHandler(this.followingCamerarButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 542);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ambientTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar fovTrackBar;
        private System.Windows.Forms.Label fovLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar ambientTrackBar;
        private System.Windows.Forms.Label ambientLabel;
        private System.Windows.Forms.Label diffuseLabel;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label specularLabel;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label nShinyLabel;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton phongRadioButton;
        private System.Windows.Forms.RadioButton gouraudRadioButton;
        private System.Windows.Forms.RadioButton flatRadioButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label flashLightRotationLabel;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.RadioButton behindCameraRButton;
        private System.Windows.Forms.RadioButton globalCameraRbutton;
        private System.Windows.Forms.RadioButton followingCamerarButton;
    }
}

