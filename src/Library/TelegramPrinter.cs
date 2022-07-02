using System.Text;

namespace ChatBotProject
{
  public class TelegramPrinter : IPrinter
  {
    /// <summary>
    /// Esta clase hace uso del Dependency Inversion Principle, ya que depende de una abstracción (Interfaz IPrinter) para implementar el metodo en vez de una clase normal.
    /// Se encarga de imprimir el tablero en el mensaje que se envia al usuario.
    /// </summary>

    public void printBoard(Board board)
    {
      StringBuilder helpStringBuilder = new StringBuilder("");

      helpStringBuilder.Append("   "); // Espacio entre columnas y filas

      // Imprimir header de columnas
      for(int col = 0; col < board.getWidth(); col++)
      {
        helpStringBuilder.Append($"{board.getHeaderLetters()[col]} ");
      }

      helpStringBuilder.Append("\n"); // Final del header
  
      for(int row = 0; row < board.getHeight(); row++)
      {
        helpStringBuilder.Append($"{row + 1}".PadRight(3)); // Imprimir rows sidebar

        for(int col = 0; col < board.getWidth(); col++){ 
          helpStringBuilder.Append($"{board.getFields()[row, col]}".PadRight(2));
        }

        helpStringBuilder.Append("\n");
      }

      Console.WriteLine(helpStringBuilder.ToString());
    }
  }
}
