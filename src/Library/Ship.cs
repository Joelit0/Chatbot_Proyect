namespace ChatBotProject
{
  public class Ship
  {
    protected int Score;
    protected int Large;
    private bool IsAlive;
    private List<string> Positions;

    public Ship(List<string> positions)
    {
      this.Large = positions.Count();
      this.Score = 20 - this.Large;
      this.IsAlive = checkIsAlive();
      this.Positions = positions;
    }

    // Score Getter.
    public int getScore()
    {
      return this.Score;
    }

    // Large Getter
    public int getLarge()
    {
      return this.Large;
    }

    // Large Getter
    public bool shipIsAlive()
    {
      return this.IsAlive;
    }
  
    // Positions Getter
    public List<string> getPositions()
    {
      return this.Positions;
    }

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

    // Verificar si el ship está vivo. Está vivo solo si su largo es mayor que 0
    public bool checkIsAlive()
    {
      return this.Large > 0;
    }

    
  }
  
  [Serializable]
  public class PositionDoesNotExistException : Exception 
  { 
  public PositionDoesNotExistException(string message) : base(message) { }
  }
}
