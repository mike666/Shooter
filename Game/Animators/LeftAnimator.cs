namespace Game {
  public class LeftAnimator : AnimatorBase {
    public LeftAnimator(IObject obj) {
      _Obj = obj;
    }

    public override void DoAnimation(ICanvas canvas, int speed) {
      while (canvas.ObjCanMove(_Obj, -1, 0) && _IsAnimating) {

        canvas.MoveObj(_Obj, -1, 0);

        System.Threading.Thread.Sleep(speed);
      }

      canvas.ClearObj(_Obj);
    }
  }
}
