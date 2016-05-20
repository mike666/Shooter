using System.Linq;

namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  class CollisionDetector : ICollisionDetector {
    
    public IObjectCollision Detect(IObject obj) {
      IObject target = ObjectRegistry.Instance.GameObjects.FirstOrDefault(c => c != obj && obj.GetX() == c.GetX() && obj.GetY() == c.GetY());

      if(target != null) {
        return new ObjectCollision() { Subject = obj, Target = target };
      }

      return null;
    }
  }
}
