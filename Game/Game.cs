using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game {
  class Game {

    public static Game _Singleton = null;
    private Player _Player = null;
    private List<IObject> _GameObjects = null;
    private CollisionDetector _CollisionDetector = null;

    public CollisionDetector CollisionDetector {
      get {
        return _CollisionDetector;
      }
    }
     

    public static Game Instance {
      get {
        if(_Singleton == null) {
          _Singleton = new Game();
        }

        return _Singleton;
      }
    }
           
    private Game() {
      _GameObjects = new List<IObject>();
      _CollisionDetector = new CollisionDetector();
    }
      

    public void Start() {
      InitGame();
    }
    
    public void Redraw() {
      Console.Clear();

      foreach (IObject obj in _GameObjects) {
        obj.Render();
      }

      _Player.Render();
    }

    /// <summary>`
    /// Initiates the game by painting the background
    /// and initiating the hero
    /// </summary>
    public void InitGame() {
      SetBackgroundColor();

      _GameObjects = new List<IObject>() {
        new Enemy(50, 5),
        new Block(25, 10),
        new Block(25, 15),
        new Block(25, 17)
      };

      _Player = new Player(0, 0);
    
      foreach(IObject obj in _GameObjects) {
        _CollisionDetector.RegisterObj(obj);
      }

      _CollisionDetector.RegisterObj(_Player);

      Thread playerThread = new Thread(new ThreadStart(() => {

        _Player.Render();
        
        ConsoleKeyInfo keyInfo;
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape) {
          switch (keyInfo.Key) {
            case ConsoleKey.UpArrow:
              Redraw();
              _Player.Move(0, -1);
              break;

            case ConsoleKey.RightArrow:
              Redraw();
              _Player.Move(1, 0);
              break;

            case ConsoleKey.DownArrow:
              Redraw();
              _Player.Move(0, 1);
              break;

            case ConsoleKey.LeftArrow:
              Redraw();
              _Player.Move(-1, 0);
              break;
            case ConsoleKey.Spacebar:
              Redraw();
              _Player.Fire();
              break;
          }
        }

      }));

      playerThread.Start();
    
        //_Enemy1.Animate(100, true, true);

      foreach (IObject obj in _GameObjects) {
        obj.Render();

        if(obj is IAnimated) {
         // ((IAnimated)obj).Animate(200, true, true);
        }

      }
    }

    private void SetBackgroundColor() {
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Clear(); //Important!
    }

  }
}
