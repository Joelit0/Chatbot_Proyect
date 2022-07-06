using System;
using System.Text;
using System.Collections.Generic;
using Telegram.Bot;

namespace ChatBotProject
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/Game".
    /// </summary>
    public class GameHandler : BaseHandler
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
        public GameState State { get; private set; }

        /// <summary>
        /// El game actual.
        /// </summary>
        public Game CurrentGame { get; private set; }

        private List<String> stringArrayToList(string[] stringArray)
        {
          List<String> stringList = new List<String>();

          foreach(string element in stringArray)
          {
            stringList.Add(element);
          }

          return stringList;
        }
  
        /// <summary>
        /// Esta clase procesa el mensaje /Game.
        /// </summary>
        public GameHandler(BaseHandler next) : base(next)
        {
          this.Keywords = new string[] { "/Game" };
          this.State = GameState.PlayerStart;
          
        }

        protected override bool CanHandle(string message)
        {
          if (this.State == GameState.PlayerStart)
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
            //Utilizamos las siguientes variables así como el foreach para identificar a los usuarios 
            //que interactuan con el bot y poder guiarlos por el flujo adecuado.
            bool RealPlayer = false;
            bool RealRivalPlayer = false;
            foreach(Game game in GamesList.GetInstance().Games)
            {
              if (game.getInMatchUsers()[0].ID == chatid)
              {
                this.Player = game.getInMatchUsers()[0];
                this.RivalPlayer = game.getInMatchUsers()[1];
                this.CurrentGame = game;
              }
              else if (game.getInMatchUsers()[1].ID == chatid)
              {
                this.RivalPlayer = game.getInMatchUsers()[1];
                this.Player = game.getInMatchUsers()[0];
                this.CurrentGame = game;
              }
              else
              {
                TelegramBot.GetInstance().botClient.SendTextMessageAsync(chatid, "No tiene ninguna partida creada, utilice /Matchmaking para crear una.");
              }
            }
            //Flujo del player, el jugador que crea la partida siempre será identificado
            //como player.
            if (this.Player.ID == chatid)
            {
              Console.WriteLine("Entro al flujo de Player");
              RealPlayer = true;
              //Estados de Usuario, los usamos para guardar la ubicación de un usuario dentro del 
              //handler.
              if (RealPlayer == true)
              {
                // Este estado de usuario está asociado al estado PlayerStart del handler.
                if (this.Player.State == "PlayerStart")
                {
                  this.State = GameState.PlayerStart;
                  response = "PlayerStart";
                }
                // Este estado de usuario está asociado al estado PlayerReadyToStartConfirmation del handler.
                else if (this.Player.State == "PlayerReadyToStartConfirmation")
                {
                  this.State = GameState.PlayerReadyToStartConfirmation;
                  response = "Entro al estado de usuario PlayerReadyToStartConfirmation";
                }
                // Este estado de usuario está asociado al estado PlayerPlacingShip1 del handler.
                else if (this.Player.State == "PlayerPlacingShip1")
                {
                  this.State = GameState.PlayerPlacingShip1;
                  response = "Entro al estado de usuario PlayerPlacingShip1";
                }
                // Este estado de usuario está asociado al estado PlayerPlacingShip2 del handler.
                else if (this.Player.State == "PlayerPlacingShip2")
                {
                  this.State = GameState.PlayerPlacingShip2;
                  response = "Entro al estado de usuario PlayerPlacingShip2";
                }
                // Este estado de usuario está asociado al estado PlayerPlacingShip3 del handler.
                else if (this.Player.State == "PlayerPlacingShip3")
                {
                  this.State = GameState.PlayerPlacingShip3;
                  response = "Entro al estado de usuario PlayerPlacingShip3";
                }
                // Este estado de usuario está asociado al estado PlayerPlacingShip4 del handler.
                else if (this.Player.State == "PlayerPlacingShip4")
                {
                  this.State = GameState.PlayerPlacingShip4;
                  response = "Entro al estado de usuario PlayerPlacingShip4";
                }
                // Este estado de usuario está asociado al estado PlayerBeginConfirmation del handler.
                else if (this.Player.State == "PlayerBeginConfirmation")
                {
                  this.State = GameState.PlayerBeginConfirmation;
                  response = "Entro al estado de usuario PlayerBeginConfirmation";
                }
                // Este estado de usuario está asociado al estado PlayerReadyToStart del handler.
                else if (this.Player.State == "PlayerReadyToStart")
                {
                  this.State = GameState.PlayerReadyToStart;
                  response = "Entro al estado de usuario PlayerReadyToStart";
                }
                // Este estado de usuario está asociado al estado PlayerPvpBattleship del handler.
                else if (this.Player.State == "PlayerPvpBattleship")
                {
                  this.State = GameState.PlayerPvpBattleship;
                  response = "Entro al estado de usuario PlayerPvpBattleship";
                }
              }
              //Estados del Handler
    
              if (this.State == GameState.PlayerStart && this.Player.Name != "" && this.RivalPlayer.Name != "")
              {
                //En el estado PlayerStart se espera que el mensaje recibido contenga /Matchmaking.
                this.State = GameState.PlayerReadyToStartConfirmation;
                this.Player.State = "PlayerReadyToStartConfirmation";
                StringBuilder GameLobbyHelpStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                              .Append("/Ready: Listo para iniciar la partida\n")
                                                                              .Append("/Leave: Salir de la partida, esta acción eliminara la partida actual.\n");
                response = GameLobbyHelpStringBuilder.ToString();
              }
              else if (this.State == GameState.PlayerReadyToStartConfirmation)
              {
                //En el estado PlayerStart se espera que el mensaje recibido contenga /Ready o /Leave
                //para poder continuar progresando a lo largo del handler.
                if (message == "/Ready")
                {
                  //En caso de que el mensaje recibido sea /Ready, continuamos nuestra progresión a travez del handler.
                  this.State = GameState.PlayerPlacingShip1;
                  this.Player.State = "PlayerPlacingShip1";
                  this.CurrentGame.PrintPlayerBoardToSelf();
                  response = "Ahora deberá ingresar el primer barco de 2 posiciones. Los barcos se ingresan de la siguiente manera **A1,A2,A3,A4** dependiendo del tamaño y posicion del barco";
                }
                else if (message == "/Leave")
                {
                  //En caso de que el mensaje recibido sea /Leave, entendemos que el usuario quiere retirarse de la partida
                  //por lo que acabamos con la progresión del handler y eliminamos la partida..
                  Player.InGame = false;
                  Player.ReadyToStartMatch = false;
                  RivalPlayer.InGame = false;
                  RivalPlayer.ReadyToStartMatch = false;
                  GamesList.GetInstance().RemoveGame(this.CurrentGame);
                  response = "La partida ha sido cancelada porque uno de los jugadores se ha salido.";
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  this.State = GameState.PlayerStart;
                  this.Player.State = "PlayerStart";
                }
                else
                {
                  //En caso de que el usuario ingrese un comando inválido, se lo enviará al comienzo de esta etapa,
                  //por lo que tendrá que decidir si enviar /Ready o /Leave nuevamente.
                  this.State = GameState.PlayerReadyToStartConfirmation;
                  this.Player.State = "PlayerReadyToStartConfirmation";
                  response = "Comando inválido, por favor intentelo nuevamente utilizando /Ready o /Leave";
                }
              }
              else if (this.State == GameState.PlayerPlacingShip1)
              {
                //En el estado PlayerPlacingShip1 se espera que el mensaje recibido contenga las 
                //coordenadas de una nave de 2 posiciones.
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.PlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintPlayerBoardToSelf();
                response = "Ahora deberá ingresar el segundo barco de 3 posiciones.";
                this.State = GameState.PlayerPlacingShip2;
                this.Player.State = "PlayerPlacingShip2";  
              }
              else if (this.State == GameState.PlayerPlacingShip2)
              {
                //En el estado PlayerPlacingShip2 se espera que el mensaje recibido contenga las 
                //coordenadas de una nave de 3 posiciones.
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.PlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintPlayerBoardToSelf();
                response = "Ahora deberá ingresar el tercer barco de 4 posiciones.";
                this.State = GameState.PlayerPlacingShip3;
                this.Player.State = "PlayerPlacingShip3";
              }
              else if (this.State == GameState.PlayerPlacingShip3)
              {
                //En el estado PlayerPlacingShip3 se espera que el mensaje recibido contenga las 
                //coordenadas de una nave de 4 posiciones.
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.PlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintPlayerBoardToSelf();
                response = "Ahora deberá ingresar el cuarto barco de 5 posiciones.";
                this.State = GameState.PlayerPlacingShip4;
                this.Player.State = "PlayerPlacingShip4";
              }
              else if (this.State == GameState.PlayerPlacingShip4)
              {
                //En el estado PlayerPlacingShip4 se espera que el mensaje recibido contenga las 
                //coordenadas de una nave de 5 posiciones.
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.PlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintPlayerBoardToSelf();
                StringBuilder GameLobbyHelpStringBuilder = new StringBuilder("¡Buen Trabajo! Ahora puedes usar:\n")
                                                                              .Append("/Begin para confirmar que estas listo para comenzar\n")
                                                                              .Append("/Leave: Salir de la partida, esta acción eliminara la partida actual.\n");
                response = GameLobbyHelpStringBuilder.ToString();
                this.State = GameState.PlayerBeginConfirmation;
                this.Player.State = "PlayerBeginConfirmation";
              }
              else if (this.State == GameState.PlayerBeginConfirmation)
              {
                //En el estado PlayerBeginConfirmation se espera que el mensaje recibido sea
                // /Begin o /Leave.
                if (message == "/Begin")
                {
                  //Si el mensaje es begin continua hacia el siguiente estado, confirmado que el jugador esta listo para comenzar
                  this.Player.SetReadyToStartMatch(true);
                  this.Player.State = "PlayerReadyToStart";
                  this.State = GameState.PlayerReadyToStart;
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, $"{this.Player.Name} ha colocado todas sus naves y esta listo para comenzar.");
                  response = "Has confirmado que estas listo para comenzar,presiona cualquier tecla para continuar";
                }
                else if (message == "/Leave")
                {
                  //Si el mensaje es /Leave elimina la instancia del juego y resetea el estado del handler al estado inicial
                  Player.InGame = false;
                  Player.ReadyToStartMatch = false;
                  RivalPlayer.InGame = false;
                  RivalPlayer.ReadyToStartMatch = false;
                  GamesList.GetInstance().RemoveGame(this.CurrentGame);
                  response = "La partida ha sido cancelada porque uno de los jugadores se ha salido.";
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  this.State = GameState.PlayerStart;
                  this.Player.State = "";
                  this.Player = null;
                  this.RivalPlayer = null;
                }
                else
                {
                  //En caso de que el mensaje no sea ni /Begin ni /Leave, el handler vuelve al estado PlayerBeginConfirmation
                  // y espera a recibir /Begin o /Leave.
                  this.State = GameState.PlayerBeginConfirmation;
                  this.Player.State = "PlayerBeginConfirmation";
                  response = "Comando inválido, por favor utiliza /Begin para confirmar que estas listo para comenzar";
                }
              }
              else if (this.State == GameState.PlayerReadyToStart)
              {
                //En este estado el mensaje esperado puede ser cualquier letra, la progresion depende si ambos players estan listos para empezar.
                if (this.Player.ReadyToStartMatch == true && this.RivalPlayer.ReadyToStartMatch == true)
                {
                  this.State = GameState.PlayerPvpBattleship;
                  this.Player.State = "PlayerPvpBattleship";
                  this.Player.MyTurn = true;
                  response = "Es tu turno, porfavor ingresa una coordenada de ataque, por ejemplo C1. ¡Que comience la batalla naval! Recuerda que puedes usar /Leave para salir de la partida";
                }
                else if (this.Player.ReadyToStartMatch == true && this.RivalPlayer.ReadyToStartMatch == false)
                {
                  this.State = GameState.PlayerReadyToStart;
                  this.Player.State = "PlayerReadyToStart";
                  response = $"{this.RivalPlayer.Name} no ha confirmado que esta listo, porfavor espera a recibir el mensaje de confirmación.";
                }
                else
                {
                  this.State = GameState.PlayerReadyToStart;
                  this.Player.State = "PlayerReadyToStart";
                  response = string.Empty;
                }
              }
              else if (this.State == GameState.PlayerPvpBattleship)
              {
                response = string.Empty;
                if (this.CurrentGame.PlayerBoardHasShips() && this.CurrentGame.RivalPlayerBoardHasShips())
                {
                  //En este estado el mensaje esperado es una coordenada a la cual atacar, el ataque de un player 
                  //se dara si y solo si la propiedad MyTurn de dicho player esta en true. 
                  if (this.Player.MyTurn == true)
                  {
                    //Realizo mi ataque, imprimo los boards correspondientes y cambio mi MyTurn a false y el MyTurn de mi rival a true.
                    this.CurrentGame.AttackRivalPlayerBoard(message);

                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, "Tu tablero");
                    this.CurrentGame.PrintPlayerBoardToSelf();

                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, $"Tablero de {this.RivalPlayer.Name}");
                    this.CurrentGame.PrintRivalPlayerBoardToEnemy();

                    this.Player.MyTurn = false;
                    this.RivalPlayer.MyTurn = true;
                    this.Player.State = "PlayerPvpBattleship";
                    this.State = GameState.PlayerPvpBattleship;
                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.RivalPlayer.ID, $"{this.Player.Name} ha atacado y terminado su turno");
                    response = "Has atacado, ahora espera a que sea tu turno para poder atacar nuevamente. Recuerda que puedes usar /Leave para salir de la partida";
                  }
                  else if (this.Player.MyTurn == false)
                  {
                    this.State = GameState.PlayerPvpBattleship;
                    this.Player.State = "PlayerPvpBattleship";
                    response = "Aún no es tu turno de atacar, porfavor espera a que sea tu turno para poder atacar nuevamente";
                  }
                  else
                  {
                    response = string.Empty;
                  }
                }
                if(!this.CurrentGame.PlayerBoardHasShips() || !this.CurrentGame.RivalPlayerBoardHasShips()) //Revisa si uno de los tableros ya no tiene barcos.
                {
                  this.Player.InGame = false;
                  this.Player.SetReadyToStartMatch(false);
                  this.RivalPlayer.InGame = false;
                  this.RivalPlayer.SetReadyToStartMatch(false);
                  this.CurrentGame.FinishGame();
                  this.Player.State = "PlayerStart";
                  this.State = GameState.PlayerStart;
                  
                  if (!this.CurrentGame.RivalPlayerBoardHasShips())
                  {
                    response = $"Felicidades {this.Player.Name}, ganaste!";
                    this.CurrentGame.setWinner(this.Player.Name);
                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.RivalPlayer.ID, $"Lo siento {this.RivalPlayer.Name}, pero has perdido");
                    this.State = GameState.PlayerStart;
                  } 
                  else
                  {
                    response = $"Lo siento {this.Player.Name}, pero has perdido";
                    this.CurrentGame.setWinner(this.RivalPlayer.Name);
                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.RivalPlayer.ID, $"Felicidades {this.RivalPlayer.Name}, ganaste!");
                    this.State = GameState.PlayerStart;
                  }
                }
                else if (message == "/Leave")
                {
                  Player.InGame = false;
                  Player.ReadyToStartMatch = false;
                  RivalPlayer.InGame = false;
                  RivalPlayer.ReadyToStartMatch = false;
                  GamesList.GetInstance().RemoveGame(this.CurrentGame);
                  response = "La partida ha sido cancelada porque uno de los jugadores se ha salido.";
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  this.State = GameState.PlayerStart;
                  this.Player.State = "";
                  this.RivalPlayer.State = "";
                  this.Player = null;
                  this.RivalPlayer = null;
                  this.CurrentGame = null;
                }
              }
              else
              {
                response = string.Empty;
              }
            }
            //Flujo del Rival
            else if (this.RivalPlayer.ID == chatid)
            {
              Console.WriteLine("Entro al flujo de Rival");
              RealRivalPlayer = true;
              //Estados de Usuario
              if (RealRivalPlayer == true)
              {
                if (this.RivalPlayer.State == "RivalPlayerStart")
                {
                  this.State = GameState.RivalPlayerStart;
                  response = "Inicio del rival";
                }
                else if (this.RivalPlayer.State == "RivalPlayerReadyToStartConfirmation")
                {
                  this.State = GameState.RivalPlayerReadyToStartConfirmation;
                  response = "Entro al estado de usuario RivalPlayerReadyToStartConfirmation";
                }
                else if (this.RivalPlayer.State == "RivalPlayerPlacingShip1")
                {
                  this.State = GameState.RivalPlayerPlacingShip1;
                  response = "Entro al estado de usuario RivalPlayerPlacingShip1";
                }
                else if (this.RivalPlayer.State == "RivalPlayerPlacingShip2")
                {
                  this.State = GameState.RivalPlayerPlacingShip2;
                  response = "Entro al estado de usuario RivalPlayerPlacingShip2";
                }
                else if (this.RivalPlayer.State == "RivalPlayerPlacingShip3")
                {
                  this.State = GameState.RivalPlayerPlacingShip3;
                  response = "Entro al estado de usuario RivalPlayerPlacingShip3";
                }
                else if (this.RivalPlayer.State == "RivalPlayerPlacingShip4")
                {
                  this.State = GameState.RivalPlayerPlacingShip4;
                  response = "Entro al estado de usuario RivalPlayerPlacingShip4";
                }
                else if (this.RivalPlayer.State == "RivalPlayerBeginConfirmation")
                {
                  this.State = GameState.RivalPlayerBeginConfirmation;
                  response = "Entro al estado de usuario RivalPlayerBeginConfirmation";
                }
                else if (this.RivalPlayer.State == "RivalPlayerReadyToStart")
                {
                  this.State = GameState.RivalPlayerReadyToStart;
                  response = "Entro al estado de usuario RivalPlayerReadyToStart";
                }
                else if (this.RivalPlayer.State == "RivalPlayerPvpBattleship")
                {
                  this.State = GameState.RivalPlayerPvpBattleship;
                  response = "Entro al estado de usuario RivalPlayerPvpBattleship";
                }
              }
              //Estados del Handler
              if (this.State == GameState.RivalPlayerStart && message == "/Game" && this.RivalPlayer.Name != "" && this.RivalPlayer.Name != "")
              {
                this.RivalPlayer.State = "RivalPlayerReadyToStartConfirmation";
                this.State = GameState.RivalPlayerReadyToStartConfirmation;
                StringBuilder GameLobbyHelpStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                              .Append("/Ready: Listo para iniciar la partida\n")
                                                                              .Append("/Leave: Salir de la partida, esta acción eliminara la partida actual.\n");
                response = GameLobbyHelpStringBuilder.ToString();
              }
              else if (this.State == GameState.RivalPlayerReadyToStartConfirmation)
              {
                if (message == "/Ready")
                {
                  this.RivalPlayer.State = "RivalPlayerPlacingShip1";
                  this.State = GameState.RivalPlayerPlacingShip1;
                  this.CurrentGame.PrintRivalPlayerBoardToSelf();
                  response = "Ahora deberá ingresar el primer barco de 2 posiciones. Los barcos se ingresan de la siguiente manera **A1,A2,A3,A4** dependiendo del tamaño y posicion del barco";
                }
                else if (message == "/Leave")
                {
                  RivalPlayer.InGame = false;
                  RivalPlayer.ReadyToStartMatch = false;
                  Player.InGame = false;
                  Player.ReadyToStartMatch = false;
                  GamesList.GetInstance().RemoveGame(this.CurrentGame);
                  response = "La partida ha sido cancelada porque uno de los jugadores se ha salido.";
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  this.Player.State = "";
                  this.RivalPlayer.State = "";
                  this.Player = null;
                  this.RivalPlayer = null;
                  this.CurrentGame = null;
                  this.State = GameState.RivalPlayerStart;
                  
                }
                else
                {
                  this.RivalPlayer.State = "PlayerReadyToStartConfirmation";
                  this.State = GameState.PlayerReadyToStartConfirmation;
                  response = "Comando inválido, por favor intentelo nuevamente utilizando /Ready o /Leave";
                }
              }
              else if (this.State == GameState.RivalPlayerPlacingShip1)
              {
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.RivalPlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintRivalPlayerBoardToSelf();
                response = "Ahora deberá ingresar el segundo barco de 3 posiciones.";
                this.RivalPlayer.State = "RivalPlayerPlacingShip2"; 
                this.State = GameState.RivalPlayerPlacingShip2;
              }
              else if (this.State == GameState.RivalPlayerPlacingShip2)
              {
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.RivalPlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintRivalPlayerBoardToSelf();
                response = "Ahora deberá ingresar el tercer barco de 4 posiciones.";
                this.RivalPlayer.State = "RivalPlayerPlacingShip3"; 
                this.State = GameState.RivalPlayerPlacingShip3;
                
              }
              else if (this.State == GameState.RivalPlayerPlacingShip3)
              {
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.RivalPlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintRivalPlayerBoardToSelf();
                response = "Ahora deberá ingresar el cuarto barco de 5 posiciones.";
                this.RivalPlayer.State = "RivalPlayerPlacingShip4";
                this.State = GameState.RivalPlayerPlacingShip4;
                 
              }
              else if (this.State == GameState.RivalPlayerPlacingShip4)
              {
                string[] shipPositions = message.ToUpper().Split(',');
                this.CurrentGame.RivalPlayerAddShipToBoard(stringArrayToList(shipPositions));
                this.CurrentGame.PrintRivalPlayerBoardToSelf();
                StringBuilder GameLobbyHelpStringBuilder = new StringBuilder("¡Buen Trabajo! Ahora puedes usar:\n")
                                                                              .Append("/Begin para confirmar que estas listo para comenzar\n")
                                                                              .Append("/Leave: Salir de la partida, esta acción eliminara la partida actual.\n");
                response = GameLobbyHelpStringBuilder.ToString();
                this.State = GameState.RivalPlayerBeginConfirmation;
                this.RivalPlayer.State = "RivalPlayerBeginConfirmation"; 
              }
              else if (this.State == GameState.RivalPlayerBeginConfirmation)
              {
                if (message == "/Begin")
                {
                  Console.WriteLine("Llegamos hasta aca");
                  this.RivalPlayer.SetReadyToStartMatch(true);
                  this.RivalPlayer.State = "RivalPlayerReadyToStart"; 
                  this.State = GameState.RivalPlayerReadyToStart;
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, $"{this.RivalPlayer.Name} ha colocado todas sus naves y esta listo para comenzar.");
                  response = "Has confirmado que estas listo para comenzar,presiona cualquier tecla para continuar";
                }
                else if (message == "/Leave")
                {
                  RivalPlayer.InGame = false;
                  RivalPlayer.ReadyToStartMatch = false;
                  Player.InGame = false;
                  Player.ReadyToStartMatch = false;
                  GamesList.GetInstance().RemoveGame(this.CurrentGame);
                  response = "La partida ha sido cancelada porque uno de los jugadores se ha salido.";
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  this.State = GameState.RivalPlayerStart;
                  this.Player.State = "";
                  this.RivalPlayer.State = "";
                  this.Player = null;
                  this.RivalPlayer = null;
                  this.CurrentGame = null;
                  
                }
                else
                {
                  this.State = GameState.RivalPlayerBeginConfirmation;
                  this.RivalPlayer.State = "RivalPlayerBeginConfirmation";
                  response = "Comando inválido, por favor utiliza /Begin para confirmar que estas listo para comenzar";
                }
              }
              else if (this.State == GameState.RivalPlayerReadyToStart)
              {
                Console.WriteLine("Y hasta aca también llegamos");
                if (this.RivalPlayer.ReadyToStartMatch == true && this.Player.ReadyToStartMatch == true)
                {
                  this.RivalPlayer.State = "RivalPlayerPvpBattleship";
                  this.RivalPlayer.MyTurn = false;
                  this.State = GameState.RivalPlayerPvpBattleship;
                  response = "Porfavor espera tu turno para ingresar una coordenada de ataque, por ejemplo C1. ¡Que comience la batalla naval! Recuerda que puedes utilizar /Leave para poder salir de la partida";
                }
                else if (this.RivalPlayer.ReadyToStartMatch == true && this.Player.ReadyToStartMatch == false)
                {
                  this.RivalPlayer.State = "RivalPlayerReadyToStart";
                  this.State = GameState.RivalPlayerReadyToStart;
                  response = $"{this.Player.Name} no ha confirmado que esta listo, porfavor espera a recibir el mensaje de confirmación.";
                }
                else
                {
                  this.RivalPlayer.State = "RivalPlayerReadyToStart";
                  this.State = GameState.RivalPlayerReadyToStart;
                  response = "Unexpected error, no sé que pasó";
                }
              }
              else if (this.State == GameState.RivalPlayerPvpBattleship)
              {
                response = string.Empty;
                if (this.CurrentGame.RivalPlayerBoardHasShips() && this.CurrentGame.PlayerBoardHasShips())
                {
                  if (this.RivalPlayer.MyTurn == true)
                  {
                    this.CurrentGame.AttackPlayerBoard(message);

                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.RivalPlayer.ID, "Tu tablero");
                    this.CurrentGame.PrintRivalPlayerBoardToSelf();

                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.RivalPlayer.ID, $"Tablero de {this.Player.Name}");
                    this.CurrentGame.PrintPlayerBoardToEnemy();

                    this.RivalPlayer.MyTurn = false;
                    this.Player.MyTurn = true;
                    this.RivalPlayer.State = "RivalPlayerPvpBattleship";
                    this.State = GameState.RivalPlayerPvpBattleship;
                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, $"{this.RivalPlayer.Name} ha atacado y terminado su turno.");
                    response = "Has atacado, ahora espera a que sea tu turno para poder atacar nuevamente. Recuerda que puedes utilizar /Leave para poder salir de la partida";
                  }
                  else if (this.RivalPlayer.MyTurn == false)
                  {
                    this.RivalPlayer.State = "RivalPlayerPvpBattleship";
                    this.State = GameState.RivalPlayerPvpBattleship;
                    response = "Aún no es tu turno de atacar, porfavor espera a que sea tu turno para poder atacar nuevamente";
                  }
                  else
                  {
                    this.RivalPlayer.State = "RivalPlayerPvpBattleship";
                    this.State = GameState.RivalPlayerPvpBattleship;
                    response = "No se que paso";
                  }
                }

                if(!this.CurrentGame.PlayerBoardHasShips() || !this.CurrentGame.RivalPlayerBoardHasShips()) //Revisa si uno de los tableros ya no tiene barcos.
                {
                  this.Player.InGame = false;
                  this.Player.SetReadyToStartMatch(false);
                  this.RivalPlayer.InGame = false;
                  this.RivalPlayer.SetReadyToStartMatch(false);
                  this.CurrentGame.FinishGame();
                  this.RivalPlayer.State = "RivalPlayerStart";
                  this.State = GameState.RivalPlayerStart;
                  
                  if (!this.CurrentGame.PlayerBoardHasShips())
                  {
                    response = $"Felicidades {this.RivalPlayer.Name}, ganaste!";
                    this.CurrentGame.setWinner(this.RivalPlayer.Name);
                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, $"Lo siento {this.Player.Name}, pero has perdido");
                    this.State = GameState.PlayerStart;
                  } 
                  else
                  {
                    response = $"Lo siento {this.RivalPlayer.Name}, pero has perdido";
                    TelegramBot.GetInstance().botClient.SendTextMessageAsync(this.Player.ID, $"Felicidades {this.Player.Name}, ganaste!");
                    this.CurrentGame.setWinner(this.Player.Name);
                    this.State = GameState.PlayerStart;
                  }
                }
                else if (message == "/Leave")
                {
                  Player.InGame = false;
                  Player.ReadyToStartMatch = false;
                  RivalPlayer.InGame = false;
                  RivalPlayer.ReadyToStartMatch = false;
                  GamesList.GetInstance().RemoveGame(this.CurrentGame);
                  response = "La partida ha sido cancelada porque uno de los jugadores se ha salido.";
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(Player.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  TelegramBot.GetInstance().botClient.SendTextMessageAsync(RivalPlayer.ID, "La partida ha sido cancelada porque uno de los jugadores se ha salido.");
                  this.State = GameState.PlayerStart;
                  this.Player.State = "";
                  this.RivalPlayer.State = "";
                  this.Player = null;
                  this.RivalPlayer = null;
                  this.CurrentGame = null;
                }
              }
              else
              {
                response = string.Empty;
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
          this.State = GameState.PlayerStart;
        }
        
        /// <summary>
        /// Estados por los que pasara el handler para asi saber que mensaje esperar y que respuesta dar.
        /// </summary>
        public enum GameState
        {
          ///-PlayerStart: Es el estado inicial del comando. En este comando pide el mensaje de /Game para
          ///asi pasar al siguiente estado.
          PlayerStart,

          ///-PlayerReadyToStartConfirmation. En este comando espera /Ready o /Leave.
          PlayerReadyToStartConfirmation,

          ///-PlayerPlacingShip1. En este comando espera una coordenada para colocar un barco de 2 posiciones.
          PlayerPlacingShip1,

          ///-PlayerPlacingShip2. En este comando espera una coordenada para colocar un barco de 3 posiciones.
          PlayerPlacingShip2,

          ///-PlayerPlacingShip3. En este comando espera una coordenada para colocar un barco de 4 posiciones.
          PlayerPlacingShip3,

          ///-PlayerPlacingShip4. En este comando espera una coordenada para colocar un barco de 5 posiciones.
          PlayerPlacingShip4,

          ///-PlayerBeginConfirmation. En este comando espera /Begin o /Leaves.
          PlayerBeginConfirmation,

          ///-PlayerReadyToStart. En este comando espera que se le ingrese cualquier letra para continuar.
          PlayerReadyToStart,

          ///-PlayerPvpBattleship. En este comando espera coordenadas para atacar a los barcos del oponente.
          PlayerPvpBattleship,

          ///-RivalPlayerStart: Es el estado inicial del comando. En este comando pide el mensaje de /Game para
          ///asi pasar al siguiente estado.
          RivalPlayerStart,

          ///-RivalPlayerReadyToStartConfirmation. En este comando espera /Ready o /Leave.
          RivalPlayerReadyToStartConfirmation,

          ///-RivalPlayerPlacingShip1. En este comando espera una coordenada para colocar un barco de 2 posiciones.
          RivalPlayerPlacingShip1,

          ///-RivalPlayerPlacingShip2. En este comando espera una coordenada para colocar un barco de 3 posiciones.
          RivalPlayerPlacingShip2,

          ///-RivalPlayerPlacingShip3. En este comando espera una coordenada para colocar un barco de 4 posiciones.
          RivalPlayerPlacingShip3,

          ///-RivalPlayerPlacingShip4. En este comando espera una coordenada para colocar un barco de 5 posiciones.
          RivalPlayerPlacingShip4,

          ///-RivalPlayerBeginConfirmation. En este comando espera /Begin o /Leaves.
          RivalPlayerBeginConfirmation,

          ///-RivalPlayerReadyToStart. En este comando espera que se le ingrese cualquier letra para continuar.
          RivalPlayerReadyToStart,

          ///-RivalPlayerPvpBattleship. En este comando espera coordenadas para atacar a los barcos del oponente.
          RivalPlayerPvpBattleship

        }
    }
}