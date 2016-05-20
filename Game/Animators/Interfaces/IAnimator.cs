using System;

namespace Game {
  public interface IAnimator {
    void Animate(ICanvas canvas, int speed, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    bool IsAnimating();
    void Stop();
  }   
}
