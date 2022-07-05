using System;
using System.Collections.Generic;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class GamesVsIAListTest
  {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void AddGameVsIATest() //Prueba la funcionalidad del método AddGame para crear y añadir una partida.
      {
        User Cristiano = new User("Cristiano","TestPassword");

        GamesVsIAList.GetInstance().AddGameVsIA(Cristiano);
        int contador = 0;
        foreach (GameVsIA game in GamesVsIAList.GetInstance().GamesVsIA)
        { 
            contador += 1;
        }
        int expected = 1;
        Assert.AreEqual(expected, contador);
        GamesVsIAList.GetInstance().RemoveGame(GamesVsIAList.GetInstance().GamesVsIA[0]);
      }

      [Test]
      public void RemoveGameTest() //Prueba la funcionalidad del método RemoveGame para remover una partida.
      {
        User Cristiano = new User("Cristiano","TestPassword");

        GamesVsIAList.GetInstance().AddGameVsIA(Cristiano);
        GamesVsIAList.GetInstance().RemoveGame(GamesVsIAList.GetInstance().GamesVsIA[0]);
        int contador = 0;
        foreach (GameVsIA game in GamesVsIAList.GetInstance().GamesVsIA)
        { 
            contador += 1;
        }
        int expectedInstances = 0;
        Assert.AreEqual(expectedInstances, contador);
      }
  }

}