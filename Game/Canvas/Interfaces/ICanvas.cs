using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
  public interface ICanvas {
    void RenderObj(IObject obj);
    void MoveObj(IObject obj, int incrX, int incrY);
    bool ObjCanMove(IObject obj, int incrX, int incrY);
    void ClearObj(IObject obj);
    void WritePos(string graphic, int x, int y);
    void RemovePos(string graphic, int x, int y);
    void ReDrawObjects(List<IObject> objects);
  }   
}
