namespace ChatBotProject
{
  public class Game
  {
    private List<User> InMatchUsers;
    private Timer Time;
    private Timer TimePerRound;
    private bool InProgress;
    private string Winner;
    private Dictionary<User, Board> InMatchUsersBoard;
    private TelegramPrinter telegramPrinter;

    public Game(List<User> InMatchUsers, int totalMins, int totalSecs, int minsPerRound, int secsPerRound)
    {
      if (InMatchUsers.Count() == 2)
      {
        this.InMatchUsers = InMatchUsers;
        this.InProgress = false;
        this.InMatchUsersBoard = new Dictionary<User, Board>();
        this.Time = new Timer(totalMins, totalSecs);
        this.TimePerRound = new Timer(minsPerRound, secsPerRound);
        this.telegramPrinter = new TelegramPrinter();
        // Llenar el diccionario con clave User y Valor Board
        foreach(User user in this.InMatchUsers)
        {
          this.InMatchUsersBoard.Add(user, new Board(10, 10));
        }
      }
    }

    public void StartGame()
    {
      this.InProgress = true;
    }

    public void FinishGame()
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
        string position = "A1";
        firstShipPositions.Add(position);
      }

      Console.WriteLine("Ingrese las 3 pocisiones del segundo barco: ");

      for(int i = 0; i < 3; i++)
      {
        string position = "A2";
        secondShipPositions.Add(position);
      }

      Console.WriteLine("Ingrese las 4 pocisiones del tercer barco: ");

      for(int i = 0; i < 4; i++)
      {
        string position = "A3";
        thirdShipPositions.Add(position);
      }

      Console.WriteLine("Ingrese las 5 pocisiones del ultimo barco: ");
      for(int i = 0; i < 5; i++)
      {
        string position = "A4";
        fourthShipPositions.Add(position);
      }

      allShipsPositions.Add(firstShipPositions);
      allShipsPositions.Add(secondShipPositions);
      allShipsPositions.Add(thirdShipPositions);
      allShipsPositions.Add(fourthShipPositions);

      return allShipsPositions;
    }


    public void PrintPlayerBoardToSelf()
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      firstUserBoard.showShips();
      this.telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[0].ID);
    }

    public void PrintPlayerBoardToEnemy()
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      firstUserBoard.hideShips();
      this.telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[1].ID);
    }

    public void PlayerAddShipToBoard(List<string> positions)
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      firstUserBoard.addShip(positions);
    }

    public bool PlayerBoardHasShips()
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      return firstUserBoard.getShips().Count != 0;
    }

    public void AttackRivalPlayerBoard(string attackPosition)
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      secondUserBoard.attack(attackPosition);
    }

    public void PrintRivalPlayerBoardToSelf()
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      secondUserBoard.showShips();
      this.telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[1].ID);
    }

    public void PrintRivalPlayerBoardToEnemy()
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      secondUserBoard.hideShips();
      this.telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[0].ID);
    }

    public void RivalPlayerAddShipToBoard(List<string> positions)
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      secondUserBoard.addShip(positions);
    }

    public bool RivalPlayerBoardHasShips()
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      return secondUserBoard.getShips().Count != 0;
    }

    public void AttackPlayerBoard(string attackPosition)
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      firstUserBoard.attack(attackPosition);
    }

    

    // Winner Getters & Setters
    public string getWinner()
    {
      return this.Winner;
    }

    public void setWinner(string winner)
    {
      foreach( User player in this.InMatchUsers)
      {
        if (player.Name == winner)
        {
          this.Winner = winner;
        }
        
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
