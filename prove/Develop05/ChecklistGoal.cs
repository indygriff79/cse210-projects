using System;

namespace GoalTracker
{
    public class ChecklistGoal : Goal
    {
        private int _current;
        private int _target;
        private int _bonus;
        private bool _completed;

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            _target = Math.Max(1, target);
            _bonus = Math.Max(0, bonus);
            _current = 0;
            _completed = false;
        }

        // For loading: set current without awarding points
        public void SetCurrent(int current)
        {
            _current = Math.Max(0, current);
            if (_current >= _target) _completed = true;
        }

        public void ForceComplete()
        {
            _completed = true;
            _current = _target;
        }

        public override int RecordEvent()
        {
            if (_completed)
                return 0;

            _current++;

            if (_current >= _target)
            {
                _completed = true;
                // award points for this event plus bonus
                return Points + _bonus;
            }
            else
            {
                // award normal points for each recorded event
                return Points;
            }
        }

        public override bool IsComplete() => _completed;

        public override string GetUpdated()
        {
            if (_completed)
                return $"Progressed '{Name}' to {_current}/{_target} and completed it! Earned {Points} + bonus {_bonus} = {Points + _bonus} points.";
            else
                return $"Progressed '{Name}' to {_current}/{_target}. Earned {Points} points.";
        }

        public override string GetDetail()
        {
            string checkbox = _completed ? "[X]" : "[ ]";
            return $"{checkbox} {Name} ({Description}) -- Completed {_current}/{_target} times";
        }

        public override string ToDataString()
        {
            // Checklist|name|description|points|current|target|bonus|completed
            return $"Checklist|{Escape(Name)}|{Escape(Description)}|{Points}|{_current}|{_target}|{_bonus}|{_completed}";
        }

        private static string Escape(string s) => s?.Replace("|", "/|") ?? "";
    }
}
