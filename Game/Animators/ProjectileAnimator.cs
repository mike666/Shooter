namespace Game {
  public class ProjectileAnimator : AnimatorBase {
    public ProjectileAnimator(IObject obj) {
      _Obj = obj;
    }

    public override void DoAnimation(ICanvas canvas, int speed) {
      while (canvas.ObjCanMove(_Obj, 1, 0) && _IsAnimating) {

        IObjectCollision collision = canvas.MoveObj(_Obj, 1, 0);
        
        if (collision != null) {
          Stop();

          canvas.ClearObj(collision.Subject);
          canvas.ClearObj(collision.Target);
          
          ObjectRegistry.Instance.RemoveObj(collision.Target);
        }

        System.Threading.Thread.Sleep(speed);
      }

      canvas.ClearObj(_Obj);
    }
  }
}
