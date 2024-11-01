public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override int GetPoints()
    {
        return Points; // Always return the points for each recording.
    }

    public override string GetDetailsString()
    {
        return $"{Name}: {Description} (Points per record: {Points})";
    }
}
