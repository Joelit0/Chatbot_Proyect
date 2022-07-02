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

    //Testea el getter de los minutos del timer.
    [Test]
    public void GetMinutesTest()
    {
      Timer timer = new Timer(10, 20);
      Assert.AreEqual(10, timer.getMins());
    }

    public void SetMinutesTest()
    {
      Timer timer = new Timer(10, 20);
      timer.setMins(30);

      Assert.AreEqual(30, timer.getMins());
    }

    //Testea el getter de los segundos del timer.
    [Test]
    public void GetSecondsTest()
    {
      Timer timer = new Timer(10, 20);
      Assert.AreEqual(20, timer.getSecs());
    }

    [Test]
    public void SetSecondsTest()
    {
      Timer timer = new Timer(10, 20);
      timer.setSecs(50);

      Assert.AreEqual(50, timer.getSecs());
    }
  }
} 