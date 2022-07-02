namespace ChatBotProject
{
  /// <summary>
  /// Esta clase Timer es un temporizador.
  /// </summary>
  public class Timer
  {
    private int Mins;
    private int Secs;
    private System.Timers.Timer timerClock;

    /// <summary>
    /// Constructor timer.
    /// </summary>
    /// <param name="mins">Recibe mins, de tipo int</param>
    /// <param name="secs">Recibe secs, de tipo int</param>
    public Timer(int mins, int secs)
    {
      this.Mins = mins;
      this.Secs = secs;
      this.timerClock = new System.Timers.Timer();
      this.timerClock.Elapsed += OnTimedEvent;
      this.timerClock.AutoReset = false;
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

    /// <summary>
    /// Se encarga de comenzar el timer. Setea el tiempo basado en los atributos minutos y segundos, luego habilita el timer.
    /// </summary>
    public void startTimer()
    {
      // Duración del timer
      this.timerClock.Interval = ((this.Mins * 60) + this.Secs) * 1000;

      // Habilitar el timer
      this.timerClock.Enabled = true;
    }

    /// <summary>
    /// Se encarga de detener el timer. Es el proceso inverso de habilitar el timer. En vez de cargar true en Eneabled, carga false
    /// </summary>
    public void stopTimer()
    {
      // Deshabilitar el timer
      this.timerClock.Enabled = false;
    }

    /// <summary>
    /// Este método es el encargado de manejar la interrupción del timer. Es decir, cuando el timer termine "caerá" en este método
    /// </summary>
    private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
    }
  }
}
