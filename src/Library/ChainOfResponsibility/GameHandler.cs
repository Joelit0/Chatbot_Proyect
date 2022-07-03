using System;
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
            foreach(User player in UsersList.GetInstance().Users)
            {
              if (player.ID == chatid)
              {
                this.Player = player;
              }
            }
            if (this.State == GameState.Start && this.Player.Name == "")
            {
              this.State = GameState.Start;
              response = "Usted no ha iniciado sesión, porfavor use /LogIn.";
            }
            else if (this.State == GameState.Start && this.Player.Name != "")
            {
              this.State = GameState.AwaitingRivalNameForGame;
              response = "Introduce el nombre del usuario que quieres enfrentar";
            }
            else if (this.State == GameState.AwaitingRivalNameForGame)
            { 
              bool registeredRival = false;  
              foreach(User player in UsersList.GetInstance().Users)
              {
                  if (player.Name == message)
                  {
                    registeredRival = true;
                    this.RivalPlayer = player;
                  }
              }
              if (registeredRival == true && this.RivalPlayer.InGame == false)
              {
                List<User> matchedUsers = new List<User>();
                matchedUsers.Add(Player);
                matchedUsers.Add(RivalPlayer);
                GamesList.GetInstance().AddGame(matchedUsers,);
                this.State = GameState.CheckingAnswerForGame;
                this.Player.InGame = true;
                TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, $"¡Prepárate!{this.Player.Name} te ha desafiado a una partida!");
                response = "Se ha creado la partida, usa /Game para dirigirte a tu partida, buena suerte!";
              }
              else
                  {
                    this.State = GameState.AwaitingRivalNameForGame;
                    response = "Este usuario no existe o se encuentra en partida. Porfavor inténtalo de nuevo o prueba ponerte en contacto con dicho usuario";
                    this.Player.InGame = false;
                  }
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

            ///-Start: Es el estadio inicial del comando. En este comando pide el mensaje de invitación para
            ///asi pasar al siguiente estado.
            Start,

            AwaitingRivalNameForGame,

            CheckingAnswerForGame
      
        }
    }
}