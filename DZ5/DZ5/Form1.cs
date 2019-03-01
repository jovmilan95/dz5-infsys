using DZ5.Shapes;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ5
{

    public partial class Form1 : Form
    {
        public Image<Bgr, Byte> Image { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ofdLoadImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var fileName = ofdLoadImage.FileName;
                this.Image = new Image<Bgr, byte>(fileName);
                this.pictureBox1.Image = this.Image.Bitmap;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxShape.DisplayMember = "Text";
            cbxShape.ValueMember = "Value";
            this.cbxShape.Items.Add(new { Text = "Triangle", Value = new ShapeDetection(Shape.Triangle) });
            this.cbxShape.Items.Add(new { Text = "Rectangle", Value = new ShapeDetection(Shape.Rectangle) });
            this.cbxShape.Items.Add(new { Text = "Hexagon", Value = new ShapeDetection(Shape.Hexagon) });
            this.cbxShape.Items.Add(new { Text = "Circle", Value = new ShapeDetection(Shape.Circle) });

            cbxColor.DisplayMember = "Text";
            cbxColor.ValueMember = "Value";
            this.cbxColor.Items.Add(new { Text = "Red", Value = new ColorDetection(1) });
            this.cbxColor.Items.Add(new { Text = "Blue", Value = new ColorDetection(2) });
            this.cbxColor.Items.Add(new { Text = "Green", Value = new ColorDetection(3) });

        }

        private void btnDetection_Click(object sender, EventArgs e)
        {
            var colordet = (this.cbxColor.SelectedItem as dynamic).Value as ColorDetection;
            var shapedet = (this.cbxShape.SelectedItem as dynamic).Value as ShapeDetection;
            var area = Decimal.ToInt32(this.numSurface.Value);
            
            this.pictureBox1.Image = shapedet.Detect(colordet, this.Image, area).Bitmap;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = new ShapeDetection(Shape.AllShapes).Detect(new ColorDetection(0), this.Image, 0).Bitmap;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var brightValue = tbarBrightness.Value;
            var contrastValue = tbarContrast.Value;
            this.pictureBox1.Image = this.Image
                                        .Mul(contrastValue / 10.0)
                                        .Add(new Bgr(brightValue, brightValue, brightValue))
                                        .Bitmap;
        }
    }
}