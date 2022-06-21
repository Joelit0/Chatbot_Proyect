using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "Registrarse".
    /// </summary>
    public class MatchmakingHandler : BaseHandler
    {

        /// <summary>
        /// Los usuarios que usan este handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }

        public User RivalPlayer { get; private set; }
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public MatchmakingState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /Matchmaking.
        /// </summary>

        public MatchmakingHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/Matchmaking" };
            this.State = MatchmakingState.Start;

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
            if (this.State == MatchmakingState.Start && this.Player.Name == "")
            {
              this.State = MatchmakingState.Start;
              response = "Usted no ha iniciado sesión, porfavor use /LogIn.";
            }
            else if (this.State == MatchmakingState.Start && this.Player.Name != "")
            {
              this.State = MatchmakingState.AwaitingRivalNameForMatchmaking;
              response = "Introduce el nombre del usuario que quieres enfrentar";
            }
            else if (this.State == MatchmakingState.AwaitingRivalNameForMatchmaking)
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
                this.State = MatchmakingState.CheckingAnswerForMatchmaking;
                this.Player.InGame = true;
                response = "Se ha enviado la invitación al jugador, esperando su respuesta";
              }
              else
                  {
                    this.State = MatchmakingState.AwaitingRivalNameForMatchmaking;
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
            this.State = MatchmakingState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum MatchmakingState
        {

            ///-Start: Es el estadio inicial del comando. En este comando pide el mensaje de invitación para
            ///asi pasar al siguiente estado.
            Start,

            AwaitingRivalNameForMatchmaking,

            CheckingAnswerForMatchmaking
      
        }
    }
}