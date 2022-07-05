
namespace ChatBotProject
{
  public class GameVsIA
  {
    private User Player;
    private bool InProgress;
    private string Winner;
    private Board PlayerBoard;
    private Board BotBoard;
    private List<string> BotAtacks;
    private TelegramPrinter telegramPrinter;

    public GameVsIA(User player)
    {
      this.Player = player;
      this.InProgress = false;
      this.PlayerBoard = new Board(10, 10);
      this.BotBoard = new Board(10, 10);
      this.telegramPrinter = new TelegramPrinter();
      this.BotAtacks = new List<string>() {};

      this.PlayerBoard.showShips();
      this.BotBoard.hideShips();
    }

    public void printPlayerBoard()
    {
      this.telegramPrinter.printBoard(this.PlayerBoard, this.Player.ID);
    }

    public void printBotBoard()
    {
      this.telegramPrinter.printBoard(this.BotBoard, this.Player.ID);
    }
    
    public void botAttack()
    {
      bool invalidBotAttack = true;
      string attackPosition = "";

      while(invalidBotAttack)
      {
        string headerLetters = this.PlayerBoard.getHeaderLetters().Substring(0, 9);
        int randomLetterIndex = new Random().Next(0, 9);
        int randomRowIndex= new Random().Next(1, 10);
        attackPosition = $"{headerLetters[randomLetterIndex]}{randomRowIndex}";

        invalidBotAttack = this.BotAtacks.Contains(attackPosition);
      }

      this.BotAtacks.Add(attackPosition);
  
      this.PlayerBoard.attack(attackPosition);
    }

    public void attackBotBoard(string attackPosition)
    {
      this.BotBoard.attack(attackPosition);
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
          new List<string>() {"I1", "I2"},
          new List<string>() {"C4", "C5", "C6"},
          new List<string>() {"D10", "D9", "D8", "D7"},
          new List<string>() {"J2", "I2", "H2", "G2", "F2"}
        },
        new List<List<string>>() {
          new List<string>() {"F5", "F6"},
          new List<string>() {"A1", "A2", "A3"},
          new List<string>() {"A10", "B10", "C10", "D10"},
          new List<string>() {"J8", "I8", "H8", "G8", "F8"}
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
      return this.PlayerBoard.getShips().Count > 0;
    }

    public bool botBoardHasShips()
    {
      return this.BotBoard.getShips().Count > 0;
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