using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class Program
{
static void Main(string[] args)
    {
        // 🌟 Extra functionality: loads scriptures from an external file (scriptures.txt)
        List<Scripture> scriptures = LoadScripturesFromFile("scriptures.txt");
        Random random = new Random();
        Scripture scripture = scriptures[random.Next(scriptures.Count)];

        int attempts = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Press Enter to hide more words, or type 'quit' to exit.\n");
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine($"\nAttempts: {attempts}");

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nAll words are hidden. Good job!");
                break;
            }

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
            attempts++;
        }
    }

    /// <summary>
    /// Loads scriptures from a specified file.
    /// Each line in the file should be formatted as: Book|Chapter|VerseStart-VerseEnd|Text
    /// </summary>
    /// <param name="filePath">The path to the scriptures file.</param>
    /// <returns>A list of Scripture objects.</returns>
    static List<Scripture> LoadScripturesFromFile(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();

        foreach (string line in File.ReadLines(filePath))
        {
            string[] parts = line.Split('|');
            if (parts.Length == 4)
            {
                string book = parts[0];
                int chapter = int.Parse(parts[1]);

                string[] verseParts = parts[2].Split('-');
                int verseStart = int.Parse(verseParts[0]);
                int verseEnd = verseParts.Length > 1 ? int.Parse(verseParts[1]) : verseStart;

                string text = parts[3];
                Reference reference = new Reference(book, chapter, verseStart, verseEnd);
                Scripture scripture = new Scripture(reference, text);
                scriptures.Add(scripture);
            }
        }

        return scriptures;
    }
}

// ----------------------
// Class: Reference
// Represents a scripture reference (e.g., "John 3:16" or "Acts 2:25-28").
// ----------------------
class Reference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int _verseEnd;

    /// <summary>
    /// Initializes a new instance of the Reference class for a single verse.
    /// </summary>
    /// <param name="book">The book name (e.g., "John").</param>
    /// <param name="chapter">The chapter number.</param>
    /// <param name="verse">The verse number.</param>
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verse;
        _verseEnd = verse;
    }

    /// <summary>
    /// Initializes a new instance of the Reference class for a range of verses.
    /// </summary>
    /// <param name="book">The book name (e.g., "Acts").</param>
    /// <param name="chapter">The chapter number.</param>
    /// <param name="verseStart">The starting verse number.</param>
    /// <param name="verseEnd">The ending verse number.</param>
    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd;
    }

    /// <summary>
    /// Returns a string representation of the scripture reference.
    /// </summary>
    /// <returns>A formatted string (e.g., "John 3:16" or "Acts 2:25-28").</returns>
    public override string ToString()
    {
        if (_verseStart == _verseEnd)
        {
            return $"{_book} {_chapter}:{_verseStart}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
        }
    }
}

// ----------------------
// Class: Word
// Represents a single word in a scripture, with the ability to be hidden.
// ----------------------
class Word
{
    private string _text;
    private bool _isHidden;

    /// <summary>
    /// Initializes a new instance of the Word class.
    /// </summary>
    /// <param name="text">The actual text of the word.</param>
    public Word(string text)
    {
        _text = text;
        _isHidden = false; // Words are initially visible
    }

    /// <summary>
    /// Hides the word.
    /// </summary>
    public void Hide()
    {
        _isHidden = true;
    }

    /// <summary>
    /// Checks if the word is currently hidden.
    /// </summary>
    /// <returns>True if the word is hidden, false otherwise.</returns>
    public bool IsHidden()
    {
        return _isHidden;
    }

    /// <summary>
    /// Gets the display text of the word. If hidden, returns underscores; otherwise, returns the word itself.
    /// </summary>
    /// <returns>The display text (either the word or underscores).</returns>
    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

// ----------------------
// Class: Scripture
// Represents a complete scripture, including its reference and a list of words.
// Manages hiding words and displaying the scripture.
// ----------------------
class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    /// <summary>
    /// Initializes a new instance of the Scripture class.
    /// </summary>
    /// <param name="reference">The reference for the scripture.</param>
    /// <param name="text">The full text of the scripture.</param>
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        // Splits the text into individual words and creates Word objects.
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    /// <summary>
    /// Hides a random set of words from the scripture that are not already hidden.
    /// </summary>
    public void HideRandomWords()
    {
        int wordsToHide = 3; // Number of words to hide in each attempt
        // Get a list of words that are currently visible
        List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();

        // Hide up to 'wordsToHide' words, or until no more visible words remain
        for (int i = 0; i < wordsToHide && visibleWords.Count > 0; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide(); // Hide the selected word
            visibleWords.RemoveAt(index); // Remove from the temporary list to avoid re-hiding
        }
    }

    /// <summary>
    /// Checks if all words in the scripture are hidden.
    /// </summary>
    /// <returns>True if all words are hidden, false otherwise.</returns>
    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    /// <summary>
    /// Gets the complete display text of the scripture, including its reference and the current state of its words (hidden or visible).
    /// </summary>
    /// <returns>A formatted string of the scripture for display.</returns>
    public string GetDisplayText()
    {
        return $"{_reference}\n{string.Join(" ", _words.Select(w => w.GetDisplayText()))}";
    }
}