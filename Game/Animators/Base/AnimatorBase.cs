using System;
using System.Threading;

namespace Game {
  public abstract class AnimatorBase : IAnimator {
    protected IObject _Obj = null;
    protected bool _IsAnimating = false;
    
    public virtual void Animate(ICanvas canvas, int speed, Action onAfterAnimate = null) {
      Thread animationThread = new Thread(new ThreadStart(() => {
        _IsAnimating = true;
        DoAnimation(canvas, speed);

        if(onAfterAnimate != null) {
          onAfterAnimate.Invoke();
        }

        _IsAnimating = false;
      }));

      animationThread.Start();
    }

    public abstract void DoAnimation(ICanvas canvas, int speed);

    public virtual bool IsAnimating() {
      return _IsAnimating;
    }

    public virtual void Stop() {
      _IsAnimating = false;
    }
  }
}
