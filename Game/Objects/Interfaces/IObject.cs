using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  public interface IObject {
    int GetX();
    int GetY();

    void SetPos(int x, int y);
    void SetGraphic(string graphic);
    string GetGraphic();
  }
}
