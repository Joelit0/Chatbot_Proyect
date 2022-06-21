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
  }
}