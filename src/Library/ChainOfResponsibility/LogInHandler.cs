using System;
using System.Collections.Generic;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "Registrarse".
    /// </summary>
    public class LogInHandler : BaseHandler
    {

        /// <summary>
        /// Los usuarios que usan este handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }

        public User ExpectedPlayer { get; private set; }

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public LogInState State { get; private set; }

        string SentName = "";
        string ExpectedPassword = "";
        int intentos = 3;

        /// <summary>
        /// Esta clase procesa el mensaje /registrarse.
        /// </summary>

        public LogInHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/LogIn" };
            this.State = LogInState.Start;
        }

        protected override bool CanHandle(string message)
        {
            if (this.State == LogInState.Start)
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
        /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo se procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(string message, long chatid, out string response)
        {   
            if (this.State == LogInState.Start)
            {
              this.State = LogInState.AwaitingNameForLogIn;
              response = "Introduce tu nombre de usuario";
            }
            else if (this.State == LogInState.AwaitingNameForLogIn)
            {
              bool alreadyRegistered = false;
              foreach(User player in UsersList.GetInstance().Users)
              {
                  if (player.Name == message)
                  {
                    this.ExpectedPlayer = player;
                    this.ExpectedPassword = player.Password;
                    this.SentName = message;
                    alreadyRegistered = true;
                  }
              }
              if (alreadyRegistered == true)
              {
                this.State = LogInState.AwaitingPasswordForLogIn;
                response = $"Bienvenido {this.SentName}!, porfavor ingresa tu contraseña";                
              }
              else
              {
                this.State = LogInState.AwaitingNameForLogIn;
                response = "Este nombre no existe, inténtalo de nuevo o usa /register para registrarte";
              }
            }
            else if (this.State == LogInState.AwaitingPasswordForLogIn)
            {
              if (this.ExpectedPassword == message)
              {
                this.Player = this.ExpectedPlayer;
                this.intentos = 3;
                foreach(User player in UsersList.GetInstance().Users)
                {
                  if (player.GetID == chatid)
                  {
                    this.Player = player;
                  }
                }
                response = $"Bienvenido de vuelta {this.SentName}, nos alegra verte!";
                this.State = LogInState.Start;
              }
              else if (message != this.ExpectedPassword && this.intentos > 0)
              {
                this.intentos -= 1;
                this.State = LogInState.AwaitingPasswordForLogIn;
                response = $"La contraseña no coincide, porfavor intentalo de nuevo, te quedan {this.intentos} this.intentos";
              }
              else
              {
                this.State = LogInState.AwaitingNameForLogIn;
                response = $"Se han acabado los this.intentos, estas seguro de que eres {this.SentName}? Porfavor introduce tu nombre nuevamente.";
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
            this.State = LogInState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum LogInState
        {

            ///-Start: Es el estadio inicial del comando. En este comando pide el mensaje de invitación para
            ///asi pasar al siguiente estado.
            Start,

            AwaitingNameForLogIn,

            AwaitingPasswordForLogIn
        }
    }
}