using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  class Game {

    public Game() {

    }

    public void Start() {
      InitGame();
    }

    public void InitGame() {
      ConsoleCanvas canvas = new ConsoleCanvas(new CollisionDetector());
                
      PlayerController playerController = PlayerBuilder.Build(0, 10, canvas);
      AIShooterController enemyController = EnemyBuilder.Build(50, 5, canvas, playerController.GetObject() as Player);

      playerController.Start();
      enemyController.Start();

      Thread gameLoop = new Thread(new ThreadStart(() => {
        while (true) {
          DrawCanvas(canvas);
          System.Threading.Thread.Sleep(100);

          if(enemyController.GetObject().Status == ObjectStatus.Shot) {
            enemyController.GetObject().SetGraphic("X");
            
            GameState.Instance.PlayerPoints++;

            DrawCanvas(canvas);
            System.Threading.Thread.Sleep(1000);
            ObjectRegistry.Instance.RemoveObj(enemyController.GetObject());

            enemyController = EnemyBuilder.Build(50, 5, canvas, playerController.GetObject() as Player);
            enemyController.Start();
          }

          if (playerController.GetObject().Status == ObjectStatus.Shot) {
            playerController.GetObject().SetPos(0, 10);
            playerController.GetObject().Status = ObjectStatus.Active;
            GameState.Instance.PlayerLives--;

            DrawCanvas(canvas);
          }
        }
      }));

      gameLoop.Start();
    }

    private void DrawCanvas(ICanvas canvas) {
      canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
      canvas.WritePos(String.Format("Points: {0} Lives: {1}", GameState.Instance.PlayerPoints, GameState.Instance.PlayerLives), 1, 0);
    }
  }
}
