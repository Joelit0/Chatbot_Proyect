using System;
using System.Text;
using System.Collections.Generic;
using Telegram.Bot;

namespace ChatBotProject
{
  /// <summary>
  /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/Matchamking".
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
    /// Esta clase procesa el mensaje /Game.
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
      foreach(GameVsIA game in GamesVsIAList.GetInstance().GamesVsIA)
      {
        if (game.getPlayer().ID == chatid)
        {
          this.Player = game.getPlayer();
          this.CurrentGame = game;
        }
      }

      if (this.Player == null && this.CurrentGame == null)
      {
        TelegramBot.GetInstance().botClient.SendTextMessageAsync(chatid, "No tiene ninguna partida creada, utilice /Matchmaking para crear una.");
      }

      if (this.State == GameVsIAState.Start && this.Player.Name != "")
      {
        this.State = GameVsIAState.ReadyToPlayConfirmation;
        StringBuilder GameLobbyHelpStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                      .Append("/Ready: Listo para iniciar la partida\n")
                                                                      .Append("/Leave: Salir de la partida, esta acción eliminara la partida actual.\n");
        response = GameLobbyHelpStringBuilder.ToString();
      }
      else if (this.State == GameVsIAState.ReadyToPlayConfirmation)
      {
        if (message == "/Ready")
        {
          Player.InGame = true;
          this.Player.SetReadyToStartMatch(true);
          this.CurrentGame.printPlayerBoard();
          this.CurrentGame.generateBotShips();
          response = "Ahora deberá ingresar el primer barco de 2 posiciones. Los barcos se ingresan de la siguiente manera **A1,A2,A3,A4** dependiendo del tamaño y posicion del barco";
          this.State = GameVsIAState.CreatingShip1;
        }
        else if (message == "/Leave")
        {
          Player.InGame = false;
          GamesVsIAList.GetInstance().RemoveGame(this.CurrentGame);
          response = "La partida te has salido";
        }
        else
        {
          this.State = GameVsIAState.ReadyToPlayConfirmation;
          response = "Comando inválido, por favor intentelo nuevamente utilizando /Ready o /Leave";
        }
      }
      else if (this.State == GameVsIAState.CreatingShip1)
      {
        string[] shipPositions = message.Split(',');
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions));
        this.CurrentGame.printPlayerBoard();
        response = "Ahora deberá ingresar el primer barco de 3 posiciones.";
        this.State = GameVsIAState.CreatingShip2;
      }
      else if (this.State == GameVsIAState.CreatingShip2)
      {
        string[] shipPositions = message.Split(',');
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions));
        response = "Ahora deberá ingresar el primer barco de 4 posiciones.";
        this.CurrentGame.printPlayerBoard();
        this.State = GameVsIAState.CreatingShip3;
      }
      else if (this.State == GameVsIAState.CreatingShip3)
      {
        string[] shipPositions = message.Split(',');
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions));
        response = "Ahora deberá ingresar el primer barco de 5 posiciones.";
        this.CurrentGame.printPlayerBoard();
        this.State = GameVsIAState.CreatingShip4;
      }
      else if (this.State == GameVsIAState.CreatingShip4)
      {
        string[] shipPositions = message.Split(',');
        this.CurrentGame.AddShipToBoard(stringArrayToList(shipPositions));
        response = "Todos los Ships correctos han sido colocados";
        this.CurrentGame.printPlayerBoard();
      }
      else if (this.State == GameVsIAState.ReadyToPlay && this.Player.ReadyToStartMatch)
      {
        // this.CurrentGame.StartGame();
        response = "La partida ha iniciado.";
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
      Start,
      ReadyToPlayConfirmation,
      ReadyToPlay,
      CreatingShip1,
      CreatingShip2,
      CreatingShip3,
      CreatingShip4
    }

    private List<String> stringArrayToList(string[] stringArray)
    {
      List<String> stringList = new List<String>();

      foreach(string element in stringArray)
      {
        stringList.Add(element);
      }

      return stringList;
    }
  }
}