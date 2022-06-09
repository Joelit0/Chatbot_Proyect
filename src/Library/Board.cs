namespace ChatBotProject
{
  public class Board
  {
    // Constants
    private const string HeaderLetters = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";

    // Variables
    private int Height;
    private int Width;
    private List<Ship> Ships;
    private string[,] Fields;

    // Constructor
    public Board(int height, int width)
    {
      this.Height = height;
      this.Width = width;
      this.Ships = new List<Ship>();
      this.Fields = new string[this.Height, this.Width];

      generateBoard();
    }

    // Board Logic

    private void generateBoard()
    {
      // Fill Board
      for(int row = 0; row < this.Height; row++){
        for(int col = 0; col < this.Width; col++){
          this.Fields[row, col] = "O";
        }
      }
    }

    public void printBoard()
    {
      Console.Write("   "); // Space between Cols & Rows

      // Print Cols Header
      for(int col = 0; col < this.Width; col++){
        Console.Write($"{HeaderLetters[col]} ");
      }

      Console.WriteLine(); // End of header
  
      // Print Board
      for(int row = 0; row < this.Height; row++){  
        Console.Write($"{row + 1}".PadRight(3)); // Print Rows Sidebar

        for(int col = 0; col < this.Width; col++){ 
          Console.Write($"{this.Fields[row, col]}".PadRight(2)); // !!!Replace ConsoleWriter for IPrinter!!!
        }

        Console.WriteLine(); // Idem
      }
    }

    public void updateBoard(string value, int row, int col)
    {
      this.Fields[row, col] = value;
    }

    // Ship Logic

    public void AddShip(Ship ship)
    {
      // If ships not contains the ship, add them
      if (!this.Ships.Contains(ship)) { this.Ships.Add(ship); }
    }

    public void RemoveShip(Ship ship)
    {
      // If ships contains the ship, delete them
      if (this.Ships.Contains(ship)) { this.Ships.Remove(ship); }
    }

    public List<Ship> GetShips()
    {
      return this.Ships;
    }

    // Attack Logic
  
    public void Attack(string position)
    {
      var translatedPositions = translateToPositions(position);
  
      int row = translatedPositions[0];
      int col = translatedPositions[1];

      updateBoard("-", row, col);
    }

    private List<int> translateToPositions(string position)
    {
      List<int> positions = new List<int>();
  
      char letter = position[0];
      string number = position.Substring(1);

      int col = HeaderLetters.IndexOf(letter);
      int row =  Int32.Parse(number) - 1;

      positions.Add(row);
      positions.Add(col);

      return positions;
    }
  }
}
