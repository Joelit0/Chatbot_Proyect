namespace ChatBotProject
{
  public class Game
  {
    private List<User> InMatchUsers;
    private Timer Time;
    private Timer TimePerRound;
    private bool InProgress;
    private User Winner;
    private Dictionary<User, Board> InMatchUsersBoard;

    public Game(List<User> InMatchUsers, int totalMins, int totalSecs, int minsPerRound, int secsPerRound)
    {
      if (InMatchUsers.Count() == 2)
      {
        this.InMatchUsers = InMatchUsers;
        this.InProgress = false;
        this.InMatchUsersBoard = new Dictionary<User, Board>();
        this.Time = new Timer(totalMins, totalSecs);
        this.TimePerRound = new Timer(minsPerRound, secsPerRound);

        // Llenar el diccionario con clave User y Valor Board
        foreach(User user in this.InMatchUsers)
        {
          this.InMatchUsersBoard.Add(user, new Board(10, 10));
        }
      }
    }

    public void StartGame()
    {
      // Crear telegramPrinter de tipo IPrinter
      IPrinter telegramPrinter = new TelegramPrinter();

      foreach(User user in this.InMatchUsers)
      {
        List<List<string>> InMatchUsershipsPositions = getInMatchUsershipsPositions();
        Board userBoard = getUserBoard(user);

        foreach(List<string> shipPositions in InMatchUsershipsPositions)
        {
          userBoard.addShip(shipPositions);
        }

        userBoard.showShips();

        telegramPrinter.printBoard(userBoard, user.ID);
      }

      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      string attackPosition;

      while(firstUserBoard.getShips().Count != 0 && secondUserBoard.getShips().Count != 0)
      {
        // Ataque del primer usuario
        Console.WriteLine("===============================================");
        Console.WriteLine($"Turno de {this.InMatchUsers[0].Name}");
        Console.WriteLine("===============================================");

        Console.WriteLine("Tu tablero:");
        firstUserBoard.showShips();
        telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[0].ID);
  
        Console.WriteLine($"Tablero de {this.InMatchUsers[1].Name}:");
        secondUserBoard.hideShips(); // Oculta los barcos
        telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[0].ID);

        // Leer ataque
        attackPosition = Console.ReadLine().ToUpper();
        secondUserBoard.attack(attackPosition);

        // Ataque del segundo usuario
        Console.WriteLine("===============================================");
        Console.WriteLine($"Turno de {this.InMatchUsers[1].Name}");
        Console.WriteLine("===============================================");

        Console.WriteLine("Tu tablero:");
        secondUserBoard.showShips();
        telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[1].ID);
  
        Console.WriteLine($"Tablero de {this.InMatchUsers[0].Name}:");
        firstUserBoard.hideShips(); // Oculta los barcos
        telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[1].ID);

        // Leer ataque
        attackPosition = Console.ReadLine().ToUpper();
        firstUserBoard.attack(attackPosition);
      }

      // Actualiza el ganador de la partida
      if (firstUserBoard.getShips().Count == 0) { this.Winner = this.InMatchUsers[1]; }

      if (secondUserBoard.getShips().Count == 0) { this.Winner = this.InMatchUsers[0]; }

      Console.WriteLine($"La partida terminó. El ganador es {this.Winner.Name}");

      FinishGame();
    }

    private void FinishGame()
    {
      this.InProgress = false;
    }

    private List<List<string>> getInMatchUsershipsPositions()
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
      if (this.InMatchUsers.Contains(user))
      {
        this.Winner = user;
      }
    }

    // InMatchUsers Getter
    public List<User> getInMatchUsers()
    {
      return this.InMatchUsers;
    }

    // Time Getters & Setters

    public Timer getTime()
    {
      return this.Time;
    }

    public void setTime(int mins, int secs)
    {
      this.Time.setMins(mins);
      this.Time.setSecs(secs);
    }

    // TimePerRound Getters & Setters

    public Timer getTimePerRound()
    {
      return this.TimePerRound;
    }

    public void setTimePerRound(int mins, int secs)
    {
      this.TimePerRound.setMins(mins);
      this.TimePerRound.setSecs(secs);
    }

    // Devuelve el board del usuario que se le pase
    public Board getUserBoard(User user)
    {
      if (this.InMatchUsers.Contains(user)) 
      {
        foreach (KeyValuePair<User, Board> userBoard in this.InMatchUsersBoard)
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