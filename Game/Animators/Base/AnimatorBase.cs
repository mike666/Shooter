using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game {
  public abstract class AnimatorBase : IAnimator {
    protected bool _IsAnimating = false;

    public virtual void Animate(ICanvas canvas, IObject obj, int speed) {
      Thread animationThread = new Thread(new ThreadStart(() => {
        _IsAnimating = true;
        DoAnimation(canvas, obj, speed);

        _IsAnimating = false;
      }));

      animationThread.Start();
    }

    public abstract void DoAnimation(ICanvas canvas, IObject obj, int speed);

    public virtual bool IsAnimating() {
      return _IsAnimating;
    }

    public virtual void Stop() {
      _IsAnimating = false;
    }

  }
}
