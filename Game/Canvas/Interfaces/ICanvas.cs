using System.Collections.Generic;

namespace Game {
  public interface ICanvas {
    void RenderObj(IObject obj);
    IObjectCollision MoveObj(IObject obj, int incrX, int incrY);
    bool ObjCanMove(IObject obj, int incrX, int incrY);
    void Clear();
    void ClearObj(IObject obj);
    void WritePos(string graphic, int x, int y);
    void WriteCenterPosX(string graphic, int y);
    void RemovePos(string graphic, int x, int y);
    void ReDrawObjects(List<IObject> objects);
    int CanvasWidth();
    int CanvasHeight();

  }   
}
