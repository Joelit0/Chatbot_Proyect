using System;
using System.Collections.Generic;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class GameVsIAHandlerTest
  {
    GameVsIAHandler handler;
    [SetUp]
    public void Setup()
    {
      handler = new GameVsIAHandler(null);
    }

    //Chequeamos GameVsIA Handler ingresar barcos.
    [Test]
    public void GameVsIAHandlerShipsTest()
    {
      long Testid = 123456;
      User player = new User("Juan", "123");
      player.ID = Testid;
      GamesVsIAList.GetInstance().AddGameVsIA(player);
      string message = "";

      message = handler.Keywords[0];
      string response;
      handler.Handle(message, Testid, out response);

      message = "/Ready";
      handler.Handle(message, Testid, out response);

      message = "A1,A2";
      handler.Handle(message, Testid, out response);

      message = "B3,B4,B5";
      handler.Handle(message, Testid, out response);

      message = "C1,C2,C3,C4";
      handler.Handle(message, Testid, out response);

      message = "D1,D2,D3,D4,D5";
      IHandler result = handler.Handle(message, Testid , out response);

      Assert.That(result, Is.Not.Null);
      Assert.That(response, Is.EqualTo("Comience a atacar el Board del Bot. Por ejemplo, A1."));
    }

    //Chequeamos GameVsIA Handler cuando ingresamos el comando /Ready.
    [Test]
    public void GameVsIAHandlerReadyTest()
    {
      long Testid = 123456;
      User player = new User("Juan", "123");
      player.ID = Testid;
      GamesVsIAList.GetInstance().AddGameVsIA(player);
      string message = "";

      message = handler.Keywords[0];
      string response;
      handler.Handle(message, Testid, out response);

      message = "/Ready";
      IHandler result = handler.Handle(message, Testid , out response);

      Assert.That(result, Is.Not.Null);
      Assert.That(response, Is.EqualTo("Ahora deberá ingresar el primer barco de 2 posiciones. Los barcos se ingresan de la siguiente manera **A1,A2,A3,A4** dependiendo del tamaño y posicion del barco"));
    }

    //Chequeamos GameVsIA Handler cuando ingresamos el comando /Leave.
    [Test]
    public void GameVsIAHandlerLeavingTest()
    {
      long Testid = 123456;
      User player = new User("Juan", "123");
      player.ID = Testid;
      GamesVsIAList.GetInstance().AddGameVsIA(player);
      string message = "";

      message = handler.Keywords[0];
      string response;
      handler.Handle(message, Testid, out response);

      message = "/Leave";
      IHandler result = handler.Handle(message, Testid , out response);

      Assert.That(result, Is.Not.Null);
      Assert.That(response, Is.EqualTo("Te has salido de la partida"));
    }

    //Chequeamos si es comando inválido.
    [Test]
    public void GameVsIAHandlerInvalidCommandTest()
    {
      long Testid = 123456;
      User player = new User("Juan", "123");
      player.ID = Testid;
      GamesVsIAList.GetInstance().AddGameVsIA(player);
      string message = "";

      message = handler.Keywords[0];
      string response;
      handler.Handle(message, Testid, out response);

      message = "/QWQWLeave";
      IHandler result = handler.Handle(message, Testid , out response);

      Assert.That(result, Is.Not.Null);
      Assert.That(response, Is.EqualTo("Comando inválido, por favor intentelo nuevamente utilizando /Ready o /Leave"));
    }

    //Chequeamos GameVsIA Handler atacando un barco.
    [Test]
    public void GameVsIAHandlerReadyShipAttackTest()
    {
      long Testid = 123456;
      User player = new User("Juan", "123");
      player.ID = Testid;
      GamesVsIAList.GetInstance().AddGameVsIA(player);
      string message = "";

      message = handler.Keywords[0];
      string response;
      handler.Handle(message, Testid, out response);

      message = "/Ready";
      handler.Handle(message, Testid, out response);

      message = "A1,A2";
      handler.Handle(message, Testid, out response);

      message = "B3,B4,B5";
      handler.Handle(message, Testid, out response);

      message = "C1,C2,C3,C4";
      handler.Handle(message, Testid, out response);

      message = "D1,D2,D3,D4,D5";
      handler.Handle(message, Testid, out response);

      message = "H9";
      IHandler result = handler.Handle(message, Testid , out response);

      Assert.That(result, Is.Not.Null);
      Assert.That(response, Is.EqualTo("El bot te ha atacado. Ahora ingrese su siguiente ataque."));
    }
  }
}