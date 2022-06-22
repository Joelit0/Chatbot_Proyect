namespace ChatBotProject
{
  public class Timer 
  {
    /// <summary>
    /// Esta clase Timer es un temporizador.
    /// </summary>
    /// <value></value>
    public int Mins { get; set; }
    public int Secs { get; set; }

    /// <summary>
    /// Constructor timer.
    /// </summary>
    /// <param name="mins">Se pasan los minutos</param>
    /// <param name="secs">Se pasan los segundos</param>
    public Timer(int mins, int secs)
    {
      this.Mins = mins;
      this.Secs = secs; 
    }
  }
}