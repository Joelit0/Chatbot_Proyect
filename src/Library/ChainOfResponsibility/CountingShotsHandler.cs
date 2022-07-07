using System;
using System.Text;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/Help".
    /// </summary>
    public class CountingShotsHandler : BaseHandler
    {

        /// <summary>
        /// Utilizamos esta propiedad para saber que usuario esta usando el handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }
        
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public CountingShotsState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /Help.
        /// </summary>

        public CountingShotsHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/CountingShots" };
            this.State = CountingShotsState.Start;
        }
        protected override bool CanHandle(string message)
        {
            if (this.State ==CountingShotsState.Start)
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
            if (this.State == CountingShotsState.Start)
            {                                                                            
              StringBuilder CountingShotss1 = new StringBuilder("Selecciona que Shots quiere contar:\n")
                                                                  .Append("/ShotsOnShipsCount : para contar shots en barcos\n")
                                                                  .Append("/ShotsOnWaterCount : para contar shots en agua\n");
              this.State = CountingShotsState.CountingShots;
              response = CountingShotss1.ToString();
            }
            else if (this.State == CountingShotsState.CountingShots)
            {
              if (message == "/ShotsOnShipsCount") 
              {
                this.State = CountingShotsState.Start;
                response = "Cantidad de veces que disparaste un barco:";
              }
              else if (message == "/ShotsOnWaterCount") 
              {
                this.State = CountingShotsState.Start;
                response = "Cantidad de veces que disparaste agua:";
              }
              else
              {  
                response = "Comando inválido";
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
            this.State = CountingShotsState.Start;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum CountingShotsState
        {

            ///-Start: Es el estadio inicial del comando.
            Start,

            CountingShots,   
        }
    }
}