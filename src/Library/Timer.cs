using System;

namespace NavalBattle;
public class Timer 
{
    protected int mins; // Minutos.
    protected int secs; // Segundos.
    
    
    public int Mins {get; set;}
    public int Secs {get; set;}

    public Timer(int Mins, int Secs)
    {
        this.Mins = mins;
        this.Secs = secs; 
    }
}