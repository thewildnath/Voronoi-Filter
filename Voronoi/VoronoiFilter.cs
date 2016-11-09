using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class VoronoiFilter {
    /*
    public static Bitmap Apply(Bitmap original, Bitmap bmp, int n) {
      return original;
    }
    */
    public static Bitmap Apply(Bitmap original, Bitmap bmp, Point[] points, FilterInfo[] info) {
      Voronoi voronoi = new Voronoi(original.Width, original.Height);

      // TODO Threading
      for (int i = 0; i < points.Length; ++i) {
        Point[] polygon = Round(voronoi.GetPolygon(i, ToFloat(points)));
        //PointF[] polygon = voronoi.GetPolygon(i, ToFloat(points));
        PolygonFilter.Apply(original, bmp, polygon, info[i]);
      };

      return bmp;
    }

    static Point[] Round(PointF[] pointsF) {
      Point[] points = new Point[pointsF.Length];
      for (int i = 0; i < points.Length; ++i) {
        points[i] = new Point(
          (int)Math.Round(pointsF[i].X),
          (int)Math.Round(pointsF[i].Y));
      }
      return points;
    }

    static PointF[] ToFloat(Point[] points) {
      PointF[] pointsF = new PointF[points.Length];
      for (int i = 0; i < points.Length; ++i) {
        pointsF[i] = new PointF(
          (float)points[i].X,
          (float)points[i].Y);
      }
      return pointsF;
    }
  }
}
