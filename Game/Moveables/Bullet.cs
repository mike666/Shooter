using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  class Bullet : AnimatedObjectBase {
    
    public Bullet(int x, int y) : base(x, y) {
      SetGraphic("->");
    }

    protected override List<string> GetMoveCommands() {
      string commands = "RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR";

      return commands.ToArray().Select(c => c.ToString()).ToList<string>();
    }

  }
}
