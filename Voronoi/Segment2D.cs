using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class Segment2D : Line2D {
    public PointF end1;
    public PointF end2;

    public Segment2D(PointF p1, PointF p2) {
      this.a = p1.Y - p2.Y;
      this.b = p2.X - p1.X;
      this.c = p1.X * p2.Y - p1.Y * p2.X;

      this.end1 = p1;
      this.end2 = p2;
    }

    public override Tuple<bool, PointF> Intersect(Line2D arg) {
      Tuple<bool, PointF> res = base.Intersect(arg);
      if (res.Item1 == true)
        return new Tuple<bool, PointF>(IsInsideBoundingBox(res.Item2), res.Item2);
      else
        return res;
    }

    public Line2D Bisector() {
      PointF center = new PointF((end1.X + end2.X) / 2,
                                   (end1.Y + end2.Y) / 2);
      float a = this.b;
      float b = -this.a;
      float c = center.Y * this.a - center.X * a;

      return new Line2D(a, b, c);
    }

    public bool IsInsideBoundingBox(PointF arg) {
      PointF lowerBounds = new PointF(
        Math.Min(end1.X, end2.X),
        Math.Min(end1.Y, end2.Y));
      PointF upperBounds = new PointF(
        Math.Max(end1.X, end2.X),
        Math.Max(end1.Y, end2.Y));

      return arg.X >= lowerBounds.X - EPS &&
             arg.X <= upperBounds.X + EPS &&
             arg.Y >= lowerBounds.Y - EPS &&
             arg.Y <= upperBounds.Y + EPS;
    }
  }
}