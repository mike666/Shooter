using System;
using System.Threading;

namespace Game {
  public class Animator : IAnimator {
    protected IObject _Obj = null;
    protected bool _IsAnimating = false;
    
    public Animator(IObject obj) {
      _Obj = obj;
    }
    
    public virtual bool IsAnimating() {
      if (_Obj.Status != ObjectStatus.Active) {
        Stop();
      }

      return _IsAnimating;
    }

    public virtual void Stop() {
      _IsAnimating = false;
    }

    public void Left(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(-1, 0), speed, units, onCollision, onAnimateFinish);
    }

    public void Right(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(1, 0), speed, units, onCollision, onAnimateFinish);
    }

    public void Up(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(0, -1), speed, units, onCollision, onAnimateFinish);
    }

    public void Down(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(0, 1), speed, units, onCollision, onAnimateFinish);
    }

    public void UpRight(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(1, -1), speed, units, onCollision, onAnimateFinish);
    }

    public void DownRight(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(1, 1), speed, units, onCollision, onAnimateFinish);
    }

    public void UpLeft(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(-1, -1), speed, units, onCollision, onAnimateFinish);
    }

    public void DownLeft(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      Animate(canvas, new Coordinate(-1, 1), speed, units, onCollision, onAnimateFinish);
    }


    public virtual void Animate(ICanvas canvas, Coordinate cooridate, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null) {
      _IsAnimating = true;

      int unitCount = 0;

        while (canvas.ObjCanMove(_Obj, cooridate.X, cooridate.Y) && IsAnimating()) {
          unitCount++;

          IObjectCollision collision = canvas.MoveObj(_Obj, cooridate.X, cooridate.Y);

          if (collision != null && onCollision != null) {
            onCollision.Invoke(collision);
          }

          if (units.HasValue && units.Value == unitCount) {
            break;
          }

          System.Threading.Thread.Sleep(speed);
        }

        if (onAnimateFinish != null) {
          onAnimateFinish.Invoke();
        }

        _IsAnimating = false;
    }
  }
}
