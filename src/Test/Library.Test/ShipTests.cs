using ChatBotProject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
namespace ChatBotProject.Test
{
  public class ShipTest
  {
      [SetUp]
      public void Setup()
      {
      }

    //Testea la obtención del largo del barco (cuantos casilleros ocupa).
      [Test]
      public void TestGetLarge()
      {
        List<string> positions = new List<string>();
        positions.Add("A1".ToUpper());
        positions.Add("A2".ToUpper());
        Ship lancha = new Ship(positions);
        Assert.AreEqual(2, lancha.getLarge());
      }

    //Testea la obtención del Score (puntuaje del barco).
        [Test]
      public void TestGetScore() 
      {
        List<string> positions = new List<string>();
        positions.Add("A1".ToUpper());
        positions.Add("A2".ToUpper());
        Ship lancha = new Ship(positions);
        Assert.AreEqual(18, lancha.getScore()); // 20 - Large = 18.
      }

    //Testea si al ser largo > 0, barco no está hundido.
        [Test]
      public void TestCheckIsAlive()
      {   
        List<string> positions = new List<string>();
        positions.Add("A1".ToUpper());
        positions.Add("A2".ToUpper());
        Ship lancha = new Ship(positions);
        Assert.AreEqual(true, lancha.checkIsAlive());
      }
    //Testea si al ser largo == 0, barco está hundido.
        [Test]
      public void TestCheckIsAlive2()
      {   
        List<string> positions = new List<string>();
        Ship lancha = new Ship(positions);
        Assert.AreEqual(false, lancha.checkIsAlive());
      }

    //Testea si se remueven las posiciones.
        [Test]
      public void TestRemovePositions()
      {   
        List<string> positions = new List<string>();
        positions.Add("A1".ToUpper());
        positions.Add("A2".ToUpper());
        Ship lancha = new Ship(positions);
        lancha.removePosition("A2");
        Assert.AreEqual(1, lancha.getLarge());
      }

    //Testea la obtención de las posiciones.
        [Test]
      public void TestGetPositions()
      {   
        List<string> positions = new List<string>();
        positions.Add("A1".ToUpper());
        positions.Add("B2".ToUpper());
        Ship lancha = new Ship(positions);
        List<string> testear = new List<string>();
        testear.Add("A1".ToUpper());
        testear.Add("B2".ToUpper());
        Assert.AreEqual(testear, lancha.getPositions());
      }    
  }
} 