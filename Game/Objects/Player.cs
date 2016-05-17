using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game {
  class Player : ObjectBase {
    private AnimatedObjectBase _Projectile;

    public Player(AnimatedObjectBase projectile, int x, int y) : base(x, y) {
      SetGraphic(":)");

      _Projectile = projectile;
    }

    public void Fire() {
      if(_Projectile.IsAnimating()) {
        return;
      }

      _Projectile.SetPos(_X + Graphic.Length, _Y);
    
      Thread thread = new Thread(new ThreadStart(() => {
        _Projectile.Animate();
      }));

      thread.Start();
    }
    
  }
}
