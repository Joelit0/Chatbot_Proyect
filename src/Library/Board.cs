namespace ChatBotProject
{
  /// <summary>
  /// La clase Board se encarga de crear el tablero y de todos los movimientos del mismo.
  /// </summary>
  public class Board
  {
    // Constantes
    private const string HeaderLetters = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";

    // Variables
    private int Height;
    private int Width;
    private List<Ship> Ships;
    private string[,] Fields;

    /// <summary>
    /// Propiedad utilizada para mantener un registro de los ataques que han
    /// impactado en barcos en este tablero.
    /// </summary>
    /// <value></value>
    public int MatchShipHits { get; set;} = 0;
    /// <summary>
    /// Propiedad utilizada para mantener un registro de los ataques que han
    /// impactado en agua en este tablero.
    /// </summary>
    /// <value></value>
    public int MatchWaterHits { get; set;} = 0;

    /// <summary>
    /// Este es el constructor de Board.
    /// Contiene la lista de barcos (List<Ship>) y los campos(Fiels).
    /// Aquí también se llama al método generateBoard(), que nos crea un tablero con posiciones vacías.
    /// </summary>
    /// <param name="height">Es la altura del tablero.</param>
    /// <param name="width">Es el ancho del tablero.</param>
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

    /// <summary>
    /// El método generateBoard crea un tablero con posiciones vacías.
    /// row es una variable equivalente a las filas del tablero.
    /// col es una variable equivalente a las columnas del tablero.
    /// Se ponen guiones en todo el tablero vacio, respetando la altura y el ancho previamente pasado.
    /// </summary>
    private void generateBoard()
    {
      // Llenar tablero de pocisiones vacías
      for(int row = 0; row < this.Height; row++){
        for(int col = 0; col < this.Width; col++){
          this.Fields[row, col] = "-";
        }
      }
    }

    /// <summary>
    /// Este método actualiza el tablero.
    /// </summary>
    /// <param name="value">Este parámetro tiene el valor que se quiere cambiar en el tablero</param>
    /// <param name="row">Las filas del tablero</param>
    /// <param name="col">Las columnas del tablero</param>
    public void updateBoard(string value, int row, int col)
    {
      this.Fields[row, col] = value;
    }
    
    /// <summary>
    /// En este método se muestran los barcos.
    /// Los barcos se representan con un simbolo "#".
    /// </summary>
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

    /// <summary>
    /// No siempre se mostrarán los barcos en la partida, para esto está el método hideShips que cambia un barco que se está mostrando como "#" a "-"-
    /// </summary>
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
    /// <summary>
    /// Se agrega el ship pasado como parámetro al tablero.
    /// </summary>
    /// <param name="positions">Posiciones para colocar el barco</param>
    public void addShip(List<string> positions)
    {
      // Si el board no contiene este ship, se agrega
      Ship ship = new Ship(positions);

      if (shipIsValid(ship)) { this.Ships.Add(ship); }
    }

    /// <summary>
    /// Este método ve si el tablero contiene el barco que deseamos eliminar, y lo borra.
    /// </summary>
    /// <param name="ship">El barco que queremos eliminar.</param>
    public void removeShip(Ship ship)
    {
      // Si el board contiene el ship, se borra
      if (this.Ships.Contains(ship)) { this.Ships.Remove(ship); }
    }

    /// <summary>
    /// Acá se retorna todos los ships del tablero.
    /// </summary>
    /// <returns></returns>
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

    
    /// <summary>
    /// Lógica del ataque.
    /// </summary>
    /// <param name="position">Posición a atacar.</param>
    public void attack(string position)
    {
      var translatedPositions = translateToPositions(position);
      int row = translatedPositions[0];
      int col = translatedPositions[1];

      foreach(Ship ship in this.Ships)
      {
        foreach(string shipPosition in ship.getPositions())
        {
          if(position.ToUpper() == shipPosition)
          {
            updateBoard("X", row, col);
            this.MatchShipHits += 1;
            ship.removePosition(shipPosition);
            break;
          }
        }

        if (!ship.shipIsAlive()) {
          removeShip(ship);
          break;
        }
      }

      if (this.Fields[row, col] != "X") 
      { 
        updateBoard("O", row, col); 
        this.MatchWaterHits += 1;
      }
    }

    // Métodos de ayuda para board, por esa razón son privados
    
    /// <summary>
    /// Este método traduce las posiciones, si recibe "A1" debería delvoer [0.0]
    /// </summary>
    /// <param name="position">Se pasa la posición que se busca traducir</param>
    /// <returns></returns>
    private List<int> translateToPositions(string position)
    {
      List<int> positions = new List<int>();
  
      char letter = Char.ToUpper(position[0]);
      string number = position.Substring(1);

      int row = Int32.Parse(number) - 1;
      int col = HeaderLetters.IndexOf(letter);

      positions.Add(row);
      positions.Add(col);

      return positions;
    }

    /// <summary>
    /// Verifica si todos los elementos de una lista son iguales.
    /// </summary>
    /// <param name="array">La lista para verificar.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Verifica si todos los elementos de una lista son consecutivos. Retorna un booleano, si son consecutivos o no.
    /// </summary>
    /// <param name="array">La lista a verificar.</param>
    /// <returns></returns>
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
