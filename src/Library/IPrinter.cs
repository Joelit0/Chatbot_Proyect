namespace ChatBotProject
{
  /// <summary>
  /// Se usa el Dependency Inversion Principle, ya que esta interfaz se implementa en la clase ConsolePrinter de forma que la clase ConsolePrinter no dependa de otra clase que no sea una abstracción.
  /// Esta clase es una interfaz para implementar en la clase ConsolePrinter.
  /// </summary>
  public interface IPrinter
  {
    /// <summary>
    /// Se usa el Dependency Inversion Principle, ya que esta interfaz se implementa en la clase ConsolePrinter de forma que la clase ConsolePrinter no dependa de otra clase que no sea una abstracción.
    /// Esta clase es una interfaz para implementar en la clase ConsolePrinter.
    /// </summary>
    void printBoard(Board board, long id);
  }
}
