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

        /// <summary>
        /// Esta clase procesa el mensaje /registrarse.
        /// </summary>

        public LogInHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/LogIn" };
            this.State = LogInState.Start;
        }


        /// <summary>
        /// Procesa todos los mensajes y retorna true siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo se procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(string message, out string response)
        {   
            string SentName = "";
            string ExpectedPassword = "";
            int intentos = 3;
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
                    ExpectedPassword = player.Password;
                    SentName = message;
                    alreadyRegistered = true;
                  }
              }
              if (alreadyRegistered == true)
              {
                this.State = LogInState.AwaitingPasswordForLogIn;
                response = $"Bienvenido {SentName}!, porfavor ingresa tu contraseña";                
              }
              else
              {
                this.State = LogInState.AwaitingNameForLogIn;
                response = "Este nombre no existe, inténtalo de nuevo o usa /register para registrarte";
              }
            }
            else if (this.State == LogInState.AwaitingPasswordForLogIn)
            {
              if (ExpectedPassword == message)
              {
                this.Player = this.ExpectedPlayer;
                intentos = 3;
                UserLogin.GetInstance().setLogedUser(this.Player);
                response = $"Bienvenido de vuelta {SentName}, nos alegra verte!";
                this.State = LogInState.Start;
              }
              else if (message != ExpectedPassword && intentos < 0)
              {
                intentos -= 1;
                this.State = LogInState.AwaitingPasswordForLogIn;
                response = $"La contraseña no coincide, porfavor intentalo de nuevo, te quedan {intentos} intentos";
              }
              else
              {
                this.State = LogInState.AwaitingNameForLogIn;
                response = $"Se han acabado los intentos, estas seguro de que eres {SentName}? Porfavor introduce tu nombre nuevamente.";
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