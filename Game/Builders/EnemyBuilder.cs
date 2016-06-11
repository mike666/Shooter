using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  /// <summary>
  ///
  /// </summary>
  public class EnemyBuilder  {

    public static AIShooterController Build(int x, int y, ICanvas canvas, Player player) {
      Enemy enemy = new Enemy(50, 5);
      enemy.loadProjectile(new Bullet(0, 0));

      ObjectRegistry.Instance.RegisterObj(enemy);

      return new AIShooterController(canvas, enemy, player);
    }
  }
}
