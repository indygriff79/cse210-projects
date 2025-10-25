using System;
using System.Collections.Generic;
using System.Dynamic;

public class Comment
{
    public string CommentName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommentName = commenterName;
        Text = text;
    }
}
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }

    private List<Comment> comments = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }
    public int GetNumComments()
    {
        return comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetNumComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($" - {comment.CommentName}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        var video1 = new Video("Intro to C#", "Tech Tutor", 600);
        video1.AddComment(new Comment("Alice", "This was super helpful!"));
        video1.AddComment(new Comment("Bob", "Loved the examples."));
        video1.AddComment(new Comment("Charlie", "Please make more videos!"));

        var video2 = new Video("Learning Encapsulation", "Code Academy", 480);
        video2.AddComment(new Comment("Dana", "Now I finally understand encapsulation."));
        video2.AddComment(new Comment("Eli", "Great explanation!"));

        var videos = new List<Video> { video1, video2 };
        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}