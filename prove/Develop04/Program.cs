using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulActivities
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Activity> activities = new()
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

                Activity selected = choice switch
                {
                    "1" => activities[0],
                    "2" => activities[1],
                    "3" => activities[2],
                    _ => null
                };

                if (selected == null)
                {
                    Console.WriteLine("Invalid option. Try again.");
                    Thread.Sleep(1000);
                    continue;
                }

                selected.RunActivity();
            }
        }
    }
}
