using System;

namespace GoalTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Goal Tracker ===");
                Console.WriteLine($"Score: {manager.Score}");
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Create new goal");
                Console.WriteLine("2. List goals");
                Console.WriteLine("3. Record event (mark goal progress)");
                Console.WriteLine("4. Save goals");
                Console.WriteLine("5. Load goals");
                Console.WriteLine("6. Quit");
                Console.Write("Select an option (1-6): ");

                string choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateGoalFlow(manager);
                        break;
                    case "2":
                        Console.WriteLine("Your goals:");
                        manager.DisplayGoals();
                        Pause();
                        break;
                    case "3":
                        RecordEventFlow(manager);
                        break;
                    case "4":
                        SaveFlow(manager);
                        break;
                    case "5":
                        LoadFlow(manager);
                        break;
                    case "6":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        static void CreateGoalFlow(GoalManager manager)
        {
            Console.WriteLine("Choose goal type:");
            Console.WriteLine("1. Simple goal (one-time)");
            Console.WriteLine("2. Eternal goal (repeatable)");
            Console.WriteLine("3. Checklist goal (complete N times for bonus)");
            Console.Write("Select (1-3): ");
            string t = Console.ReadLine();

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Description: ");
            string desc = Console.ReadLine() ?? "";

            int points = ReadIntFromConsole("Points for each record (integer): ");

            switch (t)
            {
                case "1":
                    var s = new SimpleGoal(name, desc, points);
                    manager.AddGoal(s);
                    Console.WriteLine("Simple goal created.");
                    break;
                case "2":
                    var e = new EternalGoal(name, desc, points);
                    manager.AddGoal(e);
                    Console.WriteLine("Eternal goal created.");
                    break;
                case "3":
                    int target = ReadIntFromConsole("Target count (how many times to complete): ");
                    int bonus = ReadIntFromConsole("Bonus points awarded upon completion: ");
                    var c = new ChecklistGoal(name, desc, points, target, bonus);
                    manager.AddGoal(c);
                    Console.WriteLine("Checklist goal created.");
                    break;
                default:
                    Console.WriteLine("Invalid selection - aborting create.");
                    break;
            }
            Pause();
        }

        static void RecordEventFlow(GoalManager manager)
        {
            if (manager.GetGoals().Count == 0)
            {
                Console.WriteLine("No goals to record. Create one first.");
                Pause();
                return;
            }

            Console.WriteLine("Select a goal to record an event for:");
            manager.DisplayGoals();
            Console.Write("Enter goal number: ");
            if (int.TryParse(Console.ReadLine(), out int idx))
            {
                idx -= 1;
                if (!manager.RecordEventForGoal(idx))
                {
                    Console.WriteLine("Invalid goal selected.");
                }
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
            Pause();
        }

        static void SaveFlow(GoalManager manager)
        {
            Console.Write("Enter filename to save to (e.g., mygoals.txt): ");
            string file = Console.ReadLine() ?? "goals.txt";
            try
            {
                manager.SaveToFile(file);
                Console.WriteLine($"Saved to {file}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
            Pause();
        }

        static void LoadFlow(GoalManager manager)
        {
            Console.Write("Enter filename to load from (e.g., mygoals.txt): ");
            string file = Console.ReadLine() ?? "goals.txt";
            try
            {
                manager.LoadFromFile(file);
                Console.WriteLine($"Loaded from {file}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
            Pause();
        }

        static int ReadIntFromConsole(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (int.TryParse(s, out value)) return value;
                Console.WriteLine("Please enter a valid integer.");
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
