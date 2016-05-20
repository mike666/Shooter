namespace Game {
  /// <summary>
  /// Represents a map coordinate
  /// </summary>
  public interface IObjectCollision {
    IObject Subject { get; set; }
    IObject Target { get; set; }
  }
}
