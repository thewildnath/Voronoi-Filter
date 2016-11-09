using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi {
  class FilterInfo {
    public enum FilterType {
      solidColor,
      mean,
      transparent
    };

    public FilterType type;
    public Color color;

    public FilterInfo(FilterType type, Color color) {
      this.type = type;
      this.color = color;
    }
  }
}
