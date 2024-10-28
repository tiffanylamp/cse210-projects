using System;
using System.Collections.Generic;

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } 
    private List<Comment> Comments { get; set; } = new List<Comment>();

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            comment.DisplayComment();
        }
        Console.WriteLine();
    }
}

class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    public void DisplayComment()
    {
        Console.WriteLine($"- {CommenterName}: {Text}");
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video { Title = "C# Basics", Author = "CodeAcademy", Length = 300 };
        video1.AddComment(new Comment("Faith", "This is super helpful!"));
        video1.AddComment(new Comment("Leo", "Great explanation!"));
        video1.AddComment(new Comment("Gee", "Thanks for the tips!"));
        videos.Add(video1);

        Video video2 = new Video { Title = "Advanced C# Techniques", Author = "Tech with Tim", Length = 600 };
        video2.AddComment(new Comment("Ian", "Very informative."));
        video2.AddComment(new Comment("Eve", "I learned a lot!"));
        video2.AddComment(new Comment("Vivian", "Can you do a follow-up?"));
        videos.Add(video2);

        Video video3 = new Video { Title = "C# Design Patterns", Author = "DesignMaster", Length = 420 };
        video3.AddComment(new Comment("George", "Nice breakdown of patterns."));
        video3.AddComment(new Comment("Hannah", "Simplified and easy to understand!"));
        video3.AddComment(new Comment("Ivy", "Would love to see more on this."));
        videos.Add(video3);

        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
