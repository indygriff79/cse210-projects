using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MindfulActivities
{
    public class ReflectionActivity : Activity
    {
        private List<string> _prompts = new()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times?",
            "What is your favorite thing about this experience?",
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
            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine($"\n--- {prompt} ---\n");
            Console.WriteLine("When you are ready, reflect on these questions:\n");
            ShowSpinner(2);

            Stopwatch total = Stopwatch.StartNew();

            while (total.Elapsed.TotalSeconds < DurationSeconds)
            {
                string question = _questions[_random.Next(_questions.Count)];
                Console.WriteLine($"> {question}");
                int remaining = DurationSeconds - (int)total.Elapsed.TotalSeconds;
                int pause = Math.Min(5, Math.Max(1, remaining));
                ShowSpinner(pause);
                Console.WriteLine();
            }

            total.Stop();
            EndActivity();
        }
    }
}
