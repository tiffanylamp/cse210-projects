using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected int DurationInSeconds { get; private set; }
    private string _name;
    private string _description;

    protected MindfulnessActivity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Activity: {_name}");
        Console.WriteLine(_description);
        Console.Write("Enter duration in seconds: ");
        DurationInSeconds = int.Parse(Console.ReadLine());

        Console.WriteLine("\nPrepare to begin...");
        ShowSpinner(3);

        RunActivity();

        Console.WriteLine("\nGreat job! You completed the activity.");
        Console.WriteLine($"Activity duration: {DurationInSeconds} seconds.");
        ShowSpinner(3);
    }


    protected abstract void RunActivity();

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(500);
        }
        Console.WriteLine();
    }
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() 
        : base("Breathing Activity", "This activity will help you relax by guiding your breathing. Focus on your breath and let go of any distractions.")
    {
    }

    protected override void RunActivity()
    {
        int timeElapsed = 0;
        while (timeElapsed < DurationInSeconds)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner(2);
            Console.WriteLine("Breathe out...");
            ShowSpinner(2);
            timeElapsed += 4;
        }
    }
}


public class ReflectionActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What could you learn from this experience?"
    };

    public ReflectionActivity() 
        : base("Reflection Activity", "Reflect on a time when you showed strength. Consider the details and insights of this experience.")
    {
    }

    protected override void RunActivity()
    {
        Random random = new Random();
        Console.WriteLine(_prompts[random.Next(_prompts.Count)]);
        ShowSpinner(3);

        int timeElapsed = 0;
        while (timeElapsed < DurationInSeconds)
        {
            Console.WriteLine(_questions[random.Next(_questions.Count)]);
            ShowSpinner(3);
            timeElapsed += 3;
        }
    }
}

public class ListingActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are some of your personal strengths?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() 
        : base("Listing Activity", "List as many items as you can related to the prompt.")
    {
    }

    protected override void RunActivity()
    {
        Random random = new Random();
        Console.WriteLine(_prompts[random.Next(_prompts.Count)]);
        ShowSpinner(3);

        int timeElapsed = 0;
        int itemCount = 0;
        while (timeElapsed < DurationInSeconds)
        {
            Console.Write("Enter an item: ");
            Console.ReadLine();
            itemCount++;
            timeElapsed += 3;
        }
        Console.WriteLine($"You listed {itemCount} items!");
    }
}

public class Program
{
    public static void Main()
    {
        List<MindfulnessActivity> activities = new List<MindfulnessActivity>
        {
            new BreathingActivity(),
            new ReflectionActivity(),
            new ListingActivity()
        };

        Console.WriteLine("Mindfulness Activities");
        Console.WriteLine("1. Breathing Activity\n2. Reflection Activity\n3. Listing Activity");
        Console.Write("Choose an activity (1-3): ");
        int choice = int.Parse(Console.ReadLine());

        if (choice >= 1 && choice <= activities.Count)
        {
            activities[choice - 1].Start();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
}
