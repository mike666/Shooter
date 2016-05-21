using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  class Game {
    private int _PlayerPoints = 0;
    private int _EnemyPoints = 0;

    public int Dictionay { get; private set; }

    public Game() {

    }

    public void Start() {
      InitGame();
    }

    public void InitGame() {
      ConsoleCanvas canvas = new ConsoleCanvas(new CollisionDetector());

      Player player = new Player(0, 10);
      player.loadProjectile(new Bullet(0, 0));

      Enemy enemy = new Enemy(50, 5);
      enemy.loadProjectile(new Bullet(0, 0));

      ObjectRegistry.Instance.RegisterObj(enemy);
      ObjectRegistry.Instance.RegisterObj(player);

      DrawCanvas(canvas);

      EnemyController enemyController = new EnemyController(canvas, enemy, player);
      enemyController.Start();
      
      ConsoleKeyInfo keyInfo;
      while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) {
        switch (keyInfo.Key) {
          case ConsoleKey.UpArrow:
            //     DrawCanvas(canvas);
            canvas.MoveObj(player, 0, -1);
            break;

          case ConsoleKey.RightArrow:
            //   DrawCanvas(canvas);
            canvas.MoveObj(player, 1, 0);
            break;

          case ConsoleKey.DownArrow:
            // DrawCanvas(canvas);
            canvas.MoveObj(player, 0, 1);
            break;

          case ConsoleKey.LeftArrow:
            // DrawCanvas(canvas);
            canvas.MoveObj(player, -1, 0);
            break;
          case ConsoleKey.Spacebar:
            if (!player.HasProjectile()) {
              break;
            }

            IObject bullet = player.FireProjectile();

            ObjectRegistry.Instance.RegisterObj(bullet);

            IAnimator projectileAnimator = new Animator(bullet);

            Thread playerProjectileThread = new Thread(new ThreadStart(() => {
              projectileAnimator.Right(canvas, 10, null,
             (collision) => {
               _PlayerPoints++;
               projectileAnimator.Stop();
               canvas.ClearObj(collision.Subject);
               //ObjectRegistry.Instance.RemoveObj(collision.Target);
               ObjectRegistry.Instance.RemoveObj(collision.Subject);

               enemyController.GetObject().SetGraphic("R.I.P");
               enemyController.Stop();

               Enemy newEnemy = new Enemy(50, 5);
               newEnemy.loadProjectile(new Bullet(0, 0));
               ObjectRegistry.Instance.RegisterObj(newEnemy);

               enemyController = new EnemyController(canvas, newEnemy, player);
               enemyController.Start();
                
               DrawCanvas(canvas);
             },
             () => {
               canvas.ClearObj(bullet);
               player.loadProjectile(new Bullet(0, 0));
               ObjectRegistry.Instance.RemoveObj(bullet);
               DrawCanvas(canvas);
             });
            }));

            playerProjectileThread.Start();

            break;
        }
      }
    }

    private void DrawCanvas(ICanvas canvas) {
      canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
      canvas.WritePos(String.Format("Player: {0} Enemy: {1}", _PlayerPoints, _EnemyPoints), 1, 0);
    }
  }
}
