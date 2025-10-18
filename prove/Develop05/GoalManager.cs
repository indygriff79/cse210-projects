using System;
using System.Collections.Generic;
using System.IO;

namespace GoalTracker
{
    public class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score = 0;

        public int Score => _score;

        public void AddGoal(Goal g)
        {
            if (g != null) _goals.Add(g);
        }

        public IReadOnlyList<Goal> GetGoals() => _goals.AsReadOnly();

        public void DisplayGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals created yet.");
                return;
            }

            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetail()}");
            }
        }

        public bool RecordEventForGoal(int index)
        {
            if (index < 0 || index >= _goals.Count) return false;

            Goal g = _goals[index];
            int earned = g.RecordEvent();
            if (earned > 0)
            {
                _score += earned;
            }
            Console.WriteLine(g.GetUpdated());
            Console.WriteLine($"Total score: {_score}");
            return true;
        }

        public void SaveToFile(string filename)
        {
            using StreamWriter sw = new StreamWriter(filename);
            // first line: score
            sw.WriteLine(_score);
            // then each goal as a one-line data string
            foreach (var g in _goals)
            {
                sw.WriteLine(g.ToDataString());
            }
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("Save file not found.", filename);

            string[] lines = File.ReadAllLines(filename);
            if (lines.Length == 0)
                throw new InvalidDataException("Save file is empty.");

            // reset current state
            _goals.Clear();
            _score = 0;

            // first line is score
            if (int.TryParse(lines[0], out int savedScore))
                _score = savedScore;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                Goal g;
                try
                {
                    g = Goal.FromDataString(line);
                }
                catch (FormatException)
                {
                    // skip malformed lines
                    continue;
                }

                if (g != null) _goals.Add(g);
            }
        }
    }
}
