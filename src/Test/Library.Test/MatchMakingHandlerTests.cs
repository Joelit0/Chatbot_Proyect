using NUnit.Framework;
using ChatBotProject;

namespace ChatBotProject.Test
{
    public class MatchMakingHandlerTest
    {
        MatchmakingHandler handler;
        [SetUp]
        public void Setup()
        {
          handler = new MatchmakingHandler(null);
        }

        //Chequeamos que se haga el MatchMaking, jugador contra jugador.
        [Test]
        public void MatchMakingPvP()
        {
          long Testid = 123456;
          UsersList.GetInstance().AddUser("Rodri", "16", Testid);
          UsersList.GetInstance().AddUser("Juan", "16", 4312);
          string message = "";
    
          message = handler.Keywords[0];
          string response;
          handler.Handle(message, Testid, out response);

          message = "/MatchMaking";
          handler.Handle(message, Testid, out response);

          message = "/PvP";
          handler.Handle(message, Testid, out response);

          message = "/No";
          handler.Handle(message, Testid, out response);

          message = "/No";
          handler.Handle(message, Testid, out response);

          message = "Juan";
          IHandler result = handler.Handle(message, Testid , out response);

          Assert.That(result, Is.Not.Null);
          Assert.That(response, Is.EqualTo("Se ha creado la partida, usa /Game para dirigirte a tu partida. ¡Buena suerte!"));

        }

        //Chequeamos que se haga el MatchMaking, jugador contra jugador con timer global de partidas.
        [Test]
        public void MatchMakingPvPWithGlobalTimer()
        {
          long Testid = 123456;
          UsersList.GetInstance().AddUser("Rodri", "16", Testid);
          UsersList.GetInstance().AddUser("Juan", "16", 4312);
          string message = "";
          message = handler.Keywords[0];
          string response;
          handler.Handle(message, Testid, out response);

          message = "/PvP";
          handler.Handle(message, Testid, out response);

          message = "/Si";
          handler.Handle(message, Testid, out response);

          message = "/10";
          handler.Handle(message, Testid, out response);

          message = "/No";
          handler.Handle(message, Testid, out response);

          message = "Juan";
          IHandler result = handler.Handle(message, Testid , out response);

          Assert.That(result, Is.Not.Null);
          Assert.That(response, Is.EqualTo("Se ha creado la partida, usa /Game para dirigirte a tu partida. ¡Buena suerte!"));
        }

        //Chequeamos que se haga el MatchMaking, jugador contra jugador con tiempo por rondas de partidas.
        [Test]
        public void MatchMakingPvPWithRoundTimer()
        {
          long Testid = 123456;
          UsersList.GetInstance().AddUser("Rodri", "16", Testid);
          UsersList.GetInstance().AddUser("Juan", "16", 4312);
          string message = "";
          message = handler.Keywords[0];
          string response;
          handler.Handle(message, Testid, out response);

          message = "/MatchMaking";
          handler.Handle(message, Testid, out response);

          message = "/PvP";
          handler.Handle(message, Testid, out response);

          message = "/No";
          handler.Handle(message, Testid, out response);

          message = "/Si";
          handler.Handle(message, Testid, out response);

          message = "/20";
          handler.Handle(message, Testid, out response);

          message = "Juan";
          IHandler result = handler.Handle(message, Testid , out response);

          Assert.That(result, Is.Not.Null);
          Assert.That(response, Is.EqualTo("Se ha creado la partida, usa /Game para dirigirte a tu partida. ¡Buena suerte!"));
        }
        
        //Chequeamos que se haga el MatchMaking, jugador contra jugador con tiempo por rondas y tiempo global de partidas.
        [Test]
        public void MatchMakingPvPWithRoundAndGlobalTimer()
        {
          long Testid = 123456;
          UsersList.GetInstance().AddUser("Rodri", "16", Testid);
          UsersList.GetInstance().AddUser("Juan", "16", 4312);
          string message = "";
          message = handler.Keywords[0];
          string response;
          handler.Handle(message, Testid, out response);

          message = "/MatchMaking";
          handler.Handle(message, Testid, out response);

          message = "/PvP";
          handler.Handle(message, Testid, out response);

          message = "/Si";
          handler.Handle(message, Testid, out response);

          message = "/10";
          handler.Handle(message, Testid, out response);

          message = "/Si";
          handler.Handle(message, Testid, out response);

          message = "/20";
          handler.Handle(message, Testid, out response);

          message = "Juan";
          IHandler result = handler.Handle(message, Testid , out response);

          Assert.That(result, Is.Not.Null);
          Assert.That(response, Is.EqualTo("Se ha creado la partida, usa /Game para dirigirte a tu partida. ¡Buena suerte!"));
        }

        //Chequeamos que se haga el MatchMaking, jugador contra IA.
        [Test]
        public void MatchMakingPvE()
        {
          long Testid = 123456;
          UsersList.GetInstance().AddUser("Rodri", "16", Testid);
          string message = "";
          message = handler.Keywords[0];
          string response;
          handler.Handle(message, Testid, out response);

          message = "/MatchMaking";
          handler.Handle(message, Testid, out response);

          message = "/PvE";
          IHandler result = handler.Handle(message, Testid , out response);

          Assert.That(result, Is.Not.Null);
          Assert.That(response, Is.EqualTo("Prepárate para luchar contra la IA. Introduzca /GameVsIA para ir a su partida."));

        }
        
        //Chequeamos comando inválido al seleccionar tipo de partida.
        [Test]
        public void MatchMakingInvalidCommand()
        {
          long Testid = 123456;
          UsersList.GetInstance().AddUser("Rodri", "16", Testid);
          UsersList.GetInstance().AddUser("Juan", "16", 4312);
          string message = "";
          message = handler.Keywords[0];
          string response;
          handler.Handle(message, Testid, out response);

          message = "/MatchMaking";
          handler.Handle(message, Testid, out response);

          message = "/RPvP";
          handler.Handle(message, Testid, out response);

          message = "/PSSF";
          handler.Handle(message, Testid, out response);

          IHandler result = handler.Handle(message, Testid , out response);

          Assert.That(result, Is.Not.Null);
          Assert.That(response, Is.EqualTo("Comando inválido, por favor intentelo nuevamente utilizando /PvP o /PvE"));
        }
    }
}


