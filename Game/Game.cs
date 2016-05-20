using System;

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

      IObject enemy = new Enemy(50, 5);
      IObject block1 = new Block(25, 10);
      IObject block2 = new Block(25, 15);
      IObject block3 = new Block(25, 17);
            
      ObjectRegistry.Instance.RegisterObj(player);
      ObjectRegistry.Instance.RegisterObj(enemy);
      ObjectRegistry.Instance.RegisterObj(block1);
      ObjectRegistry.Instance.RegisterObj(block2);
      ObjectRegistry.Instance.RegisterObj(block3);

      foreach (IObject obj in ObjectRegistry.Instance.GameObjects) {
        canvas.RenderObj(obj);
      }
      
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
            if (!player.HasProjectile()) {
              break;
            }

            IObject bullet = player.FireProjectile();

            ObjectRegistry.Instance.RegisterObj(bullet);

            IAnimator projectileAnimator = new ProjectileAnimator(bullet);

            projectileAnimator.Animate(canvas, 10, () => {
              player.loadProjectile(new Bullet(0, 0));
              ObjectRegistry.Instance.RemoveObj(bullet);
            });
            
            break;
        }
      }
    }
  }
}
