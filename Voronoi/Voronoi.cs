using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class Voronoi {
    float width;
    float height;
    float border;

    public Voronoi(int width, int height, int border = 5) {
      this.width = width - 1;
      this.height = height - 1;
      this.border = border;
    }

    public PointF[] GetPolygon(int index, PointF[] points_inp) {
      PointF[] points = new PointF[points_inp.Length];
      for (int i = 0; i < points_inp.Length; ++i)
        points[i] = new PointF(
          points_inp[i].X - width / 2, 
          points_inp[i].Y - height / 2);

      PointF center = points[index];
      List<PointF> dualPoints = new List<PointF>();

      for (int i = 0; i < points.Length; ++i) {
        bool ok = points[i] != center;
        for (int j = i + 1; j < points.Length && ok; ++j)
          if (points[i] == points[j])
            ok = false;
        if (ok) {
          Line2D bisector = new Segment2D(
            new PointF(0, 0), 
            new PointF(points[i].X - center.X, 
                        points[i].Y - center.Y)).Bisector();          // ~OPTIMIZATION
          PointF dual = bisector.Dual();
          dualPoints.Add(dual);
        }
      }

      dualPoints.Add(new Line2D(1, 0, -width / 2 - border + center.X).Dual());
      dualPoints.Add(new Line2D(1, 0, width / 2 + border + center.X).Dual());
      dualPoints.Add(new Line2D(0, 1, -height / 2 - border + center.Y).Dual());
      dualPoints.Add(new Line2D(0, 1, height / 2 + border + center.Y).Dual());

      PointF[] dualConvexHull = ConvexHull(dualPoints.ToArray());
      PointF[] polygon = new PointF[dualConvexHull.Length - 1];

      for (int i = 0; i < dualConvexHull.Length - 1; ++i) {
        polygon[i] = new Line2D(dualConvexHull[i], dualConvexHull[i + 1]).Dual();
        polygon[i].X += center.X;
        polygon[i].Y += center.Y;
      }

      for (int i = 0; i < polygon.Length; ++i) {
        polygon[i].X = Clamp(polygon[i].X, -width / 2, width / 2);
        polygon[i].Y = Clamp(polygon[i].Y, -height / 2, height / 2);

        polygon[i].X += width / 2;
        polygon[i].Y += height / 2;
      }

      return polygon;
    }

    float Clamp(float val, float min, float max) {
      if (float.IsNaN(val))
        val = max;
      if (val < min)
        return min;
      else if (val > max)
        return max;
      else
        return val;
    }

    double ccw(PointF p1, PointF origin, PointF p2) {
      p1.X -= origin.X;
      p1.Y -= origin.Y;
      p2.X -= origin.X;
      p2.Y -= origin.Y;
      return p1.X * p2.Y - p2.X * p1.Y;
    }

    public PointF[] ConvexHull(PointF[] points) {                                                   // PRIVAT

      if (points.Length <= 2)
        return points;

      bool[] taken = new bool[points.Length];
      List<int> stack = new List<int>();

      Array.Sort(points, Vector2Comparer);
      stack.Add(0); taken[0] = true;
      stack.Add(1); taken[1] = true;

      for (int i = 2; i < points.Length; ++i) {
        while (stack.Count >= 2 &&
               ccw(points[stack[stack.Count - 2]], points[stack[stack.Count - 1]], points[i]) > 0) {
          taken[stack[stack.Count - 1]] = false;
          stack.RemoveAt(stack.Count - 1);
        }

        stack.Add(i);
        taken[i] = true;
      }

      taken[0] = false;
      for (int i = points.Length - 1; i >= 0; --i) {
        if (!taken[i]) {
          while (stack.Count >= 2 &&
                 ccw(points[stack[stack.Count - 2]], points[stack[stack.Count - 1]], points[i]) > 0) {
            taken[stack[stack.Count - 1]] = false;
            stack.RemoveAt(stack.Count - 1);
          }

          stack.Add(i);
          taken[i] = true;
        }
      }

      PointF[] hull = new PointF[stack.Count];
      for (int i = 0; i < stack.Count; ++i)
        hull[i] = points[stack[i]];

      return hull;
    }

    int Vector2Comparer(PointF p1, PointF p2) {
      if (p1 == p2)
        return 0;
      if (p1.X == p2.X)
        return p1.Y < p2.Y ? -1 : 1;
      return p1.X < p2.X ? -1 : 1;
    }

    /*
    int width;
    int height;

    public Voronoi(int width, int height) {
      this.width = width;
      this.height = height;
    }

    public PointF[] GetPolygon(int index, PointF[] points) {
      List<PointF> polygon = new List<PointF>();



      return polygon.ToArray();
    }
    */
  }
}
