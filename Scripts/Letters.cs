using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using AnttiStarter.Extensions;
using AnttiStarter.SceneChanger;

namespace Scripts;

public partial class Letters : Node2D
{
    [Export] private PackedScene[] letters;
    [Export] private Node2D spawn;
    [Export] private SceneChanger sceneChanger;
    [Export] private Color[] colors;
    [Export] private Color breadColor;
    [Export] private WordDictionary wordDictionary;

    public override void _Ready()
    {
        var word = wordDictionary.GetRandomWord(4);
        GD.Print($"Word is {word}");
        
        var idx = 0;
        foreach (var letter in word.ToCharArray())
        {
            var index = GetIndex(letter.ToString().ToUpper());
            SpawnLetter(Mathf.Max(index, 0), idx == 0 || idx == word.Length - 1);
            idx++;
        }
    }

    private static int GetIndex(string letter)
    {
        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(letter, StringComparison.Ordinal);
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.R))
        {
            sceneChanger.ChangeScene("Main");
        }
    }

    private void SpawnLetter(int index, bool isBread)
    {
        var prefab = letters[index];
        var letter = prefab.Instantiate();
        ((CanvasItem)letter).Modulate = isBread ? breadColor :  colors.Random();
        spawn.AddChild(letter);
    }
}