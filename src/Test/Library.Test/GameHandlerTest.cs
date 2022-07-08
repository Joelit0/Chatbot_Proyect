using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using ChatBotProject;

namespace ChatBotProject.Test
{
    //AÑADÍ TESTS PARA LA DEFENSA
    public class GameHandlerTest
    {
        GameHandler handler;

        [SetUp]
        public void Setup()
        {
          handler = new GameHandler(null);
                    
        }

        [Test]
        public void GameHandlerFullTest() //Verifica que ocurre si el handler puede manejar el comando inicial a tarvez de todo su flujo, como el rival no tiene naves se declara vencedor al TestPlayerUser. 
        {
            List<User> matchedUsersTest = new List<User>();
            string message = "";
            long Testid = 98017487498;
            long TestRivalID = 541649848616517;
            User TestPlayerUser = new User("PlayerGameTest", "123");
            TestPlayerUser.ID = Testid;
            User TestRivalPlayerUser = new User("RivalPlayerGameTest", "123");
            TestRivalPlayerUser.ID = TestRivalID;
            matchedUsersTest.Add(TestPlayerUser);
            matchedUsersTest.Add(TestRivalPlayerUser);
            
            GamesList.GetInstance().AddGame(matchedUsersTest,0,0,0,0);
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Ready";
            handler.Handle(message, Testid, out response);

            message = "A1,A2";
            handler.Handle(message, Testid, out response);

            message = "B1,B2,B3";
            handler.Handle(message, Testid, out response);

            message = "C1,C2,C3,C4";
            handler.Handle(message, Testid, out response);

            message = "D1,D2,D3,D4,D5";
            handler.Handle(message, Testid, out response);

            message = "/Begin";
            handler.Handle(message, Testid, out response);

            TestRivalPlayerUser.SetReadyToStartMatch(true);
                   
            message = "g";
            handler.Handle(message, Testid , out response);

            message = "C2";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Felicidades {TestPlayerUser.Name}, ganaste!"));
        }

        [Test]
        public void GameHandlerLeaveTest() //Verifica que ocurre si el handler puede manejar el comando inicial
        {
            List<User> matchedUsersTest = new List<User>();
            string message = "";
            long Testid = 98017487498;
            User TestPlayerUser = new User("PlayerGameTest", "123");
            TestPlayerUser.ID = Testid;
            User TestRivalPlayerUser = new User("RivalPlayerGameTest", "123");
            matchedUsersTest.Add(TestPlayerUser);
            matchedUsersTest.Add(TestRivalPlayerUser);
            
            GamesList.GetInstance().AddGame(matchedUsersTest,0,0,0,0);
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Leave";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("La partida ha sido cancelada porque uno de los jugadores se ha salido."));
        }
 
        [Test]
        public void TestDoesNotHandle() //Verifica que ocurre si el handler no puede manejar el comando inicial
        {
            string message = "adios";
            long Testid = 123456;
            string response;

            IHandler result = handler.Handle(message, Testid, out response);

            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }

        [Test]
        public void GameHandlerWaterHitMarkerTest() //Verifica que el handler puede responder al comando /MatchInfo en este caso registrando ataques al agua 
        {
            List<User> matchedUsersTest = new List<User>();
            string message = "";
            long Testid = 98017487498;
            long TestRivalID = 541649848616517;
            User TestPlayerUser = new User("PlayerGameTest", "123");
            TestPlayerUser.ID = Testid;
            User TestRivalPlayerUser = new User("RivalPlayerGameTest", "123");
            TestRivalPlayerUser.ID = TestRivalID;
            matchedUsersTest.Add(TestPlayerUser);
            matchedUsersTest.Add(TestRivalPlayerUser);

            List<string> RivalTestShip = new List<string>() { "A1" };
            
            
            GamesList.GetInstance().AddGame(matchedUsersTest,0,0,0,0);
                       
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Ready";
            handler.Handle(message, Testid, out response);

            message = "A1,A2";
            handler.Handle(message, Testid, out response);

            message = "B1,B2,B3";
            handler.Handle(message, Testid, out response);

            message = "C1,C2,C3,C4";
            handler.Handle(message, Testid, out response);

            message = "D1,D2,D3,D4,D5";
            handler.Handle(message, Testid, out response);

            message = "/Begin";
            handler.Handle(message, Testid, out response);

            TestRivalPlayerUser.SetReadyToStartMatch(true);
            handler.CurrentGame.RivalPlayerAddShipToBoard(RivalTestShip);
                   
            message = "g";
            handler.Handle(message, Testid , out response);

            message = "C2";
            handler.Handle(message, Testid , out response);

            StringBuilder TestMatchStringBuilder = new StringBuilder("Las estadísticas de la partida son:\n")
                                                                  .Append($"Impactos al agua: 1\n")
                                                                  .Append($"Impactos a barcos: 0\n")
                                                                  .Append($"Puedes usar /MatchInfo para recibir estas actualizaciones, también puedes usar /Leave para salir de la partida.\n");
            string ExpectedResponse = TestMatchStringBuilder.ToString();


            message = "/MatchInfo";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo(ExpectedResponse));
        }

