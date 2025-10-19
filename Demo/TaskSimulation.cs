using System;

namespace Demo;

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
