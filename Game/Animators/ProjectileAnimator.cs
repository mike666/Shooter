using System;

namespace Game {
  public class ProjectileAnimator : AnimatorBase {
    public ProjectileAnimator(IObject obj) {
      _Obj = obj;
    }

    public override void DoAnimation(ICanvas canvas, int speed, Action<IObjectCollision> onCollision = null) {
      while (canvas.ObjCanMove(_Obj, 1, 0) && _IsAnimating) {

        IObjectCollision collision = canvas.MoveObj(_Obj, 1, 0);
        
        if (collision != null && onCollision != null) {
          onCollision.Invoke(collision);
        }

        System.Threading.Thread.Sleep(speed);
      }

      canvas.ClearObj(_Obj);
    }
  }
}
