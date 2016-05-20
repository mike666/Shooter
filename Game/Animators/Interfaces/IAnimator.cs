using System;

namespace Game {
  public interface IAnimator {
    void Animate(ICanvas canvas, int speed, Action onAfterAnimate = null);
    bool IsAnimating();
    void Stop();
  }   
}
