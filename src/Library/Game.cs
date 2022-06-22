namespace ChatBotProject
{
  public class Game
  {
    private List<User> Users;
    private Timer Time;
    private Timer TimePerRound;
    private bool InProgress;
    private User Winner;
    private Dictionary<User, Board> UsersBoard;

    public Game(List<User> users, Timer Time, Timer TimePerRound)
    {
      if (users.Count() == 2)
      {
        this.Users = users;
        this.InProgress = false;
        this.UsersBoard = new Dictionary<User, Board>();

        // Llenar el diccionario con clave User y Valor Board
        foreach(User user in this.Users)
        {
          this.UsersBoard.Add(user, new Board(10, 10));
        }
      }
    }

    public void StartGame()
    {
      // Crear ConsolePrinter de tipo IPrinter
      IPrinter consolePrinter = new ConsolePrinter();

      foreach(User user in this.Users)
      {
        List<List<string>> userShipsPositions = getUserShipsPositions();
        Board userBoard = getUserBoard(user);

        foreach(List<string> shipPositions in userShipsPositions)
        {
          userBoard.addShip(shipPositions);
        }

        userBoard.showShips();

        consolePrinter.printBoard(userBoard);
      }

      Board firstUserBoard = getUserBoard(this.Users[0]);
      Board secondUserBoard = getUserBoard(this.Users[1]);
      string attackPosition;

      while(firstUserBoard.getShips().Count != 0 && secondUserBoard.getShips().Count != 0)
      {
        // Ataque del primer usuario
        Console.WriteLine("===============================================");
        Console.WriteLine($"Turno de {this.Users[0].Name}");
        Console.WriteLine("===============================================");

        Console.WriteLine("Tu tablero:");
        firstUserBoard.showShips();
        consolePrinter.printBoard(firstUserBoard);
  
        Console.WriteLine($"Tablero de {this.Users[1].Name}:");
        secondUserBoard.hideShips(); // Oculta los barcos
        consolePrinter.printBoard(secondUserBoard);

        // Leer ataque
        attackPosition = Console.ReadLine().ToUpper();
        secondUserBoard.attack(attackPosition);

        // Ataque del segundo usuario
        Console.WriteLine("===============================================");
        Console.WriteLine($"Turno de {this.Users[1].Name}");
        Console.WriteLine("===============================================");

        Console.WriteLine("Tu tablero:");
        secondUserBoard.showShips();
        consolePrinter.printBoard(secondUserBoard);
  
        Console.WriteLine($"Tablero de {this.Users[0].Name}:");
        firstUserBoard.hideShips(); // Oculta los barcos
        consolePrinter.printBoard(firstUserBoard);

        // Leer ataque
        attackPosition = Console.ReadLine().ToUpper();
        firstUserBoard.attack(attackPosition);
      }

      // Actualiza el ganador de la partida
      if (firstUserBoard.getShips().Count == 0) { this.Winner = this.Users[1]; }

      if (secondUserBoard.getShips().Count == 0) { this.Winner = this.Users[0]; }

      Console.WriteLine($"La partida terminó. El ganador es {this.Winner.Name}");

      FinishGame();
    }

    private void FinishGame()
    {
      this.InProgress = false;
    }

    private List<List<string>> getUserShipsPositions()
    {
      List<List<string>> allShipsPositions = new List<List<string>>();
  
      List<string> firstShipPositions = new List<string>();
      List<string> secondShipPositions = new List<string>();
      List<string> thirdShipPositions = new List<string>();
      List<string> fourthShipPositions = new List<string>();

      Console.WriteLine("Ingrese las 2 pocisiones del primer barco: ");
  
      for(int i = 0; i < 2; i++)
      {
        string position = Console.ReadLine().ToUpper();
        firstShipPositions.Add(position);
      }

      Console.WriteLine("Ingrese las 3 pocisiones del segundo barco: ");

      for(int i = 0; i < 3; i++)
      {
        string position = Console.ReadLine().ToUpper();
        secondShipPositions.Add(position);
      }

      Console.WriteLine("Ingrese las 4 pocisiones del tercer barco: ");

      for(int i = 0; i < 4; i++)
      {
        string position = Console.ReadLine().ToUpper();
        thirdShipPositions.Add(position);
      }

      Console.WriteLine("Ingrese las 5 pocisiones del ultimo barco: ");
      for(int i = 0; i < 5; i++)
      {
        string position = Console.ReadLine().ToUpper();
        fourthShipPositions.Add(position);
      }

      allShipsPositions.Add(firstShipPositions);
      allShipsPositions.Add(secondShipPositions);
      allShipsPositions.Add(thirdShipPositions);
      allShipsPositions.Add(fourthShipPositions);

      return allShipsPositions;
    }

    // Winner Getters & Setters
    public User getWinner()
    {
      return this.Winner;
    }

    public void setWinner(User user)
    {
      if (this.Users.Contains(user))
      {
        this.Winner = user;
      }
    }

    // Users Getter
    public User getUsers()
    {
      return this.Winner;
    }

    // Time Getters & Setters

    public Timer getTime()
    {
      return this.Time;
    }

    public void setTime(Timer time)
    {
      this.Time = time;
    }

    // TimePerRound Getters & Setters

    public Timer getTimePerRound()
    {
      return this.TimePerRound;
    }

    public void setTimePerRound(Timer timePerRound)
    {
      this.TimePerRound = timePerRound;
    }

    // Devuelve el board del usuario que se le pase
    public Board getUserBoard(User user)
    {
      if (this.Users.Contains(user)) 
      {
        foreach (KeyValuePair<User, Board> userBoard in this.UsersBoard)
        {
          if (userBoard.Key == user)
          {
            return userBoard.Value;
          }
        }
      }

      return null;
    }
  }
}
