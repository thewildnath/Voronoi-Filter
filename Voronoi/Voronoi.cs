using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class Voronoi {
    public Point lowerLimits = new Point(-1000, -1000);
    public Point upperLimits = new Point(1000, 1000);

    public Point[] VoronoiPolygon(Point[] points, int index) {
      Point center = points[index];
      List<Point> dualPoints = new List<Point>();

      for (int i = 0; i < points.Length; ++i) {
        if (points[i] != center) {
          Line2D bisector = new Segment2D(
            new Point(0, 0), 
            new Point(points[i].X - center.X)).Bisector();          // ~OPTIMIZATION
          Point dual = bisector.Dual();
          dualPoints.Add(dual);
        }
      }

      dualPoints.Add(new Line2D(1, 0, lowerLimits.X + center.X).Dual());
      dualPoints.Add(new Line2D(1, 0, upperLimits.X + center.X).Dual());
      dualPoints.Add(new Line2D(0, 1, lowerLimits.Y + center.Y).Dual());
      dualPoints.Add(new Line2D(0, 1, upperLimits.Y + center.Y).Dual());

      Point[] dualConvexHull = ConvexHull(dualPoints.ToArray());
      Point[] polygon = new Point[dualConvexHull.Length - 1];

      for (int i = 0; i < dualConvexHull.Length - 1; ++i) {
        polygon[i] = new Line2D(dualConvexHull[i], dualConvexHull[i + 1]).Dual();
        polygon[i].X += center.X;
        polygon[i].Y += center.Y;
      }

      return polygon;
    }

    float ccw(Point p1, Point origin, Point p2) {
      p1.X -= origin.X;
      p1.Y -= origin.Y;
      p2.X -= origin.X;
      p2.Y -= origin.Y;
      return p1.X * p2.Y - p2.X * p1.Y;
    }

    public Point[] ConvexHull(Point[] points) {                                                   // PRIVAT

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

      Point[] hull = new Point[stack.Count];
      for (int i = 0; i < stack.Count; ++i)
        hull[i] = points[stack[i]];

      return hull;
    }

    int Vector2Comparer(Point p1, Point p2) {
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

    public Point[] GetPolygon(int index, Point[] points) {
      List<Point> polygon = new List<Point>();



      return polygon.ToArray();
    }
    */
  }
}
