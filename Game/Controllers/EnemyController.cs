using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  /// <summary>
  ///
  /// </summary>
  public class EnemyController : ControllerBase {
    private ICanvas _Canvas = null;
    private Enemy _Enemy = null;
    private Player _Player = null;
    private Thread _EnemyThread = null;
    private Thread _ProjectileThread = null;

    public EnemyController(ICanvas canvas, Enemy enemy, Player player) {
      _Canvas = canvas;
      _Enemy = enemy;
      _Player = player;
    }
    
    public override IObject GetObject() {
      return _Enemy;
    }
      
    public override void Start() {
      Animator enemyAnimator = new Animator(_Enemy);

      Dictionary<string, Action<ICanvas, int, int?, Action<IObjectCollision>, Action>> possibleDirections = GetPossibleDirections(enemyAnimator);
           
      _EnemyThread = new Thread(new ThreadStart(() => {
        while (true) {
          Random rand = new Random();

          List<string> targetDirections = new List<string>();

          if (_Enemy.GetY() > _Player.GetY()) {
            targetDirections.Add("Up");

            if (_Enemy.GetX() > _Player.GetX()) {
              targetDirections.Add("Left");
              targetDirections.Add("UpLeft");
            } else {
              targetDirections.Add("Right");
              targetDirections.Add("UpRight");
            }

          } else {
            targetDirections.Add("Down");

            if (_Enemy.GetX() > _Player.GetX()) {
              targetDirections.Add("Left");
              targetDirections.Add("DownLeft");
            } else {
              targetDirections.Add("Right");
              targetDirections.Add("DownRight");
            }
          }


          string direction = targetDirections[rand.Next(targetDirections.Count)];
          int speed =  rand.Next(100, 200);
          int? distance = rand.Next(10, 30);

          if (possibleDirections.ContainsKey(direction)) {
            possibleDirections[direction].Invoke(_Canvas, speed, distance, null, null);
          }

          System.Threading.Thread.Sleep(100);
        }
      }));

      _EnemyThread.Start();

      _ProjectileThread = new Thread(new ThreadStart(() => {
        while (true) {
          if (_Enemy.GetY() == _Player.GetY()) {

            // enemy has 25% chance of shooting player
            Random rand = new Random();
            if (rand.Next(4) != 0) {
             // continue;
            }

            IObject bullet = _Enemy.FireProjectile();

            ObjectRegistry.Instance.RegisterObj(bullet);

            Animator projectileAnimator = new Animator(bullet);

            projectileAnimator.Left(_Canvas, 10, null,
                (collision) => {
               //   projectileAnimator.Stop();
                },
               () => {
                 _Canvas.ClearObj(bullet);
                 _Enemy.loadProjectile(new Bullet(0, 0));
                 ObjectRegistry.Instance.RemoveObj(bullet);
               });
          }

          if(_EnemyThread.ThreadState == ThreadState.Aborted) {
            break;
          }

          System.Threading.Thread.Sleep(100);
        }
      }));

      _ProjectileThread.Start();
    }

    public override void Stop() {
      _EnemyThread.Abort();
    }
  }
}
