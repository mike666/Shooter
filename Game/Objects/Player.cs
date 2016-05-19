using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game {
  class Player : ObjectBase {
    private IObject _Projectile;

    public Player(IObject projectile, int x, int y) : base(x, y) {
      SetGraphic(":)");

      _Projectile = projectile;
    }

    public IObject Fire() {
      _Projectile.SetPos(_X + Graphic.Length, _Y);

      return _Projectile;

      /*
      if (_Projectile.IsAnimating()) {
        return;
      }

      _Projectile.SetPos(_X + Graphic.Length, _Y);
    
      Thread thread = new Thread(new ThreadStart(() => {
        _Projectile.Animate();
      }));

      thread.Start();*/
    }
    
  }
}
