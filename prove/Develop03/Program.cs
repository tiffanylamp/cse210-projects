using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Library of scriptures 
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("1 Nephi", 3, 7), "I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."),
            new Scripture(new Reference("Alma", 37, 6, 7), "By small and simple things are great things brought to pass; and small means in many instances doth confound the wise. And the Lord God doth work by means to bring about his great and eternal purposes."),
            new Scripture(new Reference("Ether", 12, 27), "And if men come unto me I will show unto them their weakness. I give unto men weakness that they may be humble; and my grace is sufficient for all men that humble themselves before me."),
            new Scripture(new Reference("Mosiah", 2, 17), "When ye are in the service of your fellow beings ye are only in the service of your God."),
            new Scripture(new Reference("Moroni", 10, 4, 5), "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost.")
        };

        // Select a random scripture 
        Random random = new Random();
        Scripture selectedScripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            selectedScripture.Display();

            Console.WriteLine("\nPress Enter to hide some words or type 'quit' to end.");
            string input = Console.ReadLine().ToLower();

            if (input == "quit")
            {
                break;
            }
            else
            {
                // Hide 
                selectedScripture.HideRandomWords(3);

                // Check if all words are hidden
                if (selectedScripture.AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine("All words are hidden. Well done!  You're doing great\n");
                    break;
                }
            }
        }
    }
}

//Scripture reference, including book name and verses
class Reference
{
    private string book;
    private int chapter;
    private int startVerse;
    private int? endVerse; 

    // Constructor for single verse references (e.g., 1 Nephi 3:7)
    public Reference(string book, int chapter, int verse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = verse;
        this.endVerse = null;
    }

    // Constructor for multiple verse references (e.g., Alma 37:6-7)
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = endVerse;
    }

    public override string ToString()
    {
        if (endVerse == null)
            return $"{book} {chapter}:{startVerse}";
        else
            return $"{book} {chapter}:{startVerse}-{endVerse}";
    }
}

// An individual word in the scripture
class Word
{
    private string text;
    private bool isHidden;

    public Word(string text)
    {
        this.text = text;
        isHidden = false;
    }

    public void Hide()
    {
        isHidden = true;
    }

    public bool IsHidden()
    {
        return isHidden;
    }

    public override string ToString()
    {
        return isHidden ? "_____" : text;
    }
}

// Scripture, including the text and its reference
class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    // Display the scripture text with the hidden words
    public void Display()
    {
        Console.WriteLine(reference);
        foreach (Word word in words)
        {
            Console.Write(word + " ");
        }
        Console.WriteLine();
    }

    // Hide a random set of words in the scripture
    public void HideRandomWords(int count)
    {
        Random random = new Random();
        int hiddenWords = 0;

        while (hiddenWords < count)
        {
            int index = random.Next(words.Count);

            if (!words[index].IsHidden())
            {
                words[index].Hide();
                hiddenWords++;
            }
        }
    }

    // To check if all words are hidden
    public bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden());
    }
}
