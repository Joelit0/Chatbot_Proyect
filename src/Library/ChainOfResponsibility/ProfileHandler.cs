using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "Registrarse".
    /// </summary>
    public class ProfileHandler : BaseHandler
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
        public ProfileState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /registrarse.
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
        /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo se procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(string message, int id, out string response)
        {   
            this.Player = UserLogin.GetInstance().LogedInPlayer;
            if (this.State == ProfileState.Start && this.Player.Name == "")
            {
              this.State = ProfileState.Start;
              response = "Usted no ha iniciado sesión, porfavor use /LogIn.";
            }
            else if (this.State == ProfileState.Start && this.Player.Name != "")
            {
              StringBuilder profileStringBuilder = new StringBuilder("Tu perfil de jugador:\n")
                                                                            .Append($"Nombre: {this.Player.Name}\n")
                                                                            .Append($"Id: {this.Player.GetID}\n");                                                       
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

            ///-Start: Es el estadio inicial del comando. En este comando pide el mensaje de invitación para
            ///asi pasar al siguiente estado.
            Start,

        }
    }
}