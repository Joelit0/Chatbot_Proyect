using System;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class ShipTest
  {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void TestGetScore() // Testea la obtención del Score (puntuaje del barco).
      {
          Ship lancha = new Ship(12,32);
          Assert.AreEqual(12,lancha.Score);
          
      }
      
      [Test]
      public void TestGetLarge() // Testea la obtención del largo del barco (cuantos casilleros ocupa).
      {
          Ship lancha = new Ship(12,32);
          Assert.AreEqual(32,lancha.Large);
      }
      
      [Test]
      public void TestCheckIsAlive() //Testea si al ser largo == 0, barco está hundido.
      {   
          Ship lancha = new Ship(12,0);
          bool valor = lancha.checkIsAlive(0);
          Assert.AreEqual(false, valor);
      }
      
  }
}