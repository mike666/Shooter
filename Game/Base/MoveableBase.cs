using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  abstract class MoveableBase : ObjectBase, IMoveable {
        
    public MoveableBase(int x, int y) : base(x, y) { }
    
    public void Move(int x, int y) {
      int newX = _X + x;
      int newY = _Y + y;

      if (CanMove(newX, newY)) {
        RemovePos(_X, _Y);

        _X = newX;
        _Y = newY;

        WritePos(_X, _Y);

        List<IObject> collidedObjects = Game.Instance.CollisionDetector.Detect();

        if (collidedObjects.Count > 0) {
          collidedObjects.ForEach(c => c.Clear());
        }
      }
    }
 
    /// <summary>
    /// Make sure that the new coordinate is not placed outside the
    /// console window (since that will cause a runtime crash
    /// </summary>
    public bool CanMove(int x, int y) {
      if (x < 0 || x >= Console.WindowWidth)
        return false;

      if (y < 0 || y >= Console.WindowHeight)
        return false;

      return true;
    }


  }
}
