
using System;
using System.Text;
using System.Collections.Generic;
using Telegram.Bot;

namespace ChatBotProject
{
  /// <summary>
  /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/GameVsIA".
  /// </summary>
  public class GameVsIAHandler : BaseHandler
  {
    /// <summary>
    /// Utilizamos esta propiedad para saber que usuario esta usando el handler.
    /// </summary>
    /// <value></value>
    public User Player { get; private set; }

    /// <summary>
    /// El estado del comando.
    /// </summary>
    public GameVsIAState State { get; private set; }

    /// <summary>
    /// El game actual.
    /// </summary>
    public GameVsIA CurrentGame { get; private set; }

    /// <summary>
    /// Esta clase procesa el mensaje /GameVsIA.
    /// </summary>

    public GameVsIAHandler(BaseHandler next) : base(next)
    {
      this.Keywords = new string[] { "/GameVsIA" };
      this.State = GameVsIAState.Start;
    }

    protected override bool CanHandle(string message)
    {
      if (this.State == GameVsIAState.Start)
      {
        return base.CanHandle(message);
      }
      else
      {
        return true;
      }
    }

    /// <summary>
    /// Procesa todos los mensajes y retorna true siempre.
    /// </summary>
    /// <param name="message">El mensaje a procesar.</param>
    /// <param name="chatid">La id del chat del usuario, la utilizamos para poder indicar que usuario es el que esta usando el bot..</param>
    /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo ser procesado.</param>
    /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
    protected override void InternalHandle(string message, long chatid, out string response)
    {
      // Este for lo que hace es iterar cada GameVsIa para obtener la partida en la que está el usuario.
      // En base a eso, si encuentra un game del usuario, setea el usuario como el Player y setea ese Game como CurrentGame
      foreach(GameVsIA game in GamesVsIAList.GetInstance().GamesVsIA)
      {
        if (game.getPlayer().ID == chatid) // Si el Player del game es igual al usuario que mandó el mensaje
        {
          this.Player = game.getPlayer();
          this.CurrentGame = game;
        }
      }

      // Si no se encontró un game para ese usuario
      if (this.Player == null && this.CurrentGame == null)
      {
        // Aviso al usuario que no tiene ninguna partida
        TelegramBot.GetInstance().botClient.SendTextMessageAsync(chatid, "No tiene ninguna partida creada, utilice /Matchmaking para crear una.");
      }

      // Si el estado es Start y se encontró un Player
      if (this.State == GameVsIAState.Start && this.Player.Name != "")
      {
        this.State = GameVsIAState.ReadyToPlayConfirmation; // Cambio el estado
        
        // Se crea un StringBuilder en el cual se concatenan los comandos a mostrarle al usuario
        // Dichos comandos son para iniciar o salir de la partida 
        StringBuilder GameLobbyHelpStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                      .Append("/Ready: Listo para iniciar la partida\n")
                                                                      .Append("/Leave: Salir de la partida, esta acción eliminara la partida actual.\n");
        response = GameLobbyHelpStringBuilder.ToString(); // Igualo la response al StringBuilder con los comandos
      }
      else if (this.State == GameVsIAState.ReadyToPlayConfirmation) // Si el estado es ReadyToPlayConfirmation
      {
        // Si el mensaje es /Ready
        if (message == "/Ready")
        {
          this.Player.InGame = true; // Pongo al usuario InGame indicando que está en partida
          this.Player.SetReadyToStartMatch(true); // Seteo que el usuario está listo para la partida
          this.CurrentGame.StartGame(); // Llamo al método start game que cambia el estado de la partida
          this.CurrentGame.printPlayerBoard(); // Imprimo el board del usuario
          this.CurrentGame.generateBotShips(); // Genero los ships del boy
          response = "Ahora deberá ingresar el primer barco de 2 posiciones. Los barcos se ingresan de la siguiente manera **A1,A2,A3,A4** dependiendo del tamaño y posicion del barco";
          this.State = GameVsIAState.CreatingShip1; // Cambio el estado a CreatingShip1(Usuario colocando el ship1)
        }
        else if (message == "/Leave") // Si el mensaje es leave
        {
          Player.InGame = false; // Cambio el atributo InGame del usuario a false indicando que no está en partida
          GamesVsIAList.GetInstance().RemoveGame(this.CurrentGame); // Remuevo el game de la lista de GamesVsIA
          response = "Te has salido de la partida";
        }
        else // Si el usuario no coloca ninguno de los comandos válidos
        {
          this.State = GameVsIAState.ReadyToPlayConfirmation; // Vuelvo a este mismo estado para que intente nuevamente
          response = "Comando inválido, por favor intentelo nuevamente utilizando /Ready o /Leave";
        }
      }
      else if (this.State == GameVsIAState.CreatingShip1) // Si el estado es CreatingShip1
      {
        string[] shipPositions = message.Split(','); // Separo el mensaje por comas. Obteniendo así posición por posición
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions)); // Convierto el string array a una lista de strings y añado el ship al board del player
        this.CurrentGame.printPlayerBoard(); // Imprimo el board del player
        response = "Ahora deberá ingresar el primer barco de 3 posiciones.";
        this.State = GameVsIAState.CreatingShip2; // Cambio el estado a CreatingShip2(Usuario colocando el ship2)
      }
      else if (this.State == GameVsIAState.CreatingShip2) // Si el estado es CreatingShip2
      {
        string[] shipPositions = message.Split(',');
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions));
        response = "Ahora deberá ingresar el primer barco de 4 posiciones.";
        this.CurrentGame.printPlayerBoard();
        this.State = GameVsIAState.CreatingShip3; // Cambio el estado a CreatingShip3(Usuario colocando el ship3)
      }
      else if (this.State == GameVsIAState.CreatingShip3) // Si el estado es CreatingShip3
      {
        string[] shipPositions = message.Split(',');
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions));
        response = "Ahora deberá ingresar el primer barco de 5 posiciones.";
        this.CurrentGame.printPlayerBoard();
        this.State = GameVsIAState.CreatingShip4; // Cambio el estado a CreatingShip4(Usuario colocando el ship4)
      }
      else if (this.State == GameVsIAState.CreatingShip4) // Si el estado es CreatingShip4
      {
        string[] shipPositions = message.Split(',');
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions));

        TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, "Tu tablero"); // Mando mensaje al player
        this.CurrentGame.printPlayerBoard(); // Imprimo el tablero del player

        TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, "Tablero del bot"); // Mando mensaje al player
        this.CurrentGame.printBotBoard(); // Imprimo el tablero del bot

        this.State = GameVsIAState.InGame; // Cambio el estado a InGame
        response = "Comience a atacar el Board del Bot. Por ejemplo, A1."; // Le indico que deberá colocar los ships
      }
      else if (this.State == GameVsIAState.InGame && this.Player.ReadyToStartMatch) // Si el estado es InGame y el user está pronto para la partida
      {
        response = string.Empty;

        if(this.CurrentGame.playerBoardHasShips() && this.CurrentGame.botBoardHasShips()) // Si el usuario y el Bot contienen ships en sus boards
        {
          this.CurrentGame.botAttack(); // Genero el ataque del bot llamando a este método de GameVsIA
          this.CurrentGame.attackBotBoard(message); // Ataco al board del bot con la posición que el usuario brindó

          TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, "Tu tablero"); // Mando mensaje al player
          this.CurrentGame.printPlayerBoard(); // Imprimo el tablero del player

          TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, "Tablero del bot"); // Mando mensaje al player
          this.CurrentGame.printBotBoard(); // Imprimo el tablero del bot
  
          this.State = GameVsIAState.InGame; // Devuelvo el estado a InGame para que siga en la partida
          response = "El bot te ha atacado. Ahora ingrese su siguiente ataque.";
        }

        if(!this.CurrentGame.playerBoardHasShips() || !this.CurrentGame.botBoardHasShips()) // Si el usuario o el bot no tiene ships en sus boards
        {
          this.Player.InGame = false; // Cambio el estado del player a que no está en partida
          this.Player.SetReadyToStartMatch(false); // Indico que el usuario ya no esta pronto para jugar una partida
          this.CurrentGame.FinishGame(); // Cambio el estado del GameVsIA a que terminó
          this.State = GameVsIAState.Start; // Vuelvo el estado del Handler a Start

          if (this.CurrentGame.playerBoardHasShips()) // Si el usuario es el que tiene barcos
          {
            response = "Felicidades, ganaste!"; // Mensaje indicando que ganó
            this.CurrentGame.setWinner(this.Player.Name); // Seteo el winner de la partida a el nombre del usuario
          } else // Si el bot es el que tiene barcos
          {
            response = "Has perdido"; // Mensaje al usuario diciendo que perdió
            this.CurrentGame.setWinner("BOT"); // Seteo el winner de la partida como Bot
          }
        }
      }
      else
      {
        response = string.Empty;
        this.State = GameVsIAState.Start;
      }
    }
    
    /// <summary>
    /// Retorna este "handler" al estado inicial.
    /// </summary>
    protected override void InternalCancel()
    {
      this.State = GameVsIAState.Start;
    }
    
    /// <summary>
    /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
    /// </summary>
    public enum GameVsIAState
    {
      ///-Start: Es el estado inicial del comando. En este comando pide el mensaje de invitación para
      ///asi pasar al siguiente estado.
      Start, // Inicio del handler
      ReadyToPlayConfirmation, // Usuario confirmando si jugará la partida
      CreatingShip1, // Usuario agregando el ship 1
      CreatingShip2, // Usuario agregando el ship 2
      CreatingShip3, // Usuario agregando el ship 3
      CreatingShip4, // Usuario agregando el ship 4
      InGame // Usuario en partida
    }

    /// <summary>
    /// Este método convierte un String Array a una List de strings
    /// </summary>
    /// <param name="stringArray">string[] que contiene las posiciones del barco</param>
    /// <returns>Retorna una lista de strings que contienen las posiciones de los barcos</returns>
    private List<string> stringArrayToList(string[] stringArray)
    {
      List<string> stringList = new List<string>(); // Incializa una lista de strings vacía

      foreach(string element in stringArray) // Por cada elemento en el array de strings que llegó por parámetros
      {
        stringList.Add(element); // Lo añado a la lista de strings
      }

      return stringList; // Retorno la lista generada
    }
  }
}
