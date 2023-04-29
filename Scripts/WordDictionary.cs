using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Extensions;
using Godot;

namespace Scripts;

public partial class WordDictionary : Node2D
{
    [Export] private string fileName;
    
    private List<string> words = new();

    private void Init()
    {
        var file = FileAccess.Open(fileName, FileAccess.ModeFlags.Read);
        var contents = file.GetAsText();
        words.AddRange(contents.Split("\n"));
    }

    public string GetRandomWord(int length = -1)
    {
        if (!words.Any())
        {
            Init();
        }
        
        return words.Where(word => length < 0 || word.Length == length).ToList().Random();
    }
}