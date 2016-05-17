using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  class Bullet : ObjectBase {
    
    public Bullet(int x, int y) : base(x, y) {
      SetGraphic("->");
    }

    public override void Render() {
    }
  }
}
