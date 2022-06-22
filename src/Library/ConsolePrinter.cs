namespace ChatBotProject
{
  /// <summary>
  /// Esta clase hace uso del Dependency Inversion Principle, ya que depende de una abstracción para implementar el metodo en vez de una clase normal.
  /// </summary>
  public class ConsolePrinter : IPrinter
  {
    public void printBoard(Board board)
    {
      Console.Write("   "); // Espacio entre columnas y filas

      // Imprimir header de columnas
      for(int col = 0; col < board.getWidth(); col++)
      {
        Console.Write($"{board.getHeaderLetters()[col]} ");
      }

      Console.WriteLine(); // Final del header
  
      for(int row = 0; row < board.getHeight(); row++)
      {  
        Console.Write($"{row + 1}".PadRight(3)); // Imprimir rows sidebar

        for(int col = 0; col < board.getWidth(); col++){ 
          Console.Write($"{board.getFields()[row, col]}".PadRight(2));
        }

        Console.WriteLine(); // Idem
      }
    }
  }
}
