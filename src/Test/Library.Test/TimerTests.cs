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

    //Testea cuando se acaba el timer.
      [Test]
      public void TestStartTimer()
      {
        Timer temporizador = new Timer(0, 0);
        Assert.AreEqual("Se ha acabado el tiempo", temporizador.StartTimer());
      }
  }
}