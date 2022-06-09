namespace ChatBotProject
{
  public class Timer 
  {
    // Minutes and Seconds
    public int Mins { get; set; }
    public int Secs { get; set; }

    public Timer(int mins, int secs)
    {
      this.Mins = mins;
      this.Secs = secs; 
    }

    public string StartTimer()
    {
      int timeLeft=this.Mins*60+this.Secs;
      if (timeLeft<=0)
      {
        return "Se ha acabado el tiempo";
      }
      else
      {
        timeLeft-=1;
        return $"Tiempo restante: {timeLeft}";
      }
    }
  }
}