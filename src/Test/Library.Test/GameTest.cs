using System;
using System.Text;
using System.Collections.Generic;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  //AÑADÍ AQUI ALGUNOS DE LOS TEST PARA LA DEFENSA
  // En esta clase hay muchos tests que no se pueden hacer debido a que dependen del metodo StartGame.
  // En ese metodo se le piden datos al usuario, lo cual no es posible dentro de un test.

  public class GameTest
  {
    [SetUp]
    public void Setup() {}

    // Este test se encarga de verificar que se setee un usuario como ganador correctamente. Y que al obtener el ganador sea el seteado previamente
    [Test]
    public void SetAndGetWinnerTest() // Test de clase
    {
      User joelUser = new User("Joel", "1234");
      User rodrigoUser = new User("Rodrigo", "abcd");

      List<User> users = new List<User>() { joelUser, rodrigoUser };

      Game game = new Game(users, 20, 10, 2, 0);

      game.setWinner(joelUser.Name);

      Assert.AreEqual(game.getWinner(), "Joel");
    }

    // Este test se encarga de verificar que no setee un usuario como ganador si este no pertenece al Game.
    [Test]
    public void SetInvalidWinnerTest() // Test de clase
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      User otherUser = new User("Juan", "0904");

      game.setWinner(otherUser.Name);

      Assert.AreEqual(game.getWinner(), null);
    } 

    // Este test verifica que una partida se cree correctamente. Tambien funciona como test del getter de Users
    [Test]
    public void CreateValidGameTest() // Test de clase y escenario
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);
      Assert.AreEqual(game.getInMatchUsers(), users);
    }

    [Test]
    public void CreateInvalidGameTest() // Test de clase y escenario
    {
      List<User> users = new List<User>() { new User("Joel", "1234") };

      Game game = new Game(users, 20, 10, 2, 0);

      Assert.AreNotEqual(game.getInMatchUsers(), users);
    }

    // Este test verifica que se cree el UserBoard correctamente. Tambien funciona como test del getter de UserBoard
    [Test]
    public void CreateUsersBoardTest() // Test de clase
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      foreach(User user in users)
      {
        Assert.AreEqual(game.getUserBoard(user) is Board, true);
      }
    }

    // Este test verifica que Game no devuelva ningun Board cuando se pide el Board de un usuario que no pertenece a dicho Game
    [Test]
    public void GetInvalidUserBoardTest() // Test de clase y escenario
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      User otherUser = new User("Juan", "0904");

      Assert.AreEqual(game.getUserBoard(otherUser), null);
    }

    // Este test verifica que funcione correctamente el getter de Time. Obtiene el timer y verifica si los minutos y segundos son correctos
    [Test]
    public void GetTimeTest() // Test de clase
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      Assert.AreEqual(game.getTime().getMins(), 20);
      Assert.AreEqual(game.getTime().getSecs(), 10);
    }

    // Este test verifica que funcione correctamente el getter de TimePerRound. Obtiene el timer y verifica si los minutos y segundos son correctos
    [Test]
    public void GetTimePerRoundTest() // Test de clase
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      Assert.AreEqual(game.getTimePerRound().getMins(), 2);
      Assert.AreEqual(game.getTimePerRound().getSecs(), 0);
    }

    // Este test se encarga de verificar que el setter de Time funcione correctamente. Para ello lo redefine y lo obtiene para ver si fue redefinido
    [Test]
    public void SetTimeTest() // Test de clase
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      game.setTime(30, 0);

      Assert.AreEqual(game.getTime().getMins(), 30);
      Assert.AreEqual(game.getTime().getSecs(), 0);
    }

    // Este test se encarga de verificar que el setter de TimePerRound funcione correctamente. Para ello lo redefine y lo obtiene para ver si fue redefinido

    [Test]
    public void SetTimePerRoundTest() // Test de clase
    {
      List<User> users = new List<User>() { new User("Joel", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      game.setTimePerRound(5, 10);

      Assert.AreEqual(game.getTimePerRound().getMins(), 5);
      Assert.AreEqual(game.getTimePerRound().getSecs(), 10);
    }
    
    [Test]
    public void GetTotalMatchHitsWaterHitsTest() // Este test prueba el registro de impactos en agua.
    {
      List<User> users = new List<User>() { new User("RivalTest", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      List<string> RodrigoShip = new List<string>() { "A10" };
      List<string> RivalTestShip = new List<string>() { "A10" };

      game.PlayerAddShipToBoard(RodrigoShip);
      game.RivalPlayerAddShipToBoard(RivalTestShip);

      game.AttackRivalPlayerBoard("J1");

      StringBuilder TestMatchStringBuilder = new StringBuilder("Las estadísticas de la partida son:\n")
                                                                  .Append($"Impactos al agua: 1\n")
                                                                  .Append($"Impactos a barcos: 0\n")
                                                                  .Append($"Puedes usar /MatchInfo para recibir estas actualizaciones, también puedes usar /Leave para salir de la partida.\n");
      string ExpectedResponse = TestMatchStringBuilder.ToString();

      string response = game.GetTotalMatchHits();
      Assert.AreEqual(ExpectedResponse, response);
    }

    [Test]
    public void GetTotalMatchHitsShipHitsTest() // Este test prueba el registro de impactos en barcos.
    {
      List<User> users = new List<User>() { new User("RivalTest", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      List<string> RodrigoShip = new List<string>() { "A10" };
      List<string> RivalTestShip = new List<string>() { "A10" };

      game.PlayerAddShipToBoard(RodrigoShip);
      game.RivalPlayerAddShipToBoard(RivalTestShip);

      game.AttackRivalPlayerBoard("A10");

      StringBuilder TestMatchStringBuilder = new StringBuilder("Las estadísticas de la partida son:\n")
                                                                  .Append($"Impactos al agua: 0\n")
                                                                  .Append($"Impactos a barcos: 1\n")
                                                                  .Append($"Puedes usar /MatchInfo para recibir estas actualizaciones, también puedes usar /Leave para salir de la partida.\n");
      string ExpectedResponse = TestMatchStringBuilder.ToString();

      string response = game.GetTotalMatchHits();
      Assert.AreEqual(ExpectedResponse, response);
    }
    
    [Test]
    public void GetTotalMatchHitsFullBattleshipHitsTest() // Este test prueba el registro de varios impactos tanto en barcos como en agua
    {
      List<User> users = new List<User>() { new User("RivalTest", "1234"), new User("Rodrigo", "abcd") };

      Game game = new Game(users, 20, 10, 2, 0);

      List<string> RodrigoShip = new List<string>() { "A10" };
      List<string> RivalTestShip = new List<string>() { "A1" };
      List<string> RodrigoShip2 = new List<string>() { "J1" };
      List<string> RivalTestShip2 = new List<string>() { "C4" };
      List<string> RodrigoShip3 = new List<string>() { "B8" };
      List<string> RivalTestShip3 = new List<string>() { "C9" };

      game.PlayerAddShipToBoard(RodrigoShip);
      game.RivalPlayerAddShipToBoard(RivalTestShip);
      game.PlayerAddShipToBoard(RodrigoShip2);
      game.RivalPlayerAddShipToBoard(RivalTestShip2);
      game.PlayerAddShipToBoard(RodrigoShip3);
      game.RivalPlayerAddShipToBoard(RivalTestShip3);

      game.AttackRivalPlayerBoard("A1");
      game.AttackPlayerBoard("A10");
      game.AttackRivalPlayerBoard("C4");
      game.AttackPlayerBoard("J1");
      game.AttackRivalPlayerBoard("C9");
      game.AttackPlayerBoard("H5");
      game.AttackRivalPlayerBoard("C2");


      StringBuilder TestMatchStringBuilder = new StringBuilder("Las estadísticas de la partida son:\n")
                                                                  .Append($"Impactos al agua: 2\n")
                                                                  .Append($"Impactos a barcos: 5\n")
                                                                  .Append($"Puedes usar /MatchInfo para recibir estas actualizaciones, también puedes usar /Leave para salir de la partida.\n");
      string ExpectedResponse = TestMatchStringBuilder.ToString();

      string response = game.GetTotalMatchHits();
      Assert.AreEqual(ExpectedResponse, response);
    }
  }
}
