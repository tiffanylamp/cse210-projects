using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _totalPoints = 0;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void ListGoals()
    {
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void RecordEvent(string goalName)
    {
        var goal = _goals.Find(g => g.Name == goalName);
        if (goal is ChecklistGoal checklistGoal)
        {
            checklistGoal.RecordEvent();
        }
        _totalPoints += goal.GetPoints();
        Console.WriteLine($"Points earned: {goal.GetPoints()}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.GetDetailsString());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                
                }
            }
        }
    }

    public int GetTotalPoints() => _totalPoints;
}
