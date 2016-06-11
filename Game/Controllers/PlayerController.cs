using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  /// <summary>
  ///
  /// </summary>
  public class PlayerController : IController {
    private ICanvas _Canvas = null;
    private Player _Player = null;
    private Thread _ObjThread = null;
    private Thread _ProjectileThread = null;

    public PlayerController(ICanvas canvas, Player player) {
      _Canvas = canvas;
      _Player = player;
    }

    public IObject GetObject() {
      return _Player;
    }

    public void Start() {
      _ObjThread = new Thread(new ThreadStart(() => {

        ConsoleKeyInfo keyInfo;
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) {
          switch (keyInfo.Key) {
            case ConsoleKey.UpArrow:
              _Canvas.MoveObj(_Player, 0, -1);
              break;

            case ConsoleKey.RightArrow:
              // check canvas boundry, player can't move further than half the width of screen
              if(_Player.GetX() + 1 > (_Canvas.CanvasWidth() / 3)) {
                break;
              }
              
             _Canvas.MoveObj(_Player, 1, 0);
              break;

            case ConsoleKey.DownArrow:
              _Canvas.MoveObj(_Player, 0, 1);
              break;

            case ConsoleKey.LeftArrow:
              _Canvas.MoveObj(_Player, -1, 0);
              break;
            case ConsoleKey.Spacebar:
              if (!_Player.HasProjectile()) {
                break;
              }

              IObject bullet = _Player.FireProjectile();

              ObjectRegistry.Instance.RegisterObj(bullet);

              IAnimator projectileAnimator = new Animator(bullet);

              Thread playerProjectileThread = new Thread(new ThreadStart(() => {
                projectileAnimator.Right(_Canvas, 10, null,
               (collision) => {

                 projectileAnimator.Stop();
                 _Canvas.ClearObj(collision.Subject);

                 if (collision.Target is Enemy) {
                   collision.Target.Status = ObjectStatus.Shot;
                 }

                 ObjectRegistry.Instance.RemoveObj(collision.Subject);
               },
               () => {
                 _Canvas.ClearObj(bullet);
                 _Player.loadProjectile(new Bullet(0, 0));
                 ObjectRegistry.Instance.RemoveObj(bullet);
               });
              }));

              playerProjectileThread.Start();

              break;
          }
        }
      }));

      _ObjThread.Start();
    }

    public void Stop() {
      _ObjThread.Abort();
    }
  }
}
