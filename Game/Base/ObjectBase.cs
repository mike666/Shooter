using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  abstract class ObjectBase : IObject {
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

    public virtual void Render() {
      RemovePos(_X, _Y);
      WritePos(_X, _Y);
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
       
    /// <summary>
    /// Overpaint the old hero
    /// </summary>
    protected void WritePos(int x, int y) {
      Console.BackgroundColor = ConsoleColor.White;
      Console.SetCursorPosition(x, y);
      Console.Write(Graphic);
    }

    /// <summary>
    /// Overpaint the old hero
    /// </summary>
    protected void RemovePos(int x, int y) {
      Console.BackgroundColor = ConsoleColor.White;
      Console.SetCursorPosition(x, y);
      Console.Write(new string(' ', Graphic.Length + 2));
    }

    /// <summary>
    /// Overpaint the old hero
    /// </summary>
    public void Clear() {
      RemovePos(_X, _Y);
    }
  }
}
