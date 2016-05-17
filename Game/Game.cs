using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game {
  class Game {
        
    private Player _Player = null;
                      
    public Game() {
    
    }

    public void Start() {
      InitGame();
    }
        
    /// <summary>`
    /// Initiates the game by painting the background
    /// and initiating the hero
    /// </summary>
    public void InitGame() {
      SetBackgroundColor();

      AnimatedObjectBase projectile = new RightAnimate(new Bullet(0, 0));

      _Player = new Player(projectile, 0, 0);

      ObjectRegistry.Instance.RegisterObj(projectile);
      ObjectRegistry.Instance.RegisterObj(new Enemy(50, 5));
      ObjectRegistry.Instance.RegisterObj(new Block(25, 10));
      ObjectRegistry.Instance.RegisterObj(new Block(25, 15));
      ObjectRegistry.Instance.RegisterObj(new Block(25, 17));
      ObjectRegistry.Instance.RegisterObj(_Player);

      foreach (IObject obj in ObjectRegistry.Instance.GameObjects) {
        obj.Render();
      }

      Thread playerThread = new Thread(new ThreadStart(() => {
       
        ConsoleKeyInfo keyInfo;
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) {
          switch (keyInfo.Key) {
            case ConsoleKey.UpArrow:
              ObjectRegistry.Instance.Redraw();
              _Player.Move(0, -1);
              break;

            case ConsoleKey.RightArrow:
              ObjectRegistry.Instance.Redraw();
              _Player.Move(1, 0);
              break;

            case ConsoleKey.DownArrow:
              ObjectRegistry.Instance.Redraw();
              _Player.Move(0, 1);
              break;

            case ConsoleKey.LeftArrow:
              ObjectRegistry.Instance.Redraw();
              _Player.Move(-1, 0);
              break;
            case ConsoleKey.Spacebar:
              ObjectRegistry.Instance.Redraw();
              _Player.Fire();
              break;
          }
        }

      }));

      playerThread.Start();
    }

    private void SetBackgroundColor() {
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Clear(); //Important!
    }

  }
}
