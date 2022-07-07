using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using ChatBotProject;

namespace ChatBotProject.Test
{
    public class GameHandlerTest
    {
        GameHandler handler;

        [SetUp]
        public void Setup()
        {
          handler = new GameHandler(null);        
        }

        // [Test]
        // public void GameHandlerFullTest() //Verifica que ocurre si el handler puede manejar el comando inicial a tarvez de todo su flujo, como el rival no tiene naves se declara vencedor al TestPlayerUser. 
        // {
        //     List<User> matchedUsersTest = new List<User>();
        //     string message = "";
        //     long Testid = 98017487498;
        //     long TestRivalID = 541649848616517;
        //     User TestPlayerUser = new User("PlayerGameTest", "123");
        //     TestPlayerUser.ID = Testid;
        //     User TestRivalPlayerUser = new User("RivalPlayerGameTest", "123");
        //     TestRivalPlayerUser.ID = TestRivalID;
        //     matchedUsersTest.Add(TestPlayerUser);
        //     matchedUsersTest.Add(TestRivalPlayerUser);
            
        //     GamesList.GetInstance().AddGame(matchedUsersTest,0,0,0,0);
        //     message = handler.Keywords[0];
        //     string response;
        //     handler.Handle(message, Testid, out response);

        //     message = "/Ready";
        //     handler.Handle(message, Testid, out response);

        //     message = "A1,A2";
        //     handler.Handle(message, Testid, out response);

        //     message = "B1,B2,B3";
        //     handler.Handle(message, Testid, out response);

        //     message = "C1,C2,C3,C4";
        //     handler.Handle(message, Testid, out response);

        //     message = "D1,D2,D3,D4,D5";
        //     handler.Handle(message, Testid, out response);

        //     message = "/Begin";
        //     handler.Handle(message, Testid, out response);

        //     TestRivalPlayerUser.SetReadyToStartMatch(true);
                   
        //     message = "g";
        //     handler.Handle(message, Testid , out response);

        //     message = "C2";
        //     IHandler result = handler.Handle(message, Testid , out response);

        //     Assert.That(result, Is.Not.Null);
        //     Assert.That(response, Is.EqualTo($"Felicidades {TestPlayerUser.Name}, ganaste!"));
        // }


        // Decidí hacer un test testeando las dos funcionalidades al mismo tiempo. Para no tener que crear otra partida.
        // Para que se vea que en el mismo juego se pueden tirar ambos nuevos comandos
        // Como hablamos en clase, comenté los tests que estaban fallando.
        [Test]
        public void GameHandlerNewFeatureTest() 
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
            Game game = GamesList.GetInstance().Games[0];

            game.RivalPlayerAddShipToBoard(new List<string>() { "A10", "A9" });

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
            handler.Handle(message, Testid , out response);

            message = "/Fails";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(response, Is.EqualTo($"La cantidad de fallos tuyos y del rival son 1"));

            TestPlayerUser.MyTurn = true;

            message = "A10";
            handler.Handle(message, Testid , out response);

            message = "/Hits";
            result = handler.Handle(message, Testid , out response);

            Assert.That(response, Is.EqualTo($"La cantidad de aciertos tuyos y del rival son 1"));
        }

        // [Test]
        // public void GameHandlerLeaveTest() //Verifica que ocurre si el handler puede manejar el comando inicial
        // {
        //     List<User> matchedUsersTest = new List<User>();
        //     string message = "";
        //     long Testid = 98017487498;
        //     User TestPlayerUser = new User("PlayerGameTest", "123");
        //     TestPlayerUser.ID = Testid;
        //     User TestRivalPlayerUser = new User("RivalPlayerGameTest", "123");
        //     matchedUsersTest.Add(TestPlayerUser);
        //     matchedUsersTest.Add(TestRivalPlayerUser);
            
        //     GamesList.GetInstance().AddGame(matchedUsersTest,0,0,0,0);
        //     message = handler.Keywords[0];
        //     string response;
        //     handler.Handle(message, Testid, out response);

        //     message = "/Leave";
        //     IHandler result = handler.Handle(message, Testid , out response);

        //     Assert.That(result, Is.Not.Null);
        //     Assert.That(response, Is.EqualTo("La partida ha sido cancelada porque uno de los jugadores se ha salido."));
        // }

        // [Test]
        // public void TestDoesNotHandle() //Verifica que ocurre si el handler no puede manejar el comando inicial
        // {
        //     string message = "adios";
        //     long Testid = 123456;
        //     string response;

        //     IHandler result = handler.Handle(message, Testid, out response);

        //     Assert.That(result, Is.Null);
        //     Assert.That(response, Is.Empty);
        // }
    }
}