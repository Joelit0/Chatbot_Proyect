using System;
using System.Text;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/Help".
    /// </summary>
    public class HelpHandler : BaseHandler
    {

        /// <summary>
        /// Utilizamos esta propiedad para saber que usuario esta usando el handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }
        
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public HelpState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /Help.
        /// </summary>

        public HelpHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/Help" };
            this.State = HelpState.Start;
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
            if (this.State == HelpState.Start)
            {
              StringBuilder helpStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                            .Append("/Register: Registrate como un usuario nuevo\n")
                                                                            .Append("/Profile: Accede a tu perfil\n")
                                                                            .Append("/Matchmaking: Busca partida con un jugador que conoces\n");
              response = helpStringBuilder.ToString();
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
            this.State = HelpState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum HelpState
        {

            ///-Start: Es el estadio inicial del comando.
            Start
        }
    }
}