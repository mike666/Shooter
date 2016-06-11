using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  class Game {
    private ConsoleKeyInfo keyInfo;

    public Game() {

    }

    public void Start() {
      InitGame();
    }

    public void InitGame() {
      ObjectRegistry.Instance.Reset();
      GameState.Instance.Reset();

      ConsoleCanvas canvas = new ConsoleCanvas(new CollisionDetector());
                
      PlayerController playerController = PlayerBuilder.Build(0, 10, canvas);
      AIShooterController enemyController = EnemyBuilder.Build(50, 5, canvas, playerController.GetObject() as Player);

      playerController.Start();
      enemyController.Start();

      bool isGameOver = false;

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

            isGameOver = GameState.Instance.PlayerLives == 0;
            
            DrawCanvas(canvas);
          }

          if(isGameOver) {
            break;
          }

        }

        playerController.Stop();
        enemyController.Stop();
        GameOver(canvas);
      }));

      gameLoop.Start();
    }

    private void GameOver(ICanvas canvas) {
      canvas.Clear();

      int centerY = canvas.CanvasHeight() / 2;

      canvas.WriteCenterPosX(String.Format("Game over!"), centerY);
      canvas.WriteCenterPosX(String.Format("Score: {0}", GameState.Instance.PlayerPoints), centerY + 1);
      canvas.WriteCenterPosX(String.Format("Press spacebar to play again", GameState.Instance.PlayerPoints), centerY + 3);
       
      while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) {
        if (keyInfo.Key == ConsoleKey.Spacebar) {
          break;
        }
      }

      InitGame();
    }

    private void DrawCanvas(ICanvas canvas) {
      canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
      canvas.WritePos(String.Format("Score: {0} Lives: {1}", GameState.Instance.PlayerPoints, GameState.Instance.PlayerLives), 1, 0);
    }
  }
}