        [Test]
        public void GameHandlerShipHitMarkerTest() //Verifica que el handler puede responder al comando /MatchInfo en este caso registrando ataques a barcos
        {
            List<User> matchedUsersTest = new List<User>();
            string message = "";
            long Testid = 98017487498;
            long TestRivalID = 541649848616517;
            User TestPlayerUser = new User("PlayerGameTest", "123");
            TestPlayerUser.ID = Testid;
            User TestRivalPlayerUser = new User("RivalPlayerGameTest", "123");
            TestRivalPlayerUser.ID = TestRivalID;
            matchedUsersTest.Add(TestPlayerUser);
            matchedUsersTest.Add(TestRivalPlayerUser);

            List<string> RivalTestShip = new List<string>() { "A1" };
            List<string> RivalTestShip2 = new List<string>() { "C4" };
            
            
            GamesList.GetInstance().AddGame(matchedUsersTest,0,0,0,0);
                       
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Ready";
            handler.Handle(message, Testid, out response);

            message = "A1,A2";
            handler.Handle(message, Testid, out response);

            message = "B1,B2,B3";
            handler.Handle(message, Testid, out response);

            message = "C1,C2,C3,C4";
            handler.Handle(message, Testid, out response);

            message = "D1,D2,D3,D4,D5";
            handler.Handle(message, Testid, out response);

            message = "/Begin";
            handler.Handle(message, Testid, out response);

            TestRivalPlayerUser.SetReadyToStartMatch(true);
            handler.CurrentGame.RivalPlayerAddShipToBoard(RivalTestShip);
            handler.CurrentGame.RivalPlayerAddShipToBoard(RivalTestShip2);
                   
            message = "g";
            handler.Handle(message, Testid , out response);

            message = "A1";
            handler.Handle(message, Testid , out response);

            StringBuilder TestMatchStringBuilder = new StringBuilder("Las estadísticas de la partida son:\n")
                                                                  .Append($"Impactos al agua: 0\n")
                                                                  .Append($"Impactos a barcos: 1\n")
                                                                  .Append($"Puedes usar /MatchInfo para recibir estas actualizaciones, también puedes usar /Leave para salir de la partida.\n");
            string ExpectedResponse = TestMatchStringBuilder.ToString();


            message = "/MatchInfo";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo(ExpectedResponse));
        }

        [Test]
        public void GameHandlerFullBattleshipHitMarkerTest() //Verifica que el handler puede responder al comando /MatchInfo en este caso registrando TANTO ataques a barcos como al agua
        {
            List<User> matchedUsersTest = new List<User>();
            string message = "";
            long Testid = 98017487498;
            long TestRivalID = 541649848616517;
            User TestPlayerUser = new User("PlayerGameTest", "123");
            TestPlayerUser.ID = Testid;
            User TestRivalPlayerUser = new User("RivalPlayerGameTest", "123");
            TestRivalPlayerUser.ID = TestRivalID;
            matchedUsersTest.Add(TestPlayerUser);
            matchedUsersTest.Add(TestRivalPlayerUser);

            List<string> RivalTestShip = new List<string>() { "A1" };
            List<string> RivalTestShip2 = new List<string>() { "C4" };
            List<string> RivalTestShip3 = new List<string>() { "C9" };
            
            GamesList.GetInstance().AddGame(matchedUsersTest,0,0,0,0);
                       
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Ready";
            handler.Handle(message, Testid, out response);

            message = "A1,A2";
            handler.Handle(message, Testid, out response);

            message = "B1,B2,B3";
            handler.Handle(message, Testid, out response);

            message = "C1,C2,C3,C4";
            handler.Handle(message, Testid, out response);

            message = "D1,D2,D3,D4,D5";
            handler.Handle(message, Testid, out response);

            message = "/Begin";
            handler.Handle(message, Testid, out response);

            TestRivalPlayerUser.SetReadyToStartMatch(true);
            handler.CurrentGame.RivalPlayerAddShipToBoard(RivalTestShip);
            handler.CurrentGame.RivalPlayerAddShipToBoard(RivalTestShip2);
            handler.CurrentGame.RivalPlayerAddShipToBoard(RivalTestShip3);
                   
            message = "g";
            handler.Handle(message, Testid , out response);

            message = "A1";
            handler.Handle(message, Testid , out response);

            handler.Player.MyTurn = true;
            handler.RivalPlayer.MyTurn = false;

            message = "C1";
            handler.Handle(message, Testid , out response);

            message = "/MatchInfo";
            IHandler result = handler.Handle(message, Testid , out response);

            StringBuilder TestMatchStringBuilder = new StringBuilder("Las estadísticas de la partida son:\n")
                                                                  .Append($"Impactos al agua: 1\n")
                                                                  .Append($"Impactos a barcos: 1\n")
                                                                  .Append($"Puedes usar /MatchInfo para recibir estas actualizaciones, también puedes usar /Leave para salir de la partida.\n");
            string ExpectedResponse = TestMatchStringBuilder.ToString();

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo(ExpectedResponse));
        }
    }
}