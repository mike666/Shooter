using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  interface IAnimated {
    void Animate(int speed, bool repeat, bool redraw);
    bool IsAnimating();
    void Stop();
  }   
}
