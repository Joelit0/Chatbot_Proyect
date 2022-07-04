namespace ChatBotProject
{
  /// <summary>
  /// Esta clase es la del barco.
  /// Cada barco tiene un puntuaje (Score), largo (Large), el booleano para ver si está vivo o hundido(IsAlive) y sus posiciones (Positions).
  /// </summary>
  public class Ship
  {
    protected int Score;
    protected int Large;
    private bool IsAlive;
    private List<string> Positions;

    /// <summary>
    /// Este es el constructor del barco.
    /// </summary>
    /// <param name="positions">Tiene las posiciones del barco.</param>
    public Ship(List<string> positions)
    {
      this.Large = positions.Count();
      this.Score = 20 - this.Large;
      this.IsAlive = checkIsAlive();
      this.Positions = positions;
    }

    /// <summary>
    /// Getter de Score.
    /// </summary>
    /// <returns></returns>
    public int getScore()
    {
      return this.Score;
    }

    /// <summary>
    /// Getter de Large.
    /// </summary>
    /// <returns></returns>
    public int getLarge()
    {
      return this.Large;
    }

    /// <summary>
    ///shipIsAlive se fija si el barco está vivo o hundido.
    /// </summary>
    /// <returns></returns>
    public bool shipIsAlive()
    {
      return this.IsAlive;
    }

    public List<string> getPositions()
    {
      return this.Positions;
    }

    /// <summary>
    /// El método removePosition busca la posición dada por parámetro y la remueve.
    /// Chequea si se hundió el barco o no (checkIsAlive)
    /// </summary>
    /// <param name="position"></param>
    public void removePosition(string position)
    {
      if(this.Positions.Contains(position))
      {
        this.Positions.Remove(position);
        this.Large -= 1;
        this.IsAlive = checkIsAlive();
      }
      else
      {
        throw new PositionDoesNotExistException("Position does not exist");
      }
    }

    /// <summary>
    /// checkIsAlive verifica si el ship está vivo. Está vivo solo si su largo es mayor que 0.
    /// </summary>
    /// <returns></returns>
    public bool checkIsAlive()
    {
      return this.Large > 0;
    }

    
  }
  
  //Excepción para cuando no existe la posisción pasada al metodo removePosition
  [Serializable]
  public class PositionDoesNotExistException : Exception 
  { 
  public PositionDoesNotExistException(string message) : base(message) { }
  }
}
