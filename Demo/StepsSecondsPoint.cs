using System;

namespace Demo;

/// <summary>
/// A measurement point. Number of steps processed at a given elapsed time in seconds.
/// </summary>
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
