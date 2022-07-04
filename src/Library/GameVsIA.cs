
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
      this.telegramPrinter = new TelegramPrinter();
    }

    public void printPlayerBoard()
    {
      this.PlayerBoard.showShips();

      this.telegramPrinter.printBoard(this.PlayerBoard, this.Player.ID);
    }

    public void AddShipToBoard(List<string> positions)
    {
      this.PlayerBoard.addShip(positions);
    }

    public void generateBotShips()
    {
      List<List<List<string>>> ships = new List<List<List<string>>>() {
        [
          ["J1", "J2"],
          ["H1", "H2", "H3"],
          ["H10", "G10", "I10", "J10"],
          ["B3", "B4", "B5", "B6", "B7"]
        ],
        [
          ["I1", "I2"],
          ["C4", "C5", "C6"],
          ["D10", "D9", "D8", "D7"],
          ["J2", "I2", "H2", "G2", "F2"]
        ],
        [
          ["F5", "F6"],
          ["A1", "A2", "A3"],
          ["A10", "B10", "C10", "D10"],
          ["J8", "I8", "H8", "G8", "F8"]
        ]
      };

      Random Rdn = new Random();
      int shipNumber = Rdn.next(0, 2);

      foreach (List<string> ship in ships[shipNumber])
      {
        Console.WriteLine(ship);
      }
    }

    // public void StartGame()
    // {
    //   Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
    //   Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
    //   string attackPosition;

      // while(firstUserBoard.getShips().Count != 0 && secondUserBoard.getShips().Count != 0)
      // {
      //   // Ataque del primer usuario
      //   Console.WriteLine("===============================================");
      //   Console.WriteLine($"Turno de {this.InMatchUsers[0].Name}");
      //   Console.WriteLine("===============================================");

      //   Console.WriteLine("Tu tablero:");
      //   firstUserBoard.showShips();
      //   telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[0].ID);
  
      //   Console.WriteLine($"Tablero de {this.InMatchUsers[1].Name}:");
      //   secondUserBoard.hideShips(); // Oculta los barcos
      //   telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[0].ID);

      //   // Leer ataque
      //   attackPosition = Console.ReadLine().ToUpper();
      //   secondUserBoard.attack(attackPosition);

      //   // Ataque del segundo usuario
      //   Console.WriteLine("===============================================");
      //   Console.WriteLine($"Turno de {this.InMatchUsers[1].Name}");
      //   Console.WriteLine("===============================================");

      //   Console.WriteLine("Tu tablero:");
      //   secondUserBoard.showShips();
      //   telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[1].ID);
  
      //   Console.WriteLine($"Tablero de {this.InMatchUsers[0].Name}:");
      //   firstUserBoard.hideShips(); // Oculta los barcos
      //   telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[1].ID);

      //   // Leer ataque
      //   attackPosition = Console.ReadLine().ToUpper();
      //   firstUserBoard.attack(attackPosition);
      // }
    // }

    private void FinishGame()
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