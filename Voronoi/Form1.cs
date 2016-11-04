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

    public Form1() {
      InitializeComponent();

      ofd = new OpenFileDialog();

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
      Bitmap bmp = VoronoiFilter.Apply(
        (Bitmap)image.Clone(),
        controller.points.ToArray(),
        controller.info.ToArray());
        //(Bitmap)image.Clone();
      Graphics g = Graphics.FromImage(bmp);

      for (int i = 0; i < controller.points.Count; ++i) {
        int x = controller.points[i].X;
        int y = controller.points[i].Y;

        SolidBrush brush = new SolidBrush(controller.info[i].color);
        g.FillRectangle(brush, x - 7, y - 7, 14, 14);
      }

      pictureBox.Image = bmp;
    }

    void SelectImage(Bitmap bmp, string name) {
      image = (Bitmap)bmp.Clone();
      pictureBox.Image = image;
      labelSelectedImage.Text = name;

      pictureBox.Size = new Size(image.Width, image.Height);
      this.ClientSize = new Size(
        12 + pictureBox.Width + 6,
        12 + buttonReset.Height + 12 + pictureBox.Height + 6);

      controller = new VoronoiController(image.Width, image.Height);
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
  }
}
