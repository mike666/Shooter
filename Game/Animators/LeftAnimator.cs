﻿namespace Game {
  public class LeftAnimator : AnimatorBase {
    public LeftAnimator() { }

    public override void DoAnimation(ICanvas canvas, IObject obj, int speed) {
      while (canvas.ObjCanMove(obj, -1, 0) && _IsAnimating) {

        canvas.MoveObj(obj, -1, 0);

        System.Threading.Thread.Sleep(speed);
      }

      canvas.ClearObj(obj);
    }
  }
}
