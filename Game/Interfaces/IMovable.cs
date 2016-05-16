using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  interface IMoveable {
    void Move(int x, int y);
    bool CanMove(int x, int y);
  }
}
