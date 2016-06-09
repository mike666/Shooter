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
      
      Player player = new Player(0, 10);
      player.loadProjectile(new Bullet(0, 0));

      Enemy enemy = new Enemy(50, 5);
      enemy.loadProjectile(new Bullet(0, 0));
            
      ObjectRegistry.Instance.RegisterObj(enemy);
      ObjectRegistry.Instance.RegisterObj(player);
            
      PlayerController playerController = new PlayerController(canvas, player);
      playerController.Start();
       
      AIShooterController enemyController = new AIShooterController(canvas, enemy, player);
      enemyController.Start();

      Thread drawThread = new Thread(new ThreadStart(() => {
        while (true) {
          DrawCanvas(canvas);
          System.Threading.Thread.Sleep(100);
        }
      }));

      drawThread.Start();

    }

    private void DrawCanvas(ICanvas canvas) {
      canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
      canvas.WritePos(String.Format("Points: {0} Lives: {1}", GameState.Instance.PlayerPoints, GameState.Instance.PlayerLives), 1, 0);
    }
  }
}
