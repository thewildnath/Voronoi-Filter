using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class PolygonFilterInfo {
    public enum FilterType {
      solidColor,
      mean,
      transparent
    };

    public FilterType type;
    public Color color;

    public PolygonFilterInfo(FilterType type, Color color) {
      this.type = type;
      this.color = color;
    }
  }
}
