using System;
using System.Collections.Generic;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
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
  }
}
