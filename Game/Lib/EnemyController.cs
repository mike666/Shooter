using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  /// <summary>
  ///
  /// </summary>
  public class EnemyController {
    private ICanvas _Canvas = null;
    private Enemy _Enemy = null;
    private Player _Player = null;
    private Thread _EnemyThread = null;
    private Thread _ProjectileThread = null;

    public EnemyController(ICanvas canvas, Enemy enemy, Player player) {
      _Canvas = canvas;
      _Enemy = enemy;
      _Player = player;
    }
    
    public IObject GetObject() {
      return _Enemy;
    }

    public void Start() {
      Animator enemyAnimator = new Animator(_Enemy);

      _EnemyThread = new Thread(new ThreadStart(() => {
        while (true) {
          Random rand = new Random();

          int direction =  rand.Next(8);
          int enemySpeed =  rand.Next(200, 500);
          int? distance = rand.Next(10, 30);

          switch (direction) {
            case 0:
              enemyAnimator.Left(_Canvas, enemySpeed, distance);
              break;
            case 1:
              enemyAnimator.Right(_Canvas, enemySpeed, distance);
              break;
            case 2:
              enemyAnimator.Up(_Canvas, enemySpeed, distance);
              break;
            case 3:
              enemyAnimator.Down(_Canvas, enemySpeed, distance);
              break;
            case 4:
              enemyAnimator.UpRight(_Canvas, enemySpeed, distance);
              break;
            case 5:
              enemyAnimator.UpLeft(_Canvas, enemySpeed, distance);
              break;
            case 6:
              enemyAnimator.DownRight(_Canvas, enemySpeed, distance);
              break;
            case 7:
              enemyAnimator.DownLeft(_Canvas, enemySpeed, distance);
              break;
          }

          System.Threading.Thread.Sleep(100);
        }
      }));

      _EnemyThread.Start();

      _ProjectileThread = new Thread(new ThreadStart(() => {
        while (true) {
          if (_Enemy.GetY() == _Player.GetY()) {

            // enemy has 25% chance of shooting player
            Random rand = new Random();
            if (rand.Next(4) != 0) {
              continue;
            }

            IObject bullet = _Enemy.FireProjectile();

            ObjectRegistry.Instance.RegisterObj(bullet);

            Animator projectileAnimator = new Animator(bullet);

            projectileAnimator.Left(_Canvas, 10, null,
                (collision) => {
               //   projectileAnimator.Stop();
                },
               () => {
                 _Canvas.ClearObj(bullet);
                 _Enemy.loadProjectile(new Bullet(0, 0));
                 ObjectRegistry.Instance.RemoveObj(bullet);
               });
          }

          System.Threading.Thread.Sleep(100);
        }
      }));

      _ProjectileThread.Start();
    }

    public void Stop() {
      _EnemyThread.Abort();
      _ProjectileThread.Abort();
    }

  }
}
