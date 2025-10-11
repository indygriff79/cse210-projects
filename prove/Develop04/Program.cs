using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MindfulActivities
{
    // Base class for all activities
    public abstract class Activity
    {
        // Encapsulated fields
        private string _name;
        private string _description;
        private int _durationSeconds;

        protected Random _random = new Random();

        // Constructor
        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _durationSeconds = 0;
        }

        // Public setter for duration (validated)
        public void SetDuration(int seconds)
        {
            if (seconds < 1)
                throw new ArgumentException("Duration must be at least 1 second.");
            _durationSeconds = seconds;
        }

        // Public getter used by derived classes
        protected int DurationSeconds => _durationSeconds;

        // Common start message
        public void StartActivity()
        {
            Console.Clear();
            Console.WriteLine($"=== {_name} ===");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
            Console.Write("Enter duration in seconds: ");
            // Duration may already be set by caller; if not, read.
            string input = Console.ReadLine();
            if (int.TryParse(input, out int sec))
            {
                SetDuration(sec);
            }
            else if (_durationSeconds < 1)
            {
                // if no valid input and not already set, ask again
                while (!int.TryParse(input, out sec) || sec < 1)
                {
                    Console.Write("Please enter a valid number of seconds: ");
                    input = Console.ReadLine();
                }
                SetDuration(sec);
            }

            Console.WriteLine();
            Console.WriteLine("Get ready...");
            // small prep pause with spinner
            ShowSpinner(3);
        }

        // Common end message
        public void EndActivity()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(2);
            Console.WriteLine();
            Console.WriteLine($"You have completed the activity for {DurationSeconds} seconds.");
            ShowSpinner(3);
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu...");
            Console.ReadLine();
        }

        // Virtual run method to be overridden by derived classes
        public abstract void RunActivity();

        // Helper: show a rotating spinner for 'seconds'
        protected void ShowSpinner(int seconds)
        {
            char[] spinner = new char[] { '|', '/', '-', '\\' };
            Stopwatch sw = Stopwatch.StartNew();
            int idx = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[idx++ % spinner.Length]);
                Thread.Sleep(150);
                Console.Write('\b');
            }
            sw.Stop();
        }

        // Helper: countdown from seconds down to 1 (prints numbers)
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b"); // erase
            }
        }

        // Helper: pause with spinner but also return early if duration expired (used by derived classes)
        protected void PauseWithSpinner(int seconds)
        {
            ShowSpinner(seconds);
        }
    }

    // Breathing activity
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base("Breathing Activity",
                   "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        public override void RunActivity()
        {
            // parent StartActivity will ask for duration
            StartActivity();

            Stopwatch total = Stopwatch.StartNew();
            Console.WriteLine();

            // We'll alternate "Breathe in..." and "Breathe out..." and perform a countdown each time.
            // For smoothness, pick 4 seconds for each inhale/exhale (but it uses DurationSeconds to limit).
            int stepSeconds = 4;
            bool inhale = true;

            while (total.Elapsed.TotalSeconds < DurationSeconds)
            {
                if (inhale)
                {
                    Console.Write("Breathe in... ");
                }
                else
                {
                    Console.Write("Breathe out... ");
                }

                // countdown for this breathe step, but don't exceed remaining time
                int remaining = DurationSeconds - (int)total.Elapsed.TotalSeconds;
                int countdownSeconds = Math.Min(stepSeconds, Math.Max(1, remaining));
                ShowCountdown(countdownSeconds);
                Console.WriteLine();
                inhale = !inhale;
            }

            total.Stop();
            EndActivity();
        }
    }

    // Reflection activity
    public class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity()
            : base("Reflection Activity",
                   "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        { }

        public override void RunActivity()
        {
            StartActivity();

            // Show a random prompt
            string prompt = GetRandomPrompt();
            Console.WriteLine();
            Console.WriteLine("Prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();

            Console.WriteLine("When you are ready, reflect on the following questions.");
            Console.WriteLine();

            Stopwatch total = Stopwatch.StartNew();

            // Pause briefly before starting questions
            ShowSpinner(2);

            while (total.Elapsed.TotalSeconds < DurationSeconds)
            {
                string question = GetRandomQuestion();
                Console.WriteLine(">> " + question);
                // Pause for a few seconds while showing spinner (e.g., 5 seconds or remaining time)
                int remaining = DurationSeconds - (int)total.Elapsed.TotalSeconds;
                int pause = Math.Min(5, Math.Max(1, remaining));
                ShowSpinner(pause);
                Console.WriteLine();
            }

            total.Stop();
            EndActivity();
        }

        private string GetRandomPrompt()
        {
            int idx = _random.Next(_prompts.Count);
            return _prompts[idx];
        }

        private string GetRandomQuestion()
        {
            int idx = _random.Next(_questions.Count);
            return _questions[idx];
        }
    }

    // Listing activity
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
            : base("Listing Activity",
                   "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }

        public override void RunActivity()
        {
            StartActivity();

            string prompt = GetRandomPrompt();
            Console.WriteLine();
            Console.WriteLine("List Prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();

            Console.WriteLine("You will have a few seconds to think, then start listing items. Press Enter after each item.");
            Console.Write("Get ready...");
            ShowCountdown(5);
            Console.WriteLine();
            Console.WriteLine("Start listing now:");

            List<string> responses = new List<string>();

            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
        
                // Inform user of remaining time
                int remaining = DurationSeconds - (int)sw.Elapsed.TotalSeconds;
                Console.Write($"({remaining}s left) > ");
                string line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    responses.Add(line.Trim());
                }
            }
            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"You listed {responses.Count} item(s).");
            EndActivity();
        }

        private string GetRandomPrompt()
        {
            return _prompts[_random.Next(_prompts.Count)];
        }
    }

    // Program (menu controller)
    class Program
    {
        static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>()
            {
                new BreathingActivity(),
                new ReflectionActivity(),
                new ListingActivity()
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindful Activities");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Select an option (1-4): ");

                string choice = Console.ReadLine();
                if (choice == "4" || choice?.ToLower() == "q")
                {
                    Console.WriteLine("Goodbye!");
                    Thread.Sleep(800);
                    break;
                }

                Activity selected = null;
                switch (choice)
                {
                    case "1":
                        selected = activities[0];
                        break;
                    case "2":
                        selected = activities[1];
                        break;
                    case "3":
                        selected = activities[2];
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option (1-4).");
                        Thread.Sleep(1000);
                        continue;
                }

                // Run the selected activity. Each activity will ask for duration as part of StartActivity.
                try
                {
                    selected.RunActivity();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
        }
    }
}
