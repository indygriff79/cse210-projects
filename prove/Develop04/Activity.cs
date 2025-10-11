using System;
using System.Diagnostics;
using System.Threading;

namespace MindfulActivities
{
    public abstract class Activity
    {
        private string _name;
        private string _description;
        private int _durationSeconds;

        protected Random _random = new Random();

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void SetDuration(int seconds)
        {
            if (seconds < 1)
                throw new ArgumentException("Duration must be at least 1 second.");
            _durationSeconds = seconds;
        }

        protected int DurationSeconds => _durationSeconds;

        public void StartActivity()
        {
            Console.Clear();
            Console.WriteLine($"=== {_name} ===\n");
            Console.WriteLine(_description);
            Console.Write("\nEnter duration in seconds: ");
            if (int.TryParse(Console.ReadLine(), out int sec))
                SetDuration(sec);
            else
                SetDuration(30);

            Console.WriteLine("\nGet ready...");
            ShowSpinner(3);
        }

        public void EndActivity()
        {
            Console.WriteLine("\nWell done!");
            ShowSpinner(2);
            Console.WriteLine($"\nYou have completed the activity for {DurationSeconds} seconds.");
            ShowSpinner(3);
            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        public abstract void RunActivity();

        protected void ShowSpinner(int seconds)
        {
            char[] spinner = { '|', '/', '-', '\\' };
            Stopwatch sw = Stopwatch.StartNew();
            int i = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[i++ % spinner.Length]);
                Thread.Sleep(150);
                Console.Write('\b');
            }
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
    }
}
