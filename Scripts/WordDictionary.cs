using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Extensions;
using Godot;

namespace Scripts;

public partial class WordDictionary : Node2D
{
    private List<string> words = new();

    private void Init()
    {
        var file = FileAccess.Open("res://enwords.txt", FileAccess.ModeFlags.Read);
        var contents = file.GetAsText();
        words.AddRange(contents.Split("\n"));
    }

    public string GetRandomWord(int length)
    {
        if (!words.Any())
        {
            Init();
        }
        
        return words.Where(word => word.Length == length).ToList().Random();
    }
}