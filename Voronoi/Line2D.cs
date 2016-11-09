using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class Line2D {
    public const float EPS = (float)0.000001;

    public float a;
    public float b;
    public float c;

    public Line2D(float a = 0, float b = 1, float c = 0) {
      this.a = a;
      this.b = b;
      this.c = c;
    }

    public Line2D(PointF p1, PointF p2) {
      this.a = p1.Y - p2.Y;
      this.b = p2.X - p1.X;
      this.c = p1.X * p2.Y - p1.Y * p2.X;
    }

    public float Distance(ref PointF point) {
      float num = Math.Abs(a * point.X + b * point.Y);
      float den = (float)Math.Sqrt(a * a + b * b);
      return num / den;
    }

    public virtual Tuple<bool, PointF> Intersect(Line2D arg) {
      if (IsParalel(arg))
        return new Tuple<bool, PointF>(false, default(PointF)); // default(T) ~= null
      else {
        PointF temp = new PointF();
        temp.X = (arg.c * b - c * arg.b) /
                 (a * arg.b - arg.a * b);
        if (b == 0) // 1st Line Vertical
          temp.Y = -(arg.c + arg.a * temp.X) / arg.b;
        else
          temp.Y = -(c + a * temp.X) / b;
        return new Tuple<bool, PointF>(true, temp);
      }
    }

    public PointF Dual() {
      //Assert.IsTrue(c != 0);
      return new PointF(a / c, b / c);
    }

    public bool IsParalel(Line2D arg) {
      return Math.Abs((-a / b) - (-arg.a / arg.b)) < EPS;
    }
  }
}