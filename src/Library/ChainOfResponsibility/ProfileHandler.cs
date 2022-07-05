
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/Profile".
    /// </summary>
    public class ProfileHandler : BaseHandler
    {

        /// <summary>
        /// Utilizamos esta propiedad para saber que usuario esta usando el handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public ProfileState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /Profile.
        /// </summary>

        public ProfileHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/Profile" };
            this.State = ProfileState.Start;
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
            if (this.State == ProfileState.Start && this.Player.Name == "")
            {
              this.State = ProfileState.Start;
              response = "Usted no se ha registrado, porfavor use /Register.";
            }
            else if (this.State == ProfileState.Start && this.Player.Name != "")
            {
              StringBuilder profileStringBuilder = new StringBuilder("Tu perfil de jugador:\n")
                                                                            .Append($"Nombre: {this.Player.Name}\n")
                                                                            .Append($"Id: {this.Player.ID}\n")
                                                                            .Append($"Puedes usar /ChangeInfo para cambiar tu nombre y contraseña.\n");
                                                                                                                                 
              response = profileStringBuilder.ToString();
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
            this.State = ProfileState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum ProfileState
        {

            ///-Start: Es el estado inicial del comando.
            Start,

        }
    }
}