public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override int GetPoints()
    {
        return IsComplete ? Points : 0;
    }

    public override string GetDetailsString()
    {
        return $"[ ] {Name}: {Description} (Points: {Points})";
    }
}
