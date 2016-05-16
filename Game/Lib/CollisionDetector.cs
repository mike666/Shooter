using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  class CollisionDetector {
    
    public static ObjectCollision Detect(IObject obj) {
      IObject target = ObjectRegistry.Instance.GameObjects.FirstOrDefault(c => c != obj && obj.GetX() == c.GetX() && obj.GetY() == c.GetY());

      if(target != null) {
        return new ObjectCollision() { Subject = obj, Target = target };
      }

      return null;
    }
  }
}
