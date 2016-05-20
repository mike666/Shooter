using System;

namespace Game {
  class Game {
    private int _Points = 0;
    
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

      DrawCanvas(canvas);

      ConsoleKeyInfo keyInfo;
      while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) {
        switch (keyInfo.Key) {
          case ConsoleKey.UpArrow:
            DrawCanvas(canvas);
            canvas.MoveObj(player, 0, -1);
            break;

          case ConsoleKey.RightArrow:
            DrawCanvas(canvas);
            canvas.MoveObj(player, 1, 0);
            break;

          case ConsoleKey.DownArrow:
            DrawCanvas(canvas);
            canvas.MoveObj(player, 0, 1);
            break;

          case ConsoleKey.LeftArrow:
            DrawCanvas(canvas);
            canvas.MoveObj(player, -1, 0);
            break;
          case ConsoleKey.Spacebar:
            if (!player.HasProjectile()) {
              break;
            }

            IObject bullet = player.FireProjectile();

            ObjectRegistry.Instance.RegisterObj(bullet);

            IAnimator projectileAnimator = new ProjectileAnimator(bullet);

            projectileAnimator.Animate(canvas, 10, 
              (collision) => {
                _Points++;
                projectileAnimator.Stop();
                canvas.ClearObj(collision.Subject);
                canvas.ClearObj(collision.Target);
                ObjectRegistry.Instance.RemoveObj(collision.Target);
                DrawCanvas(canvas);
              },
              () => {
              player.loadProjectile(new Bullet(0, 0));
              ObjectRegistry.Instance.RemoveObj(bullet);
            });
            
            break;
        }
      }
    }

    private void DrawCanvas(ICanvas canvas) {
      canvas.ReDrawObjects(ObjectRegistry.Instance.GameObjects);
      canvas.WritePos("Points: " + _Points, 1, 0);
      
      if(_Points == 4) {
        canvas.Clear();
        canvas.WritePos("Duncan wins!", 30, 10);
      }

    }
  }
}
