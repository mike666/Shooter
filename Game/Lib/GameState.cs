using System.Collections.Generic;

namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  class GameState {

    public int PlayerPoints { get; set; }
    public int PlayerLives { get; set; }

    private static GameState _Singleton = null;

    private GameState() {
      Reset();
    }

    public void Reset() {
      PlayerPoints = 0;
      PlayerLives = 3;
    }

    public static GameState Instance {
      get {
        if (_Singleton == null) {
          _Singleton = new GameState();
        }

        return _Singleton;
      }
    }
  }
}
