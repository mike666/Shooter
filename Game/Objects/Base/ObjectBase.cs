using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  public abstract class ObjectBase : IObject {
    protected string _Graphic = "";
    protected int _X = 0;
    protected int _Y = 0;
        
    protected string Graphic {
      get { return _Graphic; }
    }
        
    public ObjectBase(int x, int y) {
      _X = x;
      _Y = y;
    }
    
    public void SetGraphic(string graphic) {
      _Graphic = graphic;
    }

    public string GetGraphic() {
      return _Graphic;
    }

    public int GetX() {
      return _X;
    }

    public int GetY() {
      return _Y;
    }

    public void SetPos(int x, int y) {
      _X = x;
      _Y = y;
    }
  }
}
