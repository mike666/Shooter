using System;

namespace Game {
  public interface IAnimator {
    void Animate(ICanvas canvas, Coordinate cooridate, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    bool IsAnimating();
    void Stop();

    void Left(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    void Right(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    void Up(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    void Down(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    void UpRight(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    void DownRight(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    void UpLeft(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
    void DownLeft(ICanvas canvas, int speed, int? units = null, Action<IObjectCollision> onCollision = null, Action onAnimateFinish = null);
  }   
}
