using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  /// <summary>
  ///
  /// </summary>
  public class AIShooterController : AIControllerBase {
    private ICanvas _Canvas = null;
    private Shooter _Obj = null;
    private Player _Player = null;
    private Thread _ObjThread = null;
    private Thread _ProjectileThread = null;

    public AIShooterController(ICanvas canvas, Shooter obj, Player player) {
      _Canvas = canvas;
      _Obj = obj;
      _Player = player;
    }
    
    public override IObject GetObject() {
      return _Obj;
    }
      
    public override void Start() {
      Animator enemyAnimator = new Animator(_Obj);

      Dictionary<string, Action<ICanvas, int, int?, Action<IObjectCollision>, Action>> possibleDirections = GetPossibleDirections(enemyAnimator);
           
      _ObjThread = new Thread(new ThreadStart(() => {
        while (true) {
          if(_Obj.Status == ObjectStatus.InActive) {
            Stop();
            break;
          }

          Random rand = new Random();

          List<string> targetDirections = new List<string>();

          if (_Obj.GetY() > _Player.GetY()) {
            targetDirections.Add("Up");

            if (_Obj.GetX() > _Player.GetX()) {
              targetDirections.Add("Left");
              targetDirections.Add("UpLeft");
            } else {
              targetDirections.Add("Right");
              targetDirections.Add("UpRight");
            }

          } else {
            targetDirections.Add("Down");

            if (_Obj.GetX() > _Player.GetX()) {
              targetDirections.Add("Left");
              targetDirections.Add("DownLeft");
            } else {
              targetDirections.Add("Right");
              targetDirections.Add("DownRight");
            }
          }
          
          string direction = targetDirections[rand.Next(targetDirections.Count)];
          int speed = rand.Next(100, 200);
          int? distance = rand.Next(10, 30);

          // check canvas boundry, player can't move further than half the width of screen
          if (direction.Contains("Left") && _Obj.GetX() - distance < (_Canvas.CanvasWidth() / 3)) {
            possibleDirections["Right"].Invoke(_Canvas, speed, distance, null, null);
            continue;
          }
                    
          if (possibleDirections.ContainsKey(direction)) {
            possibleDirections[direction].Invoke(_Canvas, speed, distance, null, null);
          }

          System.Threading.Thread.Sleep(100);
        }
      }));

      _ObjThread.Start();

      _ProjectileThread = new Thread(new ThreadStart(() => {
        while (true) {
          if (_Obj.GetY() == _Player.GetY()) {

            // enemy has 10% chance of shooting player when in range
            Random rand = new Random();
            if (rand.Next(30) != 0) {
              continue;
            }

            IObject bullet = _Obj.FireProjectile();

            ObjectRegistry.Instance.RegisterObj(bullet);

            Animator projectileAnimator = new Animator(bullet);

            projectileAnimator.Left(_Canvas, 10, null,
                (collision) => {
                  if(collision.Target is Player) {
                    collision.Target.Status = ObjectStatus.Shot;
                  }
                },
               () => {
                 _Canvas.ClearObj(bullet);
                 _Obj.loadProjectile(new Bullet(0, 0));
                 ObjectRegistry.Instance.RemoveObj(bullet);
               });
          }

          if(_Obj.Status != ObjectStatus.Active) {
            break;
          }

          System.Threading.Thread.Sleep(100);
        }
      }));

      _ProjectileThread.Start();
    }

    public override void Stop() {
      _ObjThread.Abort();
    }
  }
}
