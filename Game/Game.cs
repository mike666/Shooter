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
      ConsoleCanvas canvas = new ConsoleCanvas();

      IAnimator rightAnimator = new RightAnimator();
      IAnimator leftAnimator = new LeftAnimator();

      IObject projectile = new Bullet(0, 0);
      Player player = new Player(projectile, 0, 0);

      IObject enemy = new Enemy(50, 5);
      IObject block1 = new Block(25, 10);
      IObject block2 = new Block(25, 15);
      IObject block3 = new Block(25, 17);
            
      ObjectRegistry.Instance.RegisterObj(player);
      ObjectRegistry.Instance.RegisterObj(enemy);
      ObjectRegistry.Instance.RegisterObj(block1);
      ObjectRegistry.Instance.RegisterObj(block2);
      ObjectRegistry.Instance.RegisterObj(block3);

      canvas.RenderObj(player);
      canvas.RenderObj(enemy);
      canvas.RenderObj(block1);
      canvas.RenderObj(block2);
      canvas.RenderObj(block3);
      
      leftAnimator.Animate(canvas, enemy, 300);
      
      ConsoleKeyInfo keyInfo;
      while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) {
        switch (keyInfo.Key) {
          case ConsoleKey.UpArrow:
            canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
            canvas.MoveObj(player, 0, -1);
            break;

          case ConsoleKey.RightArrow:
            canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
            canvas.MoveObj(player, 1, 0);
            break;

          case ConsoleKey.DownArrow:
            canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
            canvas.MoveObj(player, 0, 1);
            break;

          case ConsoleKey.LeftArrow:
            canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
            canvas.MoveObj(player, -1, 0);
            break;
          case ConsoleKey.Spacebar:
            if (rightAnimator.IsAnimating()) {
              break;
            }
            
            IObject bullet = player.Fire();
                        
            if (!rightAnimator.IsAnimating()) {
              rightAnimator.Animate(canvas, bullet, 10);
            }
            
            break;
        }
      }
    }
  }
}
