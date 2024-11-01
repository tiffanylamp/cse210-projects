public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;

    public ChecklistGoal(string name, string description, int points, int targetCount) 
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _currentCount = 0;
    }

    public override int GetPoints()
    {
        if (IsComplete) return Points + 500; 
        return Points * _currentCount;
    }

    public void RecordEvent()
    {
        if (!IsComplete)
        {
            _currentCount++;
            if (_currentCount >= _targetCount)
            {
                MarkComplete(); 
            }
        }
    }

    public override string GetDetailsString()
    {
        return IsComplete ? $"[X] {Name}: {Description} (Completed: {_currentCount}/{_targetCount})" 
                          : $"[ ] {Name}: {Description} (Completed: {_currentCount}/{_targetCount})";
    }
}
