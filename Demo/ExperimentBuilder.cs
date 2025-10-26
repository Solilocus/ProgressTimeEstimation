using System;

namespace Demo;

/// <summary>
/// Create various task behaviours for our experiments.
/// </summary>
public static class ExperimentBuilder
{
    // All the simulation are build to last 60 seconds and have a total of 100 steps.

    public static TaskSimulation CreateSpeedUp10Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 40, measurementCount: 5);
        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 20, measurementCount: 5);

        return simulation;
    }

    public static TaskSimulation CreateSlowDown10Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 20, measurementCount: 5);
        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 40, measurementCount: 5);

        return simulation;
    }

    public static TaskSimulation CreateSpeedUp100Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 40, measurementCount: 50);
        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 20, measurementCount: 50);

        return simulation;
    }

    public static TaskSimulation CreateSlowDown100Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 20, measurementCount: 50);
        simulation.AddMeasurements(deltaSteps: 50, durationSeconds: 40, measurementCount: 50);

        return simulation;
    }

    public static TaskSimulation CreateConstantWithStops100Measures()
    {
        var simulation = new TaskSimulation();
        simulation.AddMeasurements(deltaSteps: 20,durationSeconds: 8,measurementCount: 19);
        simulation.AddMeasurements(deltaSteps: 0, durationSeconds: 5, measurementCount: 1);  // Stop
        simulation.AddMeasurements(deltaSteps: 20, durationSeconds: 8, measurementCount: 19);
        simulation.AddMeasurements(deltaSteps: 0, durationSeconds: 5, measurementCount: 1);  // Stop
        simulation.AddMeasurements(deltaSteps: 20, durationSeconds: 8, measurementCount: 19);
        simulation.AddMeasurements(deltaSteps: 0, durationSeconds: 5, measurementCount: 1);  // Stop
        simulation.AddMeasurements(deltaSteps: 20, durationSeconds: 8, measurementCount: 19);
        simulation.AddMeasurements(deltaSteps: 0, durationSeconds: 5, measurementCount: 1);  // Stop
        simulation.AddMeasurements(deltaSteps: 20, durationSeconds: 8, measurementCount: 19);
        return simulation;
    }
}
