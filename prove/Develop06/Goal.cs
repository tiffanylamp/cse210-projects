public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;
    private bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;
    public bool IsComplete => _isComplete;

    public void MarkComplete()
    {
        _isComplete = true;
    }

    public abstract int GetPoints();
    public abstract string GetDetailsString();
}
