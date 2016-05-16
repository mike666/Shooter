using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  interface IObject {
    int GetX();
    int GetY();

    void SetPos(int x, int y);
    void SetGraphic(string graphic);
    void Render();
    void Clear();
  }
}
