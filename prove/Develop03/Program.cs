using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("1 Nephi", 3, 7), "I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."),
            new Scripture(new Reference("Alma", 37, 6, 7), "By small and simple things are great things brought to pass; and small means in many instances doth confound the wise. And the Lord God doth work by means to bring about his great and eternal purposes."),
            new Scripture(new Reference("Ether", 12, 27), "And if men come unto me I will show unto them their weakness. I give unto men weakness that they may be humble; and my grace is sufficient for all men that humble themselves before me."),
            new Scripture(new Reference("Mosiah", 2, 17), "When ye are in the service of your fellow beings ye are only in the service of your God."),
            new Scripture(new Reference("Moroni", 10, 4, 5), "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost.")
        };

        
        Random random = new Random();
        Scripture selectedScripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            selectedScripture.DisplayScripture();

            Console.WriteLine("\nPress Enter to hide some words or type 'quit' to end.");
            string input = Console.ReadLine().ToLower();

            if (input == "quit")
            {
                break;
            }
            else
            {
                
                selectedScripture.HideRandomWords(3);

                if (selectedScripture.AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine("All words are hidden. Well done!\n");
                    break;
                }
            }
        }
    }
}

class Reference
{
    private string book;
    private int chapter;
    private int startVerse;
    private int? endVerse; 
    public Reference(string book, int chapter, int verse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = verse;
        this.endVerse = null;
    }

   
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = endVerse;
    }

    public string GetReferenceString()
    {
        if (endVerse == null)
            return $"{book} {chapter}:{startVerse}";
        else
            return $"{book} {chapter}:{startVerse}-{endVerse}";
    }
}

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

    public string GetWordText()
    {
        return isHidden ? "_____" : text;
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void DisplayScripture()
    {
        Console.WriteLine(reference.GetReferenceString());
        foreach (Word word in words)
        {
            Console.Write(word.GetWordText() + " ");
        }
        Console.WriteLine();
    }

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

    public bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden());
    }
}
