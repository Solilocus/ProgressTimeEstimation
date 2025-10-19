using ConsolePlot;
using ProgressTimeEstimation;

namespace Demo;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;  // Required by ConsolePlot

        await PerformExperiment1();
        await PerformExperiment2();
        await PerformExperiment3();
        await PerformExperiment4();
    }

    static async Task PerformExperiment1()
    {
        TaskSimulation experimentSpeedUp10 = ExperimentBuilder.CreateSpeedUp10Measures();

        PlotExperiment(experimentSpeedUp10);

        Console.WriteLine("Testing slow start, then speed up. 10 measures.");
        Console.WriteLine($"Elapsed seconds | Progress % | Real remaining seconds | Estimated remaining seconds | error in seconds");

        var maxProcessTime = TimeSpan.FromSeconds(experimentSpeedUp10.TotalSteps * 6);
        var estimator = new RemainingTimeEstimator(experimentSpeedUp10.TotalSteps, maxProcessTime);
        await SimulateExperiment(experimentSpeedUp10, estimator);
    }

    static async Task PerformExperiment2()
    {
        TaskSimulation experimentSlowDown10 = ExperimentBuilder.CreateSlowDown10Measures();

        PlotExperiment(experimentSlowDown10);

        Console.WriteLine("Testing fast start, then slow down. 10 measures.");
        Console.WriteLine($"Elapsed seconds | Progress % | Real remaining seconds | Estimated remaining seconds | error in seconds");

        var maxProcessTime = TimeSpan.FromSeconds(experimentSlowDown10.TotalSteps * 6);
        var estimator = new RemainingTimeEstimator(experimentSlowDown10.TotalSteps, maxProcessTime);
        await SimulateExperiment(experimentSlowDown10, estimator);
    }

    static async Task PerformExperiment3()
    {
        TaskSimulation experimentSpeedUp100 = ExperimentBuilder.CreateSpeedUp100Measures();

        PlotExperiment(experimentSpeedUp100);

        Console.WriteLine("Testing fast start, then slow down. 10 measures.");
        Console.WriteLine($"Elapsed seconds | Progress % | Real remaining seconds | Estimated remaining seconds | error in seconds");

        var estimator = new RemainingTimeEstimator(experimentSpeedUp100.TotalSteps);
        await SimulateExperiment(experimentSpeedUp100, estimator);
    }

    static async Task PerformExperiment4()
    {
        TaskSimulation experimentSlowDown100 = ExperimentBuilder.CreateSlowDown100Measures();

        PlotExperiment(experimentSlowDown100);

        Console.WriteLine("Testing fast start, then slow down. 10 measures.");
        Console.WriteLine($"Elapsed seconds | Progress % | Real remaining seconds | Estimated remaining seconds | error in seconds");

        var estimator = new RemainingTimeEstimator(experimentSlowDown100.TotalSteps);
        await SimulateExperiment(experimentSlowDown100, estimator);
    }

    static async Task SimulateExperiment(TaskSimulation experiment, RemainingTimeEstimator estimator)
    {
        DateTime startUtc = DateTime.UtcNow;
        double totalSeconds = experiment.TotalDurationSeconds;
        double previousElapsedSeconds = 0;
        estimator.Start();
        foreach (var measure in experiment.Measurements)
        {
            double processSeconds = measure.Seconds - previousElapsedSeconds;
            if (processSeconds > 0)
            { await Task.Delay((int)(processSeconds * 1000)); }

            var estimatedRemainingTime = estimator.Update(measure.Steps);
            double realRemainingSeconds = totalSeconds - measure.Seconds;
            double errorSeconds = realRemainingSeconds - estimatedRemainingTime.TotalSeconds;
            var elapsedSeconds = (DateTime.UtcNow - startUtc).TotalSeconds;

            Console.WriteLine($"{elapsedSeconds:F3} | {measure.Steps:F3}% | {realRemainingSeconds:F3} | {estimatedRemainingTime.TotalSeconds:F3} | {errorSeconds:F3}");

            previousElapsedSeconds = elapsedSeconds;
        }
    }

    static void PlotExperiment(TaskSimulation experiment)
    {
        double[] xs = experiment.Measurements.Select(x => x.Seconds).ToArray();
        double[] ys = experiment.Measurements.Select(x => x.Steps).ToArray();

        Plot plt = new Plot(80, 22);
        plt.AddSeries(xs, ys);
        plt.Draw();
        plt.Render();
    }
}
