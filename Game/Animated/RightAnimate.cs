using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  public class RightAnimate : AnimatedObjectBase {
                
    public RightAnimate(IObject obj) : base(obj) {}

    public override void doAnimation() {
      while (_Obj.CanMove(_Obj.GetX() + 1, _Obj.GetY()) && _IsAnimating) {

        _Obj.Move(1, 0);
                               
        System.Threading.Thread.Sleep(10);
      }

      _Obj.Clear();
    }
  }
}
