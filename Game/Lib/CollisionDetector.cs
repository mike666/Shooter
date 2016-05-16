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

    private List<IObject> _GameObjects = new List<IObject>();

    public void RegisterObj(IObject obj) {
      if(!_GameObjects.Contains(obj)) {
        _GameObjects.Add(obj);
      }
    }

    public List<IObject> Detect() {
      foreach(IObject obj in _GameObjects) {
        IObject selected = _GameObjects.FirstOrDefault(c => c != obj && obj.GetX() == c.GetX() && obj.GetY() == c.GetY());

        if(selected != null) {
          return new List<IObject>() { obj, selected };
        }
      }

      return new List<IObject>();
    }
  }
}
