using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressTimeEstimation
{
    public class RemainingTimeEstimator
    {
        /// <summary>
        /// When max process time is not specified, this number of days will be used.
        /// It is the maximum time a process can take and also the initial value for the estimated remaining time.
        /// </summary>
        public const int DefaultMaxProcessDays = 7;

        /// <summary>
        /// A step is a metric measuring the advancement of the task.
        /// It can be the total number of files to copy, the number of meters to reach a destination, etc.
        /// </summary>
        public double TotalSteps { get; protected set; }

        /// <summary>
        /// The current steps the task has already performed.
        /// </summary>
        public double ProcessedSteps { get; protected set; }

        public TimeSpan MaxProcessTime { get; protected set; }

        public TimeSpan RemainingTime { get; protected set; }

        /// <summary>
        /// Current estimated speed in steps per second.
        /// </summary>
        public double CurrentSpeed { get; protected set; }

        public DateTime StartTimeUtc { get; protected set; }

        public double ProcessedPercent
        {
            get
            {
                if (TotalSteps == 0)
                { return 0; }

                return (ProcessedSteps / TotalSteps) * 100.0;
            }
        }

        public RemainingTimeEstimator(double totalSteps, TimeSpan? maxProcessTime = null)
        {
            if (totalSteps <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalSteps), "Total steps must be bigger than zero.");
            }

            TotalSteps = totalSteps;

            if (maxProcessTime.HasValue)
            {
                MaxProcessTime = maxProcessTime.Value.Duration();  // Use duration in case a negative maxProcessTime is provided.
            }
            else
            {
                MaxProcessTime = TimeSpan.FromDays(DefaultMaxProcessDays);
            }
        }

        /// <summary>
        /// Use this method when the task is started.
        /// Can also be used to reset the estimator.
        /// </summary>
        public void Start()
        {
            ProcessedSteps = 0;
            RemainingTime = MaxProcessTime;
            StartTimeUtc = DateTime.UtcNow;
            CurrentSpeed = TotalSteps / MaxProcessTime.TotalSeconds;
        }

        public TimeSpan Update(double processedSteps)
        {
            TimeSpan elapsedTime = DateTime.UtcNow - StartTimeUtc;
            double elapsedSeconds = elapsedTime.TotalSeconds;

            if (elapsedSeconds == 0 || processedSteps <= 0)
            {
                RemainingTime = TimeSpan.FromSeconds(TotalSteps / CurrentSpeed);
                return RemainingTime;
            }

            double averageSpeed = processedSteps / elapsedSeconds;
            double deltaSpeed = averageSpeed - CurrentSpeed;

            // This regulate how fast the estimated speed approach the average speed.
            // In the beginning the speed should change slowly, then accelerate toward the end.
            double proportionalGain = Math.Min(Math.Pow(processedSteps / TotalSteps, 4), 1.0);

            if (deltaSpeed > 0)
            {
                CurrentSpeed += deltaSpeed * proportionalGain;  // Increase the speed toward the measured average.
            }
            else
            {
                // The current estimated speed is higher than the measured average. We need to decelerate.
                // The slowest speed we can reach is the one where the remaining steps are reached in the estimated remaining time.
                // A speed slower than this value would make the remaining time increase, which would be jarring for the user.
                CurrentSpeed = (TotalSteps - processedSteps) / RemainingTime.TotalSeconds;
            }

            RemainingTime = TimeSpan.FromSeconds((TotalSteps - processedSteps) / CurrentSpeed);
            return RemainingTime;
        }
    }
}