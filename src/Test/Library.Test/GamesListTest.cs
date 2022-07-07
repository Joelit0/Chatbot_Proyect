using System;
using System.Collections.Generic;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class GamesListTest
  {
      [SetUp]
      public void Setup()
      {
      }

      // [Test]
      // public void AddGameTest() //Prueba la funcionalidad del método AddGame para crear y añadir una partida.
      // {
      //   List<User> matchedUsersTest = new List<User>();
      //   User Benzema = new User("Benzema","TestPassword");
      //   User Cristiano = new User("Cristiano","TestPassword");

      //   GamesList.GetInstance().AddGame(matchedUsersTest, 0,0,0,0);
      //   int contador = 0;
      //   foreach (Game game in GamesList.GetInstance().Games)
      //   { 
      //       contador += 1;
      //   }
      //   int expected = 1;
      //   Assert.AreEqual(expected, contador);
      //   GamesList.GetInstance().RemoveGame(GamesList.GetInstance().Games[0]);
      // }

      // [Test]
      // public void RemoveGameTest() //Prueba la funcionalidad del método RemoveGame para remover una partida.
      // {
      //   List<User> matchedUsersTest = new List<User>();
      //   User Benzema = new User("Benzema","TestPassword");
      //   User Cristiano = new User("Cristiano","TestPassword");

      //   GamesList.GetInstance().AddGame(matchedUsersTest, 0,0,0,0);
      //   GamesList.GetInstance().RemoveGame(GamesList.GetInstance().Games[0]);
      //   int contador = 0;
      //   foreach (Game game in GamesList.GetInstance().Games)
      //   { 
      //       contador += 1;
      //   }
      //   int expectedInstances = 0;
      //   Assert.AreEqual(expectedInstances, contador);
      // }
  }

}