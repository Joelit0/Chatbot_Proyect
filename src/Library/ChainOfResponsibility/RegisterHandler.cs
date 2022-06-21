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

        string SentName = "";
        string SentPassword = "";
        int intentos = 3;

        /// <summary>
        /// Esta clase procesa el mensaje /registrarse.
        /// </summary>

        public RegisterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/Register" };
            this.State = RegisterState.Start;

        }
        protected override bool CanHandle(string message)
        {
            if (this.State == RegisterState.Start)
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
              bool bannedName = false;
              foreach( string bannedKeyword in KeywordsList.GetInstance().BannedKeywords)
              {
                  if (bannedKeyword == message)
                  {
                    bannedName = true;
                  }
              }
              
              if (alreadyRegistered == true)
              {
                this.State = RegisterState.AwaitingInfoForRegister;
                response = "Este nombre de usuario ya existe, porfavor ingresa otro";                
              }
              else if (bannedName == true)
              {
                this.State = RegisterState.AwaitingInfoForRegister;
                response = "Este nombre de usuario no es válido"; 
              }
              else
              {
                this.SentName = message;
                Console.WriteLine($"El nombre enviado fue {this.SentName}");
                this.State = RegisterState.AwaitingPasswordForRegister;
                response = "Ahora introduce tu contraseña";
              }
            }
            else if (this.State == RegisterState.AwaitingPasswordForRegister)
            {
              this.intentos = 3;
              this.SentPassword = message;
              Console.WriteLine($"La contraseña enviada fue {this.SentPassword}");
              Console.WriteLine($"Te recuerdo que el nombre enviado fue {this.SentName}");
              this.State = RegisterState.CheckingPasswordForRegister;
              response = "Ahora vuelve a introducir tu contraseña para confirmarla.";
            }
            else if (this.State == RegisterState.CheckingPasswordForRegister)
            {   
                if (message == this.SentPassword)
                {
                  UsersList newUsers = UsersList.GetInstance();
                  newUsers.AddUser(this.SentName, this.SentPassword, chatid);
                  Console.WriteLine($"Su id es {chatid}");
                  response = "Felicidades, se ha registrado con éxito! Utilice /LogIn para iniciar sesión.";
                  this.intentos = 3;
                  this.State = RegisterState.Start;
                }
                else if (message != this.SentPassword && this.intentos > 0)
                {
                  this.intentos -= 1;
                  this.State = RegisterState.CheckingPasswordForRegister;
                  response = $"La contraseña no coincide, porfavor intentalo de nuevo, te quedan {this.intentos} intentos";
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