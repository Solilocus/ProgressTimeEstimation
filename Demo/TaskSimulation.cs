using System;

namespace Demo;

/// <summary>
/// Simulate a task progression. Contains a list of measure points with the steps processed and the elapsed time.
/// </summary>
public class TaskSimulation
{
    public double TotalDurationSeconds
    {
        get
        {
            if (Measurements is null || Measurements.Count == 0)
            {  return 0; }

            return Measurements.Last().Seconds;
        }
    }

    public double TotalSteps
    {
        get
        {
            if (Measurements is null || Measurements.Count == 0)
            { return 0; }

            return Measurements.Last().Steps;
        }
    }

    public List<StepsSecondsPoint> Measurements { get; private set; }

    public TaskSimulation()
    {
        Measurements = new List<StepsSecondsPoint>();
    }

    /// <summary>
    /// Add a linear section to the task.
    /// </summary>
    /// <param name="deltaSteps">Number of steps processed during this section.</param>
    /// <param name="durationSeconds">Duration of the section.</param>
    /// <param name="measurementCount">Number of measurements along the segment.</param>
    /// <exception cref="ArgumentException">Must have at least 1 measurement.</exception>
    public void AddMeasurements(double deltaSteps, double durationSeconds, uint measurementCount)
    {
        if (measurementCount == 0)
        { throw new ArgumentException("Cannot add measurements. Count must be greater than zero."); }

        double atomicSteps = deltaSteps / measurementCount;
        double atomicDuration = durationSeconds / measurementCount;

        StepsSecondsPoint lastMeasurement = Measurements.LastOrDefault() ?? new StepsSecondsPoint(0, 0);

        for (int i = 1; i <= measurementCount; i++)
        {
            Measurements.Add(new StepsSecondsPoint
                                    (
                                        lastMeasurement.Steps + (atomicSteps * i),
                                        lastMeasurement.Seconds + (atomicDuration * i)
                                    ));
        }
    }
}
