using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo;

public static class ExperimentBuilder
{

    public static TaskSimulation CreateSpeedUp10Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(50, 40, 5);
        simulation.AddMeasurements(50, 20, 5);

        return simulation;
    }

    public static TaskSimulation CreateSlowDown10Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(50, 20, 5);
        simulation.AddMeasurements(50, 40, 5);

        return simulation;
    }

    public static TaskSimulation CreateSpeedUp100Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(50, 40, 50);
        simulation.AddMeasurements(50, 20, 50);

        return simulation;
    }

    public static TaskSimulation CreateSlowDown100Measures()
    {
        var simulation = new TaskSimulation();

        simulation.AddMeasurements(50, 20, 50);
        simulation.AddMeasurements(50, 40, 50);

        return simulation;
    }
}
