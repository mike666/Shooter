using System;
using System.Collections.Generic;
using System.Linq;
namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  public class ObjectCollision : IObjectCollision {
    public IObject Subject { get; set; }
    public IObject Target { get; set; }
  }
}
