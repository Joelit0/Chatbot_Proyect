using ChatBotProject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
namespace ChatBotProject.Test
{
  public class TimerTest
  {
      [SetUp]
      public void Setup()
      {
      }

    //Testea la obtención de los segundos del timer.
      [Test]
      public void TestSeconds()
      {
        Timer temporizador = new Timer(1, 2);
        Assert.AreEqual(2, temporizador.Secs);
      }
      
    //Testea la obtención de los minutos del timer.
      [Test]
      public void TestMinutes()
      {
        Timer temporizador = new Timer(1, 2);
        Assert.AreEqual(1, temporizador.Mins);
      }
  }
} 