using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  public abstract class AnimatedObjectBase : IAnimatedObject, IObject {
    protected IObject _Obj = null;
    protected bool _IsAnimating = false;
            
    public AnimatedObjectBase(IObject obj) {
      _Obj = obj;
    }
    
    public bool IsAnimating() {
      return _IsAnimating;
    }

    public void Animate() {
      _IsAnimating = true;

      doAnimation();

      _IsAnimating = false;
    }

    public abstract void doAnimation();

    public void Stop() {
      _IsAnimating = false;
    }

    public int GetX() {
      return _Obj.GetX();
    }

    public int GetY() {
      return _Obj.GetY();
    }

    public void SetPos(int x, int y) {
      _Obj.SetPos(x, y);
    }

    public void SetGraphic(string graphic) {
      _Obj.SetGraphic(graphic);
    }

    public void Render() {
      _Obj.Render();
    }

    public void Move(int x, int y) {
      _Obj.Move(x, y);
    }

    public bool CanMove(int x, int y) {
     return  _Obj.CanMove(x, y);
    }

    public void Clear() {
      _Obj.Clear();
    }
  }
}
