namespace ChatBotProject
{
  /// <summary>
  /// Esta clase Timer es un temporizador.
  /// </summary>
  public class Timer 
  {
    private int Mins;
    private int Secs;

    /// <summary>
    /// Constructor timer.
    /// </summary>
    /// <param name="mins">Recibe mins, de tipo int</param>
    /// <param name="secs">Recibe secs, de tipo int</param>
    public Timer(int mins, int secs)
    {
      this.Mins = mins;
      this.Secs = secs; 
    }

    /// <summary>
    /// Getter de los minutos.
    /// </summary>
    public int getMins()
    {
      return this.Mins;
    }

    /// <summary>
    /// Setter de los minutos.
    /// </summary>
    /// <param name="mins">Recibe mins, de tipo int</param>
    public void setMins(int mins)
    {
      this.Mins = mins;
    }

    /// <summary>
    /// Getter de los segundos.
    /// </summary>
    public int getSecs()
    {
      return this.Secs;
    }

    /// <summary>
    /// Setter de los segundos.
    /// </summary>
    /// <param name="secs">Recibe secs, de tipo int</param>
    public void setSecs(int secs)
    {
      this.Secs = secs;
    }
  }
}
