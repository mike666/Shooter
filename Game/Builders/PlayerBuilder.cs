using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  /// <summary>
  ///
  /// </summary>
  public class PlayerBuilder  {

    public static PlayerController Build(int x, int y, ICanvas canvas) {
      Player player = new Player(0, 10);
      player.loadProjectile(new Bullet(0, 0));
      
      ObjectRegistry.Instance.RegisterObj(player);

      return new PlayerController(canvas, player);
    }
  }
}
