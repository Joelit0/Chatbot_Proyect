namespace ChatBotProject
{
  public class ConsolePrinter : IPrinter
  {
    /// <summary>
    /// Esta clase hace uso del Dependency Inversion Principle, ya que depende de una abstracción (Interfaz IPrinter) para implementar el metodo en vez de una clase normal.
    /// Se encarga de imprimir el tablero en el mensaje que se envia al usuario.
    /// </summary>

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
