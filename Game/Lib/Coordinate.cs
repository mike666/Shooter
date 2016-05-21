using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  public class Coordinate {
    public int X { get; set; } //Left
    public int Y { get; set; } //Top

    public Coordinate(int x, int y) {
      X = x;
      Y = y;
    }
  }
}
