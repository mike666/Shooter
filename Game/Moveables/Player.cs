using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game {
  class Player : MoveableBase {
        private Bullet _Bullet = null;

    public Player(int x, int y) : base(x, y) {
      SetGraphic(":)");

      _Bullet = new Bullet(_X + Graphic.Length, _Y);

      Game.Instance.CollisionDetector.RegisterObj(_Bullet);
    }

    public void Fire() {
      if(_Bullet.IsAnimating()) {
        return;
      }

      _Bullet.SetPos(_X + Graphic.Length, _Y);
      _Bullet.Render();

      Thread thread = new Thread(new ThreadStart(() => {
        _Bullet.Animate(10, false, false);
      }));

      thread.Start();
    }
    
  }
}
