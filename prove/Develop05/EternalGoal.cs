using System;

namespace GoalTracker
{
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        { }

        public override int RecordEvent()
        {
            // Never completes; each event grants points
            return Points;
        }

        public override bool IsComplete() => false;

        public override string GetUpdated()
        {
            return $"Recorded '{Name}' and earned {Points} points.";
        }

        public override string GetDetail()
        {
            // No completion checkbox since it's eternal; still show [ ] to indicate not a one-time completion
            return $"[~] {Name} ({Description})";
        }

        public override string ToDataString()
        {
            // Eternal|name|description|points
            return $"Eternal|{Escape(Name)}|{Escape(Description)}|{Points}";
        }

        private static string Escape(string s) => s?.Replace("|", "/|") ?? "";
    }
}
