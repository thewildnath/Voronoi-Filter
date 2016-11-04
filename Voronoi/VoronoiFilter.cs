using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class VoronoiFilter {

    

    public static Bitmap Apply(Bitmap original, Point[] points, PolygonFilterInfo[] info) {
      return original;

      Voronoi voronoi = new Voronoi(original.Width, original.Height);
      Bitmap bmp = new Bitmap(original.Size.Width, original.Size.Height);
      Graphics g = Graphics.FromImage(bmp);

      for (int i = 0; i < points.Length; ++i) {
        Point[] polygon = voronoi.GetPolygon(i, points);

        SolidBrush brush = new SolidBrush(info[i].color);
        g.FillPolygon(brush, polygon);
      }

      
      return bmp;
    }
  }
}
