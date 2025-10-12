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

        public int MaxSteps { get; protected set; }

        public TimeSpan MaxProcessTime { get; protected set; }

        public int ProcessedSteps { get; protected set; }

        public TimeSpan RemainingTime { get; protected set; }

        /// <summary>
        /// Current estimated speed in steps per second.
        /// </summary>
        public double CurrentSpeed { get; protected set; }

        public DateTime StartTimeUtc { get; protected set; }

        public RemainingTimeEstimator(int maxSteps, TimeSpan? maxProcessTime = null)
        {
            MaxSteps = maxSteps;

            if (maxProcessTime.HasValue)
            {
                MaxProcessTime = maxProcessTime.Value;
            }
            else
            {
                MaxProcessTime = TimeSpan.FromDays(DefaultMaxProcessDays);
            }
        }

        public void Start()
        {
            ProcessedSteps = 0;
            RemainingTime = MaxProcessTime;
            StartTimeUtc = DateTime.UtcNow;
            CurrentSpeed = MaxSteps / MaxProcessTime.TotalSeconds;
        }

        public TimeSpan Update(int processedSteps)
        {
            TimeSpan elapsedTime = DateTime.UtcNow - StartTimeUtc;

            // TODO: Update current speed

            return TimeSpan.FromSeconds((MaxSteps - processedSteps) / CurrentSpeed);
        }
    }
}