
namespace ChatBotProject
{
  public class GameVsIA
  {
    private User Player;
    private bool InProgress;
    private string Winner;
    private Board PlayerBoard;
    private Board BotBoard;
    private TelegramPrinter telegramPrinter;

    public GameVsIA(User player)
    {
      this.Player = player;
      this.InProgress = false;
      this.PlayerBoard = new Board(10, 10);
      this.BotBoard = new Board(10, 10);
      this.telegramPrinter = new TelegramPrinter();
    }

    public void printPlayerBoard()
    {
      this.PlayerBoard.showShips();

      this.telegramPrinter.printBoard(this.PlayerBoard, this.Player.ID);
    }

    public void attackBotBoard(string attackPosition)
    {
      this.BotBoard.attack(attackPosition);
    }
    
    public void printBotBoard()
    {
      this.BotBoard.hideShips();

      this.telegramPrinter.printBoard(this.BotBoard, this.Player.ID);
    }

    public void AddShipToBoard(List<string> positions)
    {
      this.PlayerBoard.addShip(positions);
    }
    public void generateBotShips()
    {
      List<List<List<string>>> possibleShips = new List<List<List<string>>>() {
        new List<List<string>>() {
          new List<string>() {"J1", "J2"},
          new List<string>() {"H1", "H2", "H3"},
          new List<string>() {"H10", "G10", "I10", "J10"},
          new List<string>() {"B3", "B4", "B5", "B6", "B7"}
        },
        new List<List<string>>() {
          new List<string>() {"J1", "J2"},
          new List<string>() {"H1", "H2", "H3"},
          new List<string>() {"H10", "G10", "I10", "J10"},
          new List<string>() {"B3", "B4", "B5", "B6", "B7"}
        },
        new List<List<string>>() {
          new List<string>() {"J1", "J2"},
          new List<string>() {"H1", "H2", "H3"},
          new List<string>() {"H10", "G10", "I10", "J10"},
          new List<string>() {"B3", "B4", "B5", "B6", "B7"}
        }
      };

      int shipNumber = new Random().Next(0, 2);


      foreach (List<string> shipList in possibleShips[shipNumber])
      {
        this.BotBoard.addShip(shipList);
      }
    }

    public bool playerBoardHasShips()
    {
      return this.PlayerBoard.getShips().Count != 0;
    }

    public bool botBoardHasShips()
    {
      return this.BotBoard.getShips().Count != 0;
    }

    public void StartGame()
    {
      this.InProgress = true;
    }

    public void FinishGame()
    {
      this.InProgress = false;
    }

    // Winner Getters & Setters
    public string getWinner()
    {
      return this.Winner;
    }

    public void setWinner(string winner)
    {
      this.Winner = winner;
    }

    // Player Getter
    public User getPlayer()
    {
      return this.Player;
    }

    // Devuelve el board del player
    public Board getPlayerBoard()
    {
      return this.PlayerBoard;
    }
  }
}