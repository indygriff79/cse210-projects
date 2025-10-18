using System;

namespace GoalTracker
{
    public class SimpleGoal : Goal
    {
        private bool _completed;

        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _completed = false;
        }

        // External helper to mark completed when loading without awarding points
        public void ForceComplete()
        {
            _completed = true;
        }

        public override int RecordEvent()
        {
            if (_completed)
            {
                return 0; // already complete -> no points
            }
            _completed = true;
            return Points;
        }

        public override bool IsComplete() => _completed;

        public override string GetUpdated()
        {
            return _completed ? $"Completed '{Name}' and earned {Points} points." : $"Recorded '{Name}' (not complete).";
        }

        public override string GetDetail()
        {
            string checkbox = _completed ? "[X]" : "[ ]";
            return $"{checkbox} {Name} ({Description})";
        }

        public override string ToDataString()
        {
            // Simple|name|description|points|completed
            return $"Simple|{Escape(Name)}|{Escape(Description)}|{Points}|{_completed}";
        }

        // Helper to escape '|' characters to avoid breaking save format
        private static string Escape(string s) => s?.Replace("|", "/|") ?? "";

    }
}
