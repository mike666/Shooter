using System;
using System.Collections.Generic;
using System.Threading;

namespace Game {
  /// <summary>
  ///
  /// </summary>
  public abstract class ControllerBase : IController {
     
    protected virtual Dictionary<string, Action<ICanvas, int, int?, Action<IObjectCollision>, Action>> GetPossibleDirections(IAnimator animator) {
      return new Dictionary<string, Action<ICanvas, int, int?, Action<IObjectCollision>, Action>> {
        { "Left", animator.Left },
        { "Right", animator.Right },
        { "Up", animator.Up },
        { "Down", animator.Down },
        { "UpRight", animator.UpRight },
        { "UpLeft", animator.UpLeft },
        { "DownRight", animator.DownRight },
        { "DownLeft", animator.DownLeft }
      };
    }

    public abstract IObject GetObject();

    public abstract void Start();

    public abstract void Stop();
  }
}
