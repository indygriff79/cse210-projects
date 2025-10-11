using System;
using System.Diagnostics;

namespace MindfulActivities
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base("Breathing Activity",
                   "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        public override void RunActivity()
        {
            StartActivity();
            Stopwatch total = Stopwatch.StartNew();
            Console.WriteLine();

            int stepSeconds = 4;
            bool inhale = true;

            while (total.Elapsed.TotalSeconds < DurationSeconds)
            {
                Console.Write(inhale ? "Breathe in... " : "Breathe out... ");
                int remaining = DurationSeconds - (int)total.Elapsed.TotalSeconds;
                int countdown = Math.Min(stepSeconds, Math.Max(1, remaining));
                ShowCountdown(countdown);
                Console.WriteLine();
                inhale = !inhale;
            }

            total.Stop();
            EndActivity();
        }
    }
}
