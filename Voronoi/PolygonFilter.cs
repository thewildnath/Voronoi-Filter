using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class PolygonFilter {
    public static Bitmap Apply(Bitmap original, Bitmap bmp, Point[] polygon, FilterInfo info) {
      //Graphics g = Graphics.FromImage(bmp);
      //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      //SolidBrush brush = new SolidBrush(info.color);
      //g.FillPolygon(brush, polygon);

      CoverPolygon(original, bmp, polygon, info);

      return bmp;
    }

    static Bitmap CoverPolygon(Bitmap original, Bitmap bmp, Point[] polygon, FilterInfo info) {
      int width = original.Size.Width;
      int height = original.Size.Height;

      PointF center = new Point();
      Point lowerBounds = new Point();
      Point upperBounds = new Point();
      for (int i = 0; i < polygon.Length; ++i) {
        center.X += polygon[i].X;
        center.Y += polygon[i].Y;
        lowerBounds.X = Math.Min(lowerBounds.X, polygon[i].X);
        lowerBounds.Y = Math.Min(lowerBounds.Y, polygon[i].Y);
        upperBounds.X = Math.Max(upperBounds.X, polygon[i].X);
        upperBounds.Y = Math.Max(upperBounds.Y, polygon[i].Y);
      }
      center.X /= polygon.Length;
      center.Y /= polygon.Length;

      int[] minX = new int[height];
      int[] maxX = new int[height];
      for (int y = lowerBounds.Y; y <= upperBounds.Y; ++y) {
        Line2D row = new Line2D(0, 1, -y);
        minX[y] = width;
        maxX[y] = 0;

        for (int i = 0; i < polygon.Length; ++i) {
          Segment2D seg = new Segment2D(polygon[i], polygon[(i + 1) % polygon.Length]);
          Tuple<bool, PointF> res = seg.Intersect(row);

          if (res.Item1 == true) {
            //if (res.Item2.X <= center.X)
              minX[y] = (int)Math.Min(minX[y], res.Item2.X);
            //if (res.Item2.X >= center.X)
              maxX[y] = (int)Math.Max(maxX[y], res.Item2.X);
          }
        }
      }

      // THIS HAS TO GO SOMEWHERE ELSE
      //Graphics g = Graphics.FromImage(bmp);
      //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

      if (info.type == FilterInfo.FilterType.solidColor) {
        //SolidBrush brush = new SolidBrush(info.color);
        //g.FillPolygon(brush, polygon);
        Fill(bmp, info.color, lowerBounds.Y, upperBounds.Y, minX, maxX);
      } else if (info.type == FilterInfo.FilterType.mean) {
        Color color = GetMean(original, lowerBounds.Y, upperBounds.Y, minX, maxX);
        //SolidBrush brush = new SolidBrush(color);
        //g.FillPolygon(brush, polygon);
        Fill(bmp, color, lowerBounds.Y, upperBounds.Y, minX, maxX);
      }

      return bmp;
    }

    static Color GetMean(Bitmap bmp, int minY, int maxY, int[] minX, int[] maxX) {
      int sumR = 0;
      int sumG = 0;
      int sumB = 0;
      int count = 0;
      //*
      BitmapData data = bmp.LockBits(
        new Rectangle(0, 0, bmp.Width, bmp.Height),
        ImageLockMode.ReadWrite,
        PixelFormat.Format24bppRgb);
      int stride = data.Stride;

      unsafe
      {
        byte* ptr = (byte*)data.Scan0;
        // Get Mean Color
        for (int y = minY; y <= maxY; ++y) {
          for (int x = minX[y]; x <= maxX[y]; ++x) {
            sumR += ptr[(x * 3) + y * stride];
            sumG += ptr[(x * 3) + y * stride + 1];
            sumB += ptr[(x * 3) + y * stride + 2];
            ++count;
          }
        }
      }
      bmp.UnlockBits(data);
      //*/
      /*
      for (int y = minY; y <= maxY; ++y) {
        for (int x = minX[y]; x <= maxX[y]; ++x) {
          sumR += bmp.GetPixel(x, y).R;
          sumG += bmp.GetPixel(x, y).G;
          sumB += bmp.GetPixel(x, y).B;
          ++count;
        }
      }
      //*/
      int meanR = 0;
      int meanG = 0;
      int meanB = 0;

      if (count > 0) {
        meanR = sumR / count;
        meanG = sumG / count;
        meanB = sumB / count;
      }

      return Color.FromArgb(meanR, meanG, meanB);
    }

    static void Fill(Bitmap bmp, Color color, int minY, int maxY, int[] minX, int[] maxX) {
      BitmapData data = bmp.LockBits(
        new Rectangle(0, 0, bmp.Width, bmp.Height),
        ImageLockMode.ReadWrite,
        PixelFormat.Format24bppRgb);
      int stride = data.Stride;

      unsafe
      {
        byte* ptr = (byte*)data.Scan0;
        // Get Mean Color
        for (int y = minY; y <= maxY; ++y) {
          for (int x = minX[y]; x <= maxX[y]; ++x) {
            ptr[(x * 3) + y * stride] = color.R;
            ptr[(x * 3) + y * stride + 1] = color.G;
            ptr[(x * 3) + y * stride + 2] = color.B;
          }
        }
      }
      bmp.UnlockBits(data);
    }
  }
}

/*
      BitmapData data = bmp.LockBits(
        new Rectangle(0, 0, bmp.Width, bmp.Height), 
        ImageLockMode.ReadWrite, 
        PixelFormat.Format24bppRgb);
      int stride = data.Stride;

      unsafe
      {
        byte* ptr = (byte*)data.Scan0;
        // Get Mean Color
        int sumR = 0;
        int sumG = 0;
        int sumB = 0;
        int count = 0;
        for (int y = lowerBounds.Y; y <= upperBounds.Y; ++y) {
          for (int x = minX[y]; x <= maxX[y]; ++x) {
            sumR += ptr[(x * 3) + y * stride];
            sumG += ptr[(x * 3) + y * stride + 1];
            sumB += ptr[(x * 3) + y * stride + 2];
            ++count;
          }
        }

        if (count > 0) {
          int meanR = sumR / count;
          int meanG = sumG / count;
          int meanB = sumB / count;
          for (int y = lowerBounds.Y; y <= upperBounds.Y; ++y) {
            for (int x = minX[y]; x <= maxX[y]; ++x) {
              ptr[(x * 3) + y * stride] = (byte)meanR;
              ptr[(x * 3) + y * stride + 1] = (byte)meanG;
              ptr[(x * 3) + y * stride + 2] = (byte)meanB;
            }
          }
        }
      }

      bmp.UnlockBits(data);
      */