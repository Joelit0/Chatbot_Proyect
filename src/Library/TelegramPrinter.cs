using System.Text;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace ChatBotProject
{
  public class TelegramPrinter : IPrinter
  {
    /// <summary>
    /// Esta clase hace uso del Dependency Inversion Principle, ya que depende de una abstracción (Interfaz IPrinter) para implementar el metodo en vez de una clase normal.
    /// Se encarga de imprimir el tablero en el mensaje que se envia al usuario.
    /// </summary>

    public void printBoard(Board board, long id)
    {
      StringBuilder boardInText = new StringBuilder("");

      boardInText.Append("__"); // Espacio entre columnas y filas

      // Imprimir header de columnas
      for(int col = 0; col < board.getWidth(); col++)
      {
        boardInText.Append($"{board.getHeaderLetters()[col]} ");
      }

      boardInText.Append("\n"); // Final del header
  
      for(int row = 0; row < board.getHeight(); row++)
      {
        if (row != 9)
        {
          boardInText.Append($"_{row + 1}".PadRight(3)); // Imprimir rows sidebar
        }
        else
        {
          boardInText.Append($"{row + 1}".PadRight(3)); // Imprimir rows sidebar
        }

        for(int col = 0; col < board.getWidth(); col++){ 
          boardInText.Append($"{board.getFields()[row, col]}".PadRight(3));
        }

        boardInText.Append("\n"); // Final de la fila
      }

      TelegramBot.GetInstance().botClient.SendTextMessageAsync(id, boardInText.ToString());
    }
  }
}
