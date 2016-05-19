using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  class ConsoleCanvas : ICanvas {

    public ConsoleCanvas() {
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Clear(); //Important!
    }
    
    public void RenderObj(IObject obj) {
      RemovePos(obj.GetGraphic(), obj.GetX(), obj.GetY());
      WritePos(obj.GetGraphic(), obj.GetX(), obj.GetY());
    }


    public void MoveObj(IObject obj, int incrX, int incrY) {
      if (ObjCanMove(obj, incrX, incrY)) {
        RemovePos(obj.GetGraphic(), obj.GetX(), obj.GetY());

        int newX = obj.GetX() + incrX;
        int newY = obj.GetY() + incrY;

        obj.SetPos(newX, newY);

        WritePos(obj.GetGraphic(), newX, newY);

        ObjectCollision collision = CollisionDetector.Detect(obj);

        if (collision != null) {
          ClearObj(collision.Subject);
          ClearObj(collision.Target);
          
          ObjectRegistry.Instance.RemoveObj(collision.Target);
        }
      }
    }

    /// <summary>
    /// Overpaint the old hero
    /// </summary>
    public void RemovePos(string graphic, int x, int y) {
      Console.SetCursorPosition(x, y);
      Console.Write(new string(' ', graphic.Length + 2));
    }


    /// <summary>
    /// Overpaint the old hero
    /// </summary>
    public void WritePos(string graphic, int x, int y) {
      Console.SetCursorPosition(x, y);
      Console.Write(graphic);
    }

    public void ClearObj(IObject obj) {
      Console.SetCursorPosition(obj.GetX(), obj.GetY());
      Console.Write(new string(' ', obj.GetGraphic().Length + 2));
    }

    public bool ObjCanMove(IObject obj, int incrX, int incrY) {
      int newX = obj.GetX() + incrX;
      int newY = obj.GetY() + incrY;

      if (newX < 0 || newX >= Console.WindowWidth) {
        return false;
      }

      if (newY < 0 || newY >= Console.WindowHeight) {
        return false;
      }

      return true;
    }

    public void ReDrawObjects(List<IObject> objects) {
      Console.Clear();

      foreach (IObject obj in objects) {
        RenderObj(obj);
      }
    }
  }
}
