using System;

namespace GoalTracker
{
    // Base Goal class
    public abstract class Goal
    {
        // Encapsulated fields
        private string _name;
        private string _description;
        private int _points;

        // Constructor
        protected Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        // Read-only properties for derived classes / manager
        public string Name => _name;
        public string Description => _description;
        public int Points => _points;

        // Required abstract methods (to be overridden)
        public abstract int RecordEvent();      // returns points gained by recording this event
        public abstract bool IsComplete();      // whether goal is complete (true/false)
        public abstract string GetUpdated();    // short message after recording event (e.g., points gained)
        public abstract string GetDetail();     // string representing the goal for display (with status)

        // For saving/loading - each derived class should provide a serializable representation
        public abstract string ToDataString();  // one-line representation for saving
        public static Goal FromDataString(string dataLine)
        {
            // Expected formats:
            // Simple|name|description|points|completed(true/false)
            // Eternal|name|description|points
            // Checklist|name|description|points|current|target|bonus|completed(true/false)
            // We'll attempt to parse robustly.

            if (string.IsNullOrWhiteSpace(dataLine))
                return null;

            string[] parts = dataLine.Split('|');
            if (parts.Length < 4)
                throw new FormatException("Invalid saved goal line.");

            string type = parts[0];
            string name = parts[1];
            string description = parts[2];

            if (!int.TryParse(parts[3], out int points))
                points = 0;

            switch (type)
            {
                case "Simple":
                    {
                        bool completed = false;
                        if (parts.Length >= 5)
                            bool.TryParse(parts[4], out completed);
                        var g = new SimpleGoal(name, description, points);
                        if (completed)
                            g.ForceComplete(); // internal helper to set completed true without awarding points
                        return g;
                    }
                case "Eternal":
                    {
                        var g = new EternalGoal(name, description, points);
                        return g;
                    }
                case "Checklist":
                    {
                        // Expect parts: Checklist|name|desc|points|current|target|bonus|completed
                        int current = 0, target = 0, bonus = 0;
                        bool completed = false;
                        if (parts.Length >= 6) int.TryParse(parts[4], out current);
                        if (parts.Length >= 7) int.TryParse(parts[5], out target);
                        if (parts.Length >= 8) int.TryParse(parts[6], out bonus);
                        if (parts.Length >= 9) bool.TryParse(parts[7], out completed);

                        var g = new ChecklistGoal(name, description, points, target, bonus);
                        g.SetCurrent(current);
                        if (completed)
                            g.ForceComplete();
                        return g;
                    }
                default:
                    throw new FormatException($"Unknown goal type '{type}' in saved data.");
            }
        }
    }
}
