namespace Game {
  public class Shooter : ObjectBase {
    protected IObject _Projectile = null;

    public Shooter(int x, int y) : base(x, y) {
      SetGraphic(":)");
    }

    public void loadProjectile(IObject obj) {
      _Projectile = obj;
    }

    public bool HasProjectile() {
      return _Projectile != null;
    }

    public IObject FireProjectile() {
      IObject projectile = _Projectile;
      _Projectile = null;

      if (projectile != null) {
        projectile.SetPos(_X + Graphic.Length, _Y);
      }

      return projectile;
    }
    
  }
}
