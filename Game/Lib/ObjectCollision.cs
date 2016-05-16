using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  class ObjectCollision {
    public IObject Subject { get; set; }
    public IObject Target { get; set; }
  }
}
