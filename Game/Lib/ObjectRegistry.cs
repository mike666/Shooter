using System.Collections.Generic;

namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  class ObjectRegistry {
    private static ObjectRegistry _Singleton = null;
    private List<IObject> _GameObjects = new List<IObject>();

    public static ObjectRegistry Instance {
      get {
        if (_Singleton == null) {
          _Singleton = new ObjectRegistry();
        }

        return _Singleton;
      }
    }

    public List<IObject> GameObjects {
      get { return _GameObjects; }
    }

    public void RegisterObj(IObject obj) {
      if(obj != null && !_GameObjects.Contains(obj)) {
        _GameObjects.Add(obj);
      }
    }

    public void RemoveObj(IObject obj) {
      if (obj != null &&_GameObjects.Contains(obj)) {
        _GameObjects.Remove(obj);
      }
    }
  }
}
