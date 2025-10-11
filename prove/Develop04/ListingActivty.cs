using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MindfulActivities
{
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new()
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
            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine($"\n--- {prompt} ---");
            Console.Write("\nGet ready...");
            ShowCountdown(5);
            Console.WriteLine("\nStart listing items (press Enter after each one):\n");

            List<string> responses = new();
            Stopwatch sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                    responses.Add(line.Trim());
            }

            Console.WriteLine($"\nYou listed {responses.Count} items!");
            EndActivity();
        }
    }
}
