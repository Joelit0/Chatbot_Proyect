using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using System.Timers;

namespace ChatBotProject
{
  public class Program
  {

    private static IHandler firstHandler;

    static void Main(string[] args)
    {
      /*
      // ===========================================================
      // Lógica del juego
      // List<User> users = new List<User>();

      // User user1 = new User("Joel", "1234");
      // User user2 =new User("Rodrigo", "412f41");

      // users.Add(user1);
      // users.Add(user2);

      // Game game = new Game(users, 20, 10, 2, 0);
      // game.StartGame();
      // ===========================================================

      // // ===========================================================
      */
      // // Lógica del bot

       firstHandler =
         new HelpHandler(
         new RegisterHandler(
         new ProfileHandler(
         new ChangeProfileInfoHandler(
         new MatchmakingHandler(
         new GameHandler(null)
       )))));

       var cts = new CancellationTokenSource();

       TelegramBot.GetInstance().botClient.StartReceiving(
                 HandleUpdateAsync,
                 HandleErrorAsync,
                 new ReceiverOptions()
                 {
                     AllowedUpdates = Array.Empty<UpdateType>()
                 },
                 cts.Token);

      // Console.WriteLine($"Bot is up!");

      // // Esperamos a que el usuario aprete Enter en la consola para terminar el bot.
       Console.ReadLine();

      // // Terminamos el bot.
       cts.Cancel();
      // // ===========================================================
    }

    /// <summary>
    /// Maneja las actualizaciones del bot (todo lo que llega), incluyendo mensajes, ediciones de mensajes,
    /// respuestas a botones, etc. En este ejemplo sólo manejamos mensajes de texto.
    /// </summary>
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      try
      {
        // Sólo respondemos a mensajes de texto
        if (update.Type == UpdateType.Message)
        {
          await HandleMessageReceived(botClient, update.Message);
        }
      }
      catch(Exception e)
      {
        await HandleErrorAsync(botClient, e, cancellationToken);
      }
    }

    /// <summary>
    /// Maneja los mensajes que se envían al bot a través de handlers de una chain of responsibility.
    /// </summary>
    /// <param name="message">El mensaje recibido</param>
    /// <returns></returns>
    private static async Task HandleMessageReceived(ITelegramBotClient botClient, Message message)
    {
      Chat chatInfo = message.Chat;
      Console.WriteLine($"Received a message from {message.From.FirstName} saying: {message.Text}");

      string response = string.Empty;

      foreach(User player in UsersList.GetInstance().Users)
      {
        if (player.ID == message.MessageId)
        {
          player.LastMessage = message.Text;
        }
      }

      string messageContent = message.Text;

      firstHandler.Handle(messageContent, chatInfo.Id , out response);

      if (!string.IsNullOrEmpty(response))
      {
        await botClient.SendTextMessageAsync(message.Chat.Id, response);
      }
    }

    /// <summary>
    /// Manejo de excepciones. Por ahora simplemente la imprimimos en la consola.
    /// </summary>

    public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
      Console.WriteLine(exception.Message);
      return Task.CompletedTask;
    }
  }
}
