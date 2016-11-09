using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class Point2D {
    double x;
    double y;

    public double X {
      get {
        return x;
      }
      set {
        x = value;
      }
    }

    public double Y {
      get {
        return y;
      }
      set {
        y = value;
      }
    }

    public Point2D(double x, double y) {
      this.x = x;
      this.y = y;
    }
  }
}
