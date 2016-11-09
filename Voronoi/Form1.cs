using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voronoi {
  public partial class Form1 : Form {
    Bitmap image;
    OpenFileDialog ofd;
    VoronoiController controller;

    Timer timer;

    int pointNumber;

    public Form1() {
      InitializeComponent();

      ofd = new OpenFileDialog();

      textBoxPointNumber.Text = "0";
      pointNumber = 0;
      SelectImage(new Bitmap(250, 250), "empty");

      timer = new Timer();
      timer.Interval = 1000 / 60;
      timer.Tick += Update;
      timer.Start();
    }

    void Update(object sender, EventArgs e) {
      if (controller.modified) {
        UpdatePicture();
        controller.modified = false;
      }
    }

    void UpdatePicture() {
      Bitmap bmp = (Bitmap)pictureBox.Image;
      bmp.Dispose();
      bmp = new Bitmap(image.Size.Width, image.Size.Height);
      VoronoiFilter.Apply(
        image,
        bmp,
        controller.points.ToArray(),
        controller.info.ToArray());
      /*
      Graphics g = Graphics.FromImage(bmp);
      for (int i = 0; i < controller.points.Count; ++i) {
        float x = controller.points[i].X;
        float y = controller.points[i].Y;

        SolidBrush brush = new SolidBrush(Color.Gray);
        g.FillRectangle(brush, x - 7, y - 7, 14, 14);
      }
      //*/

      pictureBox.Image = bmp;
    }

    void SelectImage(Bitmap bmp, string name) {
      labelSelectedImage.Text = name;
      Initialize(bmp);

      pictureBox.Size = new Size(image.Width, image.Height);
      this.ClientSize = new Size(
        12 + pictureBox.Width + 6,
        12 + buttonOFD.Size.Height + 6 + buttonReset.Height + 12 + pictureBox.Height + 6);
    }

    void Initialize(Bitmap bmp) {
      image = (Bitmap)bmp.Clone();
      pictureBox.Image = (Bitmap)bmp.Clone();

      controller = new VoronoiController(image.Width, image.Height);

      pointNumber = int.Parse(textBoxPointNumber.Text);
      GenerateRandomPoints();
    }

    void GenerateRandomPoints() {
      int width = image.Size.Width;
      int height = image.Size.Height;
      Random random = new Random();
      for (int i = 0; i < pointNumber; ++i) {
        int x = random.Next(width);
        int y = random.Next(height);
        controller.CreatePoint(x, y);
      }
    }

    private void buttonOFD_Click(object sender, EventArgs e) {
      if (ofd.ShowDialog() == DialogResult.OK) {
        SelectImage(new Bitmap(Image.FromFile(ofd.FileName)),
          ofd.SafeFileName);
      }
    }

    private void pictureBox_MouseDown(object sender, MouseEventArgs e) {
      int type;
      if (e.Button == MouseButtons.Left)
        type = 0;
      else
        type = 1;

      controller.MouseDown(
        type,
        e.Location.X,
        e.Location.Y);
    }

    private void pictureBox_MouseUp(object sender, MouseEventArgs e) {
      controller.MouseUp();
    }

    private void pictureBox_MouseMove(object sender, MouseEventArgs e) {
      controller.MouseMove(
        e.Location.X,
        e.Location.Y);
    }

    private void buttonReset_Click(object sender, EventArgs e) {
      Initialize((Bitmap)image);
    }

    private void buttonSave_Click(object sender, EventArgs e) {
      Image image = pictureBox.Image;
      image.Save(labelSelectedImage.Text + "voronoi-" + controller.points.Count.ToString() + ".bmp");
    }
  }
}
