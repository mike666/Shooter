using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  public interface IAnimator {
    void Animate(ICanvas canvas, IObject obj, int speed);
    bool IsAnimating();
    void Stop();
  }   
}
