using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  class Block : ObjectBase {
    
    public Block(int x, int y) : base(x, y) {
      SetGraphic("[]");
    }
  }
}
