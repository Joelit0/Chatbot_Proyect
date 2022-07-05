using System;
using System.Collections.Generic;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class GameVsIATests
  {
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public void TestplayerBoardHasShips() 
    {
      User player = new User("marta","1");
      GameVsIA test = new GameVsIA(player);
      List<string> positions =  new List<string>() {"A1", "B1"};
      test.AddShipToBoard(positions);

      Assert.AreEqual(true,test.playerBoardHasShips());
    }
    [Test]
    public void TestplayerBoardWithNoShips() 
    {
      User player = new User("marta","1");
      GameVsIA test = new GameVsIA(player);
      Assert.AreEqual(false,test.playerBoardHasShips());
    }

    //Chequea la obtención del ganador.
    [Test]
    public void TestGetAndSetWinner() 
    {
      User player = new User("marta","1");
      GameVsIA test = new GameVsIA(player);
      test.setWinner("marta");
      Assert.AreEqual("marta",test.getWinner());
    }

    //Chequea la obtención del jugador.
    [Test]
    public void TestGetPlayer() 
    {
      User player = new User("juan","123");
      GameVsIA test = new GameVsIA(player);
      Assert.AreEqual("juan",test.getPlayer().Name);
    }

  }
}