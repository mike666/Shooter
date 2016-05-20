using System;
using System.Collections.Generic;

namespace Game {
  class ConsoleCanvas : ICanvas {
    private ICollisionDetector _CollisionDetector = null;

    public ConsoleCanvas(ICollisionDetector collisionDetector) {
      _CollisionDetector = collisionDetector;
           
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Clear(); //Important!
    }
    
    public void RenderObj(IObject obj) {
      RemovePos(obj.GetGraphic(), obj.GetX(), obj.GetY());
      WritePos(obj.GetGraphic(), obj.GetX(), obj.GetY());
    }
    
    public IObjectCollision MoveObj(IObject obj, int incrX, int incrY) {
      IObjectCollision collision = null;

      if (ObjCanMove(obj, incrX, incrY)) {
        RemovePos(obj.GetGraphic(), obj.GetX(), obj.GetY());

        int newX = obj.GetX() + incrX;
        int newY = obj.GetY() + incrY;

        obj.SetPos(newX, newY);

        WritePos(obj.GetGraphic(), newX, newY);

        collision = _CollisionDetector.Detect(obj);
      }

      return collision;
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

      if (newY < 2 || newY >= Console.WindowHeight) {
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
