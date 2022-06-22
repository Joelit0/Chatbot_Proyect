namespace ChatBotProject
{
  public class Board
  {
    // Constantes
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

    // Getters

    public int getWidth()
    {
      return this.Width;
    }

    public int getHeight()
    {
      return this.Height;
    }

    public string getHeaderLetters()
    {
      return HeaderLetters;
    }

    public string[,] getFields()
    {
      return this.Fields;
    }

    // Lógica del tablero

    private void generateBoard()
    {
      // Llenar tablero de pocisiones vacías
      for(int row = 0; row < this.Height; row++){
        for(int col = 0; col < this.Width; col++){
          this.Fields[row, col] = "-";
        }
      }
    }

    public void updateBoard(string value, int row, int col)
    {
      this.Fields[row, col] = value;
    }

    public void showShips()
    {
      foreach(Ship ship in this.Ships)
      {
        foreach(string position in ship.getPositions())
        {
          var translatedPositions = translateToPositions(position);

          int row = translatedPositions[0];
          int col = translatedPositions[1];

          updateBoard("#", row, col);
        }
      }
    }

    public void hideShips()
    {
      foreach(Ship ship in this.Ships)
      {
        foreach(string position in ship.getPositions())
        {
          var translatedPositions = translateToPositions(position);

          int row = translatedPositions[0];
          int col = translatedPositions[1];

          updateBoard("-", row, col);
        }
      }
    }

    // Ship Lógica

    public void addShip(List<string> positions)
    {
      // Si el board no contiene este ship, se agrega
      Ship ship = new Ship(positions);

      if (shipIsValid(ship)) { this.Ships.Add(ship); }
    }

    public void removeShip(Ship ship)
    {
      // Si el board contiene el ship, se borra
      if (this.Ships.Contains(ship)) { this.Ships.Remove(ship); }
    }

    // Retorna todos los ships del tablero
    public List<Ship> getShips()
    {
      return this.Ships;
    }

    public bool shipIsValid(Ship ship)
    {
      int totalRows = this.Fields.GetLength(0);
      int totalCols = this.Fields.GetLength(1);

      List<int> shipRows = new List<int>();
      List<int> shipCols = new List<int>();

      foreach(Ship boardShip in this.Ships)
      {
        foreach(string position in boardShip.getPositions())
        {
          if(ship.getPositions().Contains(position)) { return false; }
        }
      }

      foreach(string position in ship.getPositions())
      {
        var translatedPositions = translateToPositions(position);
        
        int row = translatedPositions[0];
        int col = translatedPositions[1];

        // Si esta fuera del tablero
        if(row >= totalRows || col >= totalCols) { return false; }

        shipRows.Add(row);
        shipCols.Add(col);
      }

      // Ordenar ambas listas
      shipRows.Sort();
      shipCols.Sort();

      if (allElementsAreEqual(shipRows) && allElementsAreConsecutives(shipCols)) { return true; }

      if (allElementsAreEqual(shipCols) && allElementsAreConsecutives(shipRows)) { return true; }

      return false;
    }

    // Attack Lógica
  
    public void attack(string position)
    {
      var translatedPositions = translateToPositions(position);
      int row = translatedPositions[0];
      int col = translatedPositions[1];

      foreach(Ship ship in this.Ships)
      {
        foreach(string shipPosition in ship.getPositions())
        {
          if(position == shipPosition)
          {
            updateBoard("X", row, col);
            ship.removePosition(shipPosition);
            break;
          }
        }

        if (!ship.shipIsAlive()) {
          removeShip(ship);
          break;
        }
      }

      if (this.Fields[row, col] != "X") { updateBoard("O", row, col); }
    }

    // Métodos de ayuda para board, por esa razón son privados

    // Traducir las posiciones. Si recibe "A1" debería devolver [0,0]

    private List<int> translateToPositions(string position)
    {
      List<int> positions = new List<int>();
  
      char letter = Char.ToUpper(position[0]);
      string number = position.Substring(1);

      int row =  Int32.Parse(number) - 1;
      int col = HeaderLetters.IndexOf(letter);

      positions.Add(row);
      positions.Add(col);

      return positions;
    }

    // Verifica si todos los elementos de una lista son iguales

    private bool allElementsAreEqual(List<int> array)
    {
      int index = 0;

      for (index = 0; index < array.Count; index++)
      {
        if(array[0] == array[index])
        { continue; } else
        { break; }
      }
  
      return index == array.Count;
    }

    // Verifica si todos los elementos de una lista son consecutivos

    private bool allElementsAreConsecutives(List<int> array)
    {
      bool isConsecutive = true;

      for (int i = 1; i < array.Count; i++)
      {
        if (array[i] - 1 != array[i - 1])
        {
          isConsecutive = false;
          break;
        }
      }

      return isConsecutive;
    }
  }
}
