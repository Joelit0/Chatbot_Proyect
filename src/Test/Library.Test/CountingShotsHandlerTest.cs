using NUnit.Framework;
using ChatBotProject;

namespace ChatBotProject.Test
{
    public class CountingShotsHandlerTest
    {
      CountingShotsHandler handler;
      [SetUp]
      public void Setup()
      {
        handler = new CountingShotsHandler(null);
      }

      //Chequeamos que se haga el CountingShots.
      [Test]
      public void CountingShotsOnShipTest()
      {
        long Testid = 123456;
        UsersList.GetInstance().AddUser("Rodri", "16", Testid);
        UsersList.GetInstance().AddUser("Juan", "16", 4312);
        string message = "";
  
        message = handler.Keywords[0];
        string response;
        handler.Handle(message, Testid, out response);

        message = "/CountingShots";
        handler.Handle(message, Testid, out response);

        message = "/ShotsOnShipsCount";
        IHandler result = handler.Handle(message, Testid , out response);

        Assert.That(result, Is.Not.Null);
        Assert.That(response, Is.EqualTo("Cantidad de veces que disparaste un barco:"));
      }

      [Test]
      public void CountingShotsOnWaterTest()
      {
        long Testid = 123456;
        UsersList.GetInstance().AddUser("Rodri", "16", Testid);
        UsersList.GetInstance().AddUser("Juan", "16", 4312);
        string message = "";
  
        message = handler.Keywords[0];
        string response;
        handler.Handle(message, Testid, out response);

        message = "/CountingShots";
        handler.Handle(message, Testid, out response);

        message = "/ShotsOnWaterCount";
        IHandler result = handler.Handle(message, Testid , out response);

        Assert.That(result, Is.Not.Null);
        Assert.That(response, Is.EqualTo("Cantidad de veces que disparaste agua:"));
      }

      [Test]
      public void CountingShotsInvalidComand()
      {
        long Testid = 123456;
        UsersList.GetInstance().AddUser("Rodri", "16", Testid);
        UsersList.GetInstance().AddUser("Juan", "16", 4312);
        string message = "";
  
        message = handler.Keywords[0];
        string response;
        handler.Handle(message, Testid, out response);

        message = "/CountingShots";
        handler.Handle(message, Testid, out response);

        message = "/dfsaShotsOnWaterCount";
        IHandler result = handler.Handle(message, Testid , out response);

        Assert.That(result, Is.Not.Null);
        Assert.That(response, Is.EqualTo("Comando inválido"));
      }
    }
}