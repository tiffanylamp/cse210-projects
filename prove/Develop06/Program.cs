using System;
using System.Collections.Generic;
using System.IO;

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        GoalManager goalManager = new GoalManager();  
        bool running = true;  

        while (running)
        {
            Console.WriteLine("\nEternal Quest Program - Menu:");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. List All Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Show Current Score");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option (1-7): ");

        
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateGoal(goalManager);
                    break;
                case "2":
                    goalManager.ListGoals();
                    break;
                case "3":
                    goalManager.SaveGoals();
                    Console.WriteLine("Goals saved successfully.");
                    break;
                case "4":
                    goalManager.LoadGoals();
                    Console.WriteLine("Goals loaded successfully.");
                    break;
                case "5":
                    RecordEvent(goalManager);
                    break;
                case "6":
                    Console.WriteLine($"Current Score: {goalManager.Score}");
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("Exiting program...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CreateGoal(GoalManager goalManager)
    {
        Console.WriteLine("\nSelect Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choose a goal type (1-3): ");
        string goalType = Console.ReadLine();

        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();
        Console.Write("Enter the point value for this goal: ");
        int points = int.Parse(Console.ReadLine());

        switch (goalType)
        {
            case "1":
                goalManager.AddGoal(new SimpleGoal(name, points));
                break;
            case "2":
                goalManager.AddGoal(new EternalGoal(name, points));
                break;
            case "3":
                Console.Write("Enter the required completions: ");
                int requiredCompletions = int.Parse(Console.ReadLine());
                Console.Write("Enter the bonus for completing the checklist goal: ");
                int bonus = int.Parse(Console.ReadLine());
                goalManager.AddGoal(new ChecklistGoal(name, points, requiredCompletions, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
        Console.WriteLine("Goal created successfully.");
    }

    static void RecordEvent(GoalManager goalManager)
    {
        Console.WriteLine("\nAvailable Goals:");
        goalManager.ListGoals();

        Console.Write("Enter the number of the goal to record: ");
        int goalIndex = int.Parse(Console.ReadLine()) - 1;

        if (goalIndex >= 0 && goalIndex < goalManager.Goals.Count)
        {
            goalManager.RecordEvent(goalIndex);
            Console.WriteLine("Event recorded.");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }
}


abstract class Goal
{
    private string _name;
    private int _points;

    protected Goal(string name, int points)
    {
        _name = name;
        _points = points;
    }

    public string Name => _name;
    public int Points => _points;

    public abstract void RecordEvent();
    public abstract string GetStatus();
    public abstract string SaveData();
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, int points) : base(name, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"Goal '{Name}' completed! You earned {Points} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already complete.");
        }
    }

    public override string GetStatus()
    {
        return _isComplete ? "[X]" : "[ ]";
    }

    public override string SaveData()
    {
        return $"SimpleGoal,{Name},{Points},{_isComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Goal '{Name}' recorded! You earned {Points} points.");
    }

    public override string GetStatus()
    {
        return "[∞]";
    }

    public override string SaveData()
    {
        return $"EternalGoal,{Name},{Points}";
    }
}

class ChecklistGoal : Goal
{
    private int _requiredCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, int points, int requiredCount, int bonus) : base(name, points)
    {
        _requiredCount = requiredCount;
        _currentCount = 0;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _currentCount++;
        if (_currentCount >= _requiredCount)
        {
            Console.WriteLine($"Checklist goal '{Name}' completed! You earned {Points + _bonus} points.");
        }
        else
        {
            Console.WriteLine($"Checklist goal '{Name}' progress: {_currentCount}/{_requiredCount}. Points this time: {Points}");
        }
    }

    public override string GetStatus()
    {
        return $"[{_currentCount}/{_requiredCount}]";
    }

    public override string SaveData()
    {
        return $"ChecklistGoal,{Name},{Points},{_currentCount},{_requiredCount},{_bonus}";
    }
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            _goals[index].RecordEvent();
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    public void ShowGoals()
    {
        Console.WriteLine("Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i].Name}");
        }
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.SaveData());
            }
        }
    }
}

