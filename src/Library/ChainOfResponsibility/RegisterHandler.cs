using System;
using System.Collections.Generic;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "Registrarse".
    /// </summary>
    public class RegisterHandler : BaseHandler
    {

        /// <summary>
        /// Los usuarios que usan este handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public RegisterState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /registrarse.
        /// </summary>

        public RegisterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/Register" };
            this.State = RegisterState.Start;

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
            string SentPassword = "";
            int intentos = 3;
            if (this.State == RegisterState.Start)
            {
              this.State = RegisterState.AwaitingInfoForRegister;
              response = "Como te gustaría llamar a tu usuario?";
            }
            else if (this.State == RegisterState.AwaitingInfoForRegister)
            {
              bool alreadyRegistered = false;
              foreach(User player in UsersList.GetInstance().Users)
              {
                  if (player.Name == message)
                  {
                    alreadyRegistered = true;
                  }
              }
              if (alreadyRegistered == true)
              {
                this.State = RegisterState.AwaitingInfoForRegister;
                response = "Este nombre de usuario ya existe, porfavor ingresa otro";                
              }
              else
              {
                SentName = message;
                this.State = RegisterState.AwaitingPasswordForRegister;
                response = "Ahora introduce tu contraseña";
              }
            }
            else if (this.State == RegisterState.AwaitingPasswordForRegister)
            {
              SentPassword = message;
              this.State = RegisterState.CheckingPasswordForRegister;
              response = "Ahora vuelve a introducir tu contraseña para confirmarla.";
            }
            else if (this.State == RegisterState.CheckingPasswordForRegister)
            {   
                if (message == SentPassword)
                {
                  UsersList newUsers = UsersList.GetInstance();
                  newUsers.AddUser(SentName, SentPassword);
                  response = "Felicidades, se ha registrado con éxito! Utilice /LogIn para iniciar sesión.";
                  intentos = 3;
                  this.State = RegisterState.Start;
                }
                else if (message != SentPassword && intentos < 0)
                {
                  intentos -= 1;
                  this.State = RegisterState.CheckingPasswordForRegister;
                  response = $"La contraseña no coincide, porfavor intentalo de nuevo, te quedan {intentos} intentos";
                }
                else
                {
                  this.State = RegisterState.AwaitingPasswordForRegister;
                  response = "Se han acabado los intentos, porfavor introduce una nueva contraseña.";
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
            this.State = RegisterState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum RegisterState
        {

            ///-Start: Es el estadio inicial del comando. En este comando pide el mensaje de invitación para
            ///asi pasar al siguiente estado.
            Start,

            AwaitingInfoForRegister,

            AwaitingPasswordForRegister,

            CheckingPasswordForRegister,
      
        }
    }
}