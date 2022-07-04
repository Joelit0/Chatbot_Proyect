using System;
using System.Text;
using System.Collections.Generic;
using Telegram.Bot;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/Matchamking".
    /// </summary>
    public class GameHandler : BaseHandler
    {
        /// <summary>
        /// Utilizamos esta propiedad para saber que usuario esta usando el handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }

        /// <summary>
        /// Utilizamos esta propiedad para saber a que usuario vamos a invitar a la partida.
        /// </summary>
        /// <value></value>
        public User RivalPlayer { get; private set; }

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public GameState State { get; private set; }

        /// <summary>
        /// El game actual.
        /// </summary>
        public Game CurrentGame { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /Game.
        /// </summary>

        public GameHandler(BaseHandler next) : base(next)
        {
          this.Keywords = new string[] { "/Game" };
          this.State = GameState.Start;
        }

        protected override bool CanHandle(string message)
        {
          if (this.State == GameState.Start)
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
            foreach(Game game in GamesList.GetInstance().Games)
            {
              if (game.getInMatchUsers()[0].ID == chatid)
              {
                this.Player = game.getInMatchUsers()[0];
                this.RivalPlayer = game.getInMatchUsers()[1];
                this.CurrentGame = game;
              }
              else if (game.getInMatchUsers()[1].ID == chatid)
              {
                this.RivalPlayer = game.getInMatchUsers()[1];
                this.Player = game.getInMatchUsers()[0];
                this.CurrentGame = game;
              }
              else
              {
                TelegramBot.GetInstance().botClient.SendTextMessageAsync(chatid, "No tiene ninguna partida creada, utilice /Matchmaking para crear una.");
              }
            }

            if (this.State == GameState.Start && this.Player.Name != "" && this.RivalPlayer.Name != "")
            {
              this.State = GameState.ReadyToStartConfirmation;
              StringBuilder GameLobbyHelpStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                            .Append("/Ready: Listo para iniciar la partida\n")
                                                                            .Append("/Leave: Salir de la partida, esta acción eliminara la partida actual.\n");
              response = GameLobbyHelpStringBuilder.ToString();
            }
            else if (this.State == GameState.ReadyToStartConfirmation)
            {
              if (message == "/Ready")
              {
                if (this.Player.ID == chatid)
                {
                  this.Player.SetReadyToStartMatch(true);
                }
                else if (this.RivalPlayer.ID == chatid)
                {
                  this.RivalPlayer.SetReadyToStartMatch(true);
                  this.RivalPlayer.State = "";
                }

                this.State = GameState.ReadyToStart;

                response = "sd";
              }
              else if (message == "/Leave")
              {
                Player.InGame = false;
                Player.ReadyToStartMatch = false;
                RivalPlayer.InGame = false;
                RivalPlayer.ReadyToStartMatch = false;
                GamesList.GetInstance().RemoveGame(this.CurrentGame);
                response = "La partida ha sido cancelada porque uno de los jugadores se ha salido.";
                TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
              }
              else
              {
                this.State = GameState.ReadyToStartConfirmation;
                response = "Comando inválido, por favor intentelo nuevamente utilizando /Ready o /Leave";
              }
            }
            else if (this.State == GameState.ReadyToStart && this.Player.ReadyToStartMatch && this.RivalPlayer.ReadyToStartMatch)
            {
              Console.WriteLine("x");
              this.CurrentGame.StartGame();
              response = "La partida ha iniciado.";
            }
            else if (this.State == GameState.ReadyToStart)
            {
              this.State = GameState.Start;
              response = "Esperando al otro usuario...";
            }
            else
            {
              response = string.Empty;
            }
        }
        
        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
          this.State = GameState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum GameState
        {
          ///-Start: Es el estado inicial del comando. En este comando pide el mensaje de invitación para
          ///asi pasar al siguiente estado.
          Start,
          ReadyToStartConfirmation,
          ReadyToStart,
          CheckingAnswerForGame
        }
    }
}