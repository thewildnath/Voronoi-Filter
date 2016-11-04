using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class VoronoiController {
    int width;
    int height;

    int mouseAccuracy;

    int currSelected;

    public bool modified;

    public List<Point> points;
    public List<PolygonFilterInfo> info;

    public VoronoiController(int width, int height) {
      this.width = width;
      this.height = height;

      mouseAccuracy = 50;

      currSelected = -1;

      modified = false;

      points = new List<Point>();
      info = new List<PolygonFilterInfo>();
    }

    public void MouseDown(int type, int posX, int posY) {
      Tuple<int, int> rez = GetClosest(posX, posY);

      if (rez.Item1 != -1 && rez.Item2 <= mouseAccuracy) {
        if (type == 0)
          currSelected = rez.Item1;
        else
          DeletePoint(rez.Item1);
      } else if (type == 0) {
        CreatePoint(posX, posY);
        currSelected = points.Count - 1;
      }
    }

    public void MouseMove(int posX, int posY) {
      if (currSelected != -1)
        points[currSelected] = MovePoint(points[currSelected], posX, posY);
    }

    public void MouseUp() {
      currSelected = -1;
    }

    Tuple<int, int> GetClosest(int posX, int posY) {
      int index = -1;
      int minDist = width * height + 1;

      for (int i = 0; i < points.Count; ++i) {
        int temp = GetDistanceSquared(points[i].X, points[i].Y, posX, posY);
        if (temp < minDist) {
          minDist = temp;
          index = i;
        }
      }

      return new Tuple<int, int>(index, minDist);
    }

    void CreatePoint(int posX, int posY) {
      points.Add(new Point(posX, posY));
      Random random = new Random();
      Color color = Color.FromArgb(
        random.Next(0, 255), 
        random.Next(0, 255), 
        random.Next(0, 255));
      info.Add(new PolygonFilterInfo(
        PolygonFilterInfo.FilterType.solidColor, 
        color));
      modified = true;
    }

    void DeletePoint(int index) {
      points.RemoveAt(index);
      info.RemoveAt(index);

      modified = true;
    }

    // TODO - use ref? Not Quite right...
    Point MovePoint(Point point, int posX, int posY) {
      point.X = Clamp(posX, 0, width - 1);
      point.Y = Clamp(posY, 0, height - 1);

      modified = true;

      return point;
    }

    int GetDistanceSquared(int x1, int y1, int x2, int y2) {
      return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
    }

    int Clamp(int x, int min, int max) {
      if (x < min)
        return min;
      else if (x > max)
        return max;
      else
        return x;
    }
  }
}
