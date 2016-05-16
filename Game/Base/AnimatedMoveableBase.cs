using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  abstract class AnimatedObjectBase : MoveableBase, IAnimated {
    protected List<string> _MoveCommands = null;
    protected int _CommandIndex = 0;
    protected bool _IsAnimating = false;

    protected Dictionary<string, Coordinate> _MoveCommandLookup = new Dictionary<string, Coordinate>() {
      {"L", new Coordinate() { X = -1, Y = 0 }},
      {"R", new Coordinate() { X = 1, Y = 0 }},
      {"U", new Coordinate() { X = 0, Y = -1 }},
      {"D", new Coordinate() { X = 0, Y = 1 }}
    };
    
    public AnimatedObjectBase(int x, int y) : base(x, y) {
      _MoveCommands = GetMoveCommands();
    }
    
    public bool IsAnimating() {
      return _IsAnimating;
    }

    public void Animate(int speed, bool repeat, bool redraw) {
      _IsAnimating = true;

      while (_CommandIndex < _MoveCommands.Count() && _IsAnimating) {

        if (_CommandIndex < _MoveCommands.Count()) {

          Coordinate coordinate = _MoveCommandLookup[_MoveCommands[_CommandIndex]];
          Move(coordinate.X, coordinate.Y);
          
          _CommandIndex++;
        }

        if (repeat && _CommandIndex >= _MoveCommands.Count()) {
          _CommandIndex = 0;
        }
                
        if (redraw) {
          Game.Instance.Redraw();
        }

        System.Threading.Thread.Sleep(speed);
      }

      _IsAnimating = false;

      _CommandIndex = 0;
    }
        
    public void Stop() {
      _IsAnimating = false;
    }

    protected abstract List<string> GetMoveCommands();
  }
}
