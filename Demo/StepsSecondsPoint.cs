using System;

namespace Demo;

public record StepsSecondsPoint
{
    public double Steps { get; }
    public double Seconds { get; }

    public StepsSecondsPoint(double steps, double seconds) 
    {
        Steps = steps;
        Seconds = seconds;
    }
}
