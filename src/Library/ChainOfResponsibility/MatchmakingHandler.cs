using System;
using System.Text;
using System.Collections.Generic;
using Telegram.Bot;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/Matchamking".
    /// </summary>
    public class MatchmakingHandler : BaseHandler
    {

        /// <summary>
        /// Utilizamos esta propiedad para saber que usuario esta usando el handler.
        /// </summary>
        /// <value></value>
        public User Player { get; private set; }

        /// <summary>
        /// Utilizamos esta propiedad para saber a que usuario vamos a invitar a la partida.
        /// </summary>
        /// <value></value>
        public User RivalPlayer { get; private set; }
        /// <summary>
        /// El estado del comando.
        /// </summary>
        
        public int GlobalMins = 0;

        public int RoundSecs = 0;

        public MatchmakingState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje /Matchmaking.
        /// </summary>

        public MatchmakingHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/Matchmaking" };
            this.State = MatchmakingState.Start;
        }

        protected override bool CanHandle(string message)
        {
            if (this.State == MatchmakingState.Start)
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
            if (this.State == MatchmakingState.Start && this.Player.Name == "")
            {
              this.State = MatchmakingState.Start;
              response = "Usted no ha iniciado sesión, por favor use /LogIn.";
            }
            else if (this.State == MatchmakingState.Start && this.Player.Name != "" && this.Player.InGame == true)
            {
              this.State = MatchmakingState.Start;
              response = "Usted ya tiene una partida creada, porfavor utiliza /Game para dirigirse a ella";
            }
            else if (this.State == MatchmakingState.Start && this.Player.Name != "")
            {
              this.State = MatchmakingState.AwaitingMatchmakingType;
              StringBuilder MatchmakingHelp = new StringBuilder("Selecciona que tipo de partida estas buscando:\n")
                                                                  .Append("/PvP: Una partida contra otro jugador\n")
                                                                  .Append("/PvE: Una partida contra un npc controlado por el bot\n");
              response = MatchmakingHelp.ToString();
            }
            else if (this.State == MatchmakingState.AwaitingMatchmakingType)
            {
              if (message == "/PvP")
              {
                this.State = MatchmakingState.AwaitingGlobalTimeConfig;
                response = "¿Quiere jugar una partida por tiempo?\nIntroduzca /Si o /No para confirmar o denegar.";
              }
              else if (message == "/PvE")
              {
                this.State = MatchmakingState.AwaitingIaForMatchmaking;
                response = "Prepárate para luchar contra la IA";
              }
              else
              {
                this.State = MatchmakingState.AwaitingMatchmakingType;
                response = "Comando inválido, por favor intentelo nuevamente utilizando /PvP o /PvE";
              }
            }
            else if (this.State == MatchmakingState.AwaitingGlobalTimeConfig)
            {
              if (message == "/Si")
              {
                this.State = MatchmakingState.AwaitingGlobalTime;
                StringBuilder MatchmakingGlobalTimeHelp = new StringBuilder("Selecciona cuanto tiempo le gustaría que dure su partida:\n")
                                                                  .Append("/10: Una partida de 10 min\n")
                                                                  .Append("/20: Una partida de 20 min\n")
                                                                  .Append("/20: Una partida de 20 min\n");
                response = MatchmakingGlobalTimeHelp.ToString();
              }
              else if (message == "/No")
              {
                this.State = MatchmakingState.AwaitingRoundTimeConfig;
                response = "¿Quiere añadir un límite de tiempo entre rondas?\nIntroduzca /Si o /No para confirmar o denegar.";
              }
              else
              {
                this.State = MatchmakingState.AwaitingGlobalTimeConfig;
                response = "Comando inválido, por favor inténtelo de nuevo";
              }
            }
            else if (this.State == MatchmakingState.AwaitingGlobalTime)
            {
              if ( message == "/10")
              {
                this.GlobalMins = 10;
                this.State = MatchmakingState.AwaitingRoundTimeConfig;
                response = "¿Quiere añadir un límite de tiempo entre rondas?\nIntroduzca /Si o /No para confirmar o denegar.";
              }
              else if ( message == "/20")
              {
                this.GlobalMins = 20;
                this.State = MatchmakingState.AwaitingRoundTimeConfig;
                response = "¿Quiere añadir un límite de tiempo entre rondas?\nIntroduzca /Si o /No para confirmar o denegar.";
              }
              else if ( message == "/20")
              {
                this.GlobalMins = 20;
                this.State = MatchmakingState.AwaitingRoundTimeConfig;
                response = "¿Quiere añadir un límite de tiempo entre rondas?\nIntroduzca /Si o /No para confirmar o denegar.";
              }
              else 
              {
                this.State = MatchmakingState.AwaitingGlobalTime;
                response = "Comando invalido, por favor intentelo de nuevo";
              }
            }
            else if (this.State == MatchmakingState.AwaitingRoundTimeConfig)
            {
              if (message == "/Si")
              {
                this.State = MatchmakingState.AwaitingRoundTime;
                StringBuilder MatchmakingRoundTimeHelp = new StringBuilder("Selecciona cuanto tiempo le gustaría que duren sus rondas:\n")
                                                                  .Append("/10: Una ronda de 10 segundos\n")
                                                                  .Append("/20: Una ronda de 20 segundos\n")
                                                                  .Append("/30: Una ronda de 30 segundos\n");
                response = MatchmakingRoundTimeHelp.ToString();
              }
              else if (message == "/No")
              {
                this.State = MatchmakingState.AwaitingRivalNameForMatchmaking;
                response = "Introduce el nombre del usuario que quieres enfrentar";
              }
              else
              {
                this.State = MatchmakingState.AwaitingRoundTimeConfig;
                response = "Comando inválido, por favor inténtelo de nuevo";
              }
            }
            else if (this.State == MatchmakingState.AwaitingRoundTime)
            {
              if ( message == "/10")
              {
                this.RoundSecs = 10;
                this.State = MatchmakingState.AwaitingRivalNameForMatchmaking;
                response = "Introduce el nombre del usuario que quieres enfrentar";
              }
              else if ( message == "/20")
              {
                this.RoundSecs = 20;
                this.State = MatchmakingState.AwaitingRivalNameForMatchmaking;
                response = "Introduce el nombre del usuario que quieres enfrentar";
              }
              else if ( message == "/30")
              {
                this.RoundSecs = 30;
                this.State = MatchmakingState.AwaitingRivalNameForMatchmaking;
                response = "Introduce el nombre del usuario que quieres enfrentar";
              }
              else 
              {
                this.State = MatchmakingState.AwaitingRoundTime;
                response = "Comando invalido, por favor intentelo de nuevo";
              }
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
                List<User> matchedUsers = new List<User>();
                matchedUsers.Add(Player);
                matchedUsers.Add(RivalPlayer);
                GamesList.GetInstance().AddGame(matchedUsers, this.GlobalMins, 0, 0, this.RoundSecs);
                this.GlobalMins = 0;
                this.RoundSecs = 0;
                this.Player.InGame = true;
                this.RivalPlayer.InGame = true;
                TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, $"¡Prepárate!¡{this.Player.Name} te ha desafiado a una partida! Utiliza /Game para dirgitrte a tu partida.");
                this.State = MatchmakingState.Start;
                response = "Se ha creado la partida, usa /Game para dirigirte a tu partida. ¡Buena suerte!";
              }
              else
                  {
                    this.State = MatchmakingState.AwaitingRivalNameForMatchmaking;
                    response = "Este usuario no existe o se encuentra en partida. por favor inténtalo de nuevo o prueba ponerte en contacto con dicho usuario";
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

            AwaitingMatchmakingType,

            AwaitingGlobalTimeConfig,

            AwaitingGlobalTime,

            AwaitingRoundTimeConfig,

            AwaitingRoundTime,

            AwaitingRivalNameForMatchmaking,

            AwaitingIaForMatchmaking
        }
    }
}