namespace ChatBotProject
{
  /// <summary>
  /// Se usa el Dependency Inversion Principle, ya que esta interfaz se implementa en la clase ConsolePrinter de forma que la clase ConsolePrinter no dependa de otra clase que no sea una abstracción
  /// </summary>
  public interface IPrinter
  {
    void printBoard(Board board);
  }
}
