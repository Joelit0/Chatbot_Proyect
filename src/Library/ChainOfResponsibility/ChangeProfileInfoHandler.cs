using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/ChangeInfo".
    /// </summary>
    public class ChangeProfileInfoHandler : BaseHandler
    {

        /// <summary>
        /// Utilizamos esta propiedad para saber que usuario esta usando el handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public ChangeProfileInfoState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /ChangeInfo.
        /// </summary>

        public ChangeProfileInfoHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/ChangeInfo" };
            this.State = ChangeProfileInfoState.Start;
        }

        protected override bool CanHandle(string message)
        {
            if (this.State == ChangeProfileInfoState.Start)
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
        /// <param name="chatid">La id del chat del usuario, la utilizamos para poder indicar que usuario es el que esta usando el bot</param>
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
            if (this.State == ChangeProfileInfoState.Start && this.Player.Name == "")
            {
              this.State = ChangeProfileInfoState.Start;
              response = "Usted no ha iniciado sesión, porfavor use /LogIn.";
            }
            else if (this.State == ChangeProfileInfoState.Start && this.Player.Name != "")
            {
              this.State = ChangeProfileInfoState.AwaitingWhatToChange;
              response = "Porfavor use /Name si quiere cambiar su nombre y /Password si quiere cambiar su contraseña";
            }
            else if (this.State == ChangeProfileInfoState.AwaitingWhatToChange)
            {
              if (message == "/Name")
              {
                this.State = ChangeProfileInfoState.ChangeName;
                response = "Introduzca su nuevo nombre";
              }
              else if (message == "/Password")
              {
                this.State = ChangeProfileInfoState.ChangePassword;
                response = "Introduzca su nueva contraseña";
              }
              else
              {
                this.State = ChangeProfileInfoState.AwaitingWhatToChange;
                response = "Comando Invalido! Porfavor use /Name si quiere cambiar su nombre o /Password si quiere cambiar su contraseña";
              }
            }
            else if (this.State == ChangeProfileInfoState.ChangeName)
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
                this.State = ChangeProfileInfoState.ChangeName;
                response = "Este nombre de usuario ya existe, porfavor ingresa otro";                
              }
              else if (bannedName == true)
              {
                this.State = ChangeProfileInfoState.ChangeName;
                response = "Este nombre de usuario no es válido"; 
              }
              else
              {
                this.Player.Name = message;
                this.State = ChangeProfileInfoState.Start;
                response = $"Su nombre de usuario se ha cambiado a {this.Player.Name}";
              }
            }
            else if (this.State == ChangeProfileInfoState.ChangePassword)
            {
              this.Player.Password = message;
              response = $"Su contraseña se ha cambiado a {this.Player.Password}";
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
            this.State = ChangeProfileInfoState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum ChangeProfileInfoState
        {

            ///-Start: Es el estadio inicial del comando.
            Start,

            ///-AwaitingWhatToChange: En este estado el mensaje esperado
            /// es un /Name o /Password, para saber a que estado continuar.
            AwaitingWhatToChange,
            
            ///-ChangeName: En este estado el mensaje esperado es el nuevo nombre que
            ///el usuario quiere utilizar. 
            ChangeName,

            ///-ChangePassword: En este estado el mensaje esperado es la nueva contraseña que
            ///el usuario quiere utilizar.
            ChangePassword,
        }
    }
}