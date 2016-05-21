using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  public class Enemy : Shooter {
    
    public Enemy(int x, int y) : base(x, y) {
      SetGraphic("(:");
    }
  }
}
