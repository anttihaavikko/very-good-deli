using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Animations;
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
    [Export] private Appearer paper;
    [Export] private PackedScene evaluationRow;
    [Export] private Control evaluationContainer;
    [Export] private PackedScene spacer;
    [Export] private RichTextLabel levelTimer;

    private readonly List<Letter> ingredients = new();
    private bool evaluating;
    private float timeLeft = 120f;

    private GameState State => GetNode<GameState>("/root/GameState");

    public override void _Ready()
    {
        var word = State.Word ?? wordDictionary.GetRandomWord(2 + State.Level);
        timeLeft = word.Length * 15f;
        
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
        if (!evaluating)
        {
            timeLeft = Mathf.Max(timeLeft - (float)delta, 0);
            var seconds = Mathf.RoundToInt(timeLeft);
            levelTimer.Text = $"{Mathf.FloorToInt(seconds / 60f)}:{seconds % 60:00}";
        }
        
        if (Input.IsKeyPressed(Key.N))
        {
            sceneChanger.ChangeScene("res://Scenes/WordPick.tscn");
        }
        
        if (Input.IsKeyPressed(Key.E) && !evaluating)
        {
            Evaluate();
        }
    }

    private void Evaluate()
    {
        paper.Toggle();
        evaluating = true;
        
        var top = ingredients.OrderBy(i => i.GetHighestPoint()).First();
        var bottom = ingredients.OrderByDescending(i => i.GetLowestPoint()).First();
        var height = Mathf.RoundToInt(Mathf.Abs(bottom.GetLowestPoint() - top.GetHighestPoint()) * 0.02f);
        
        var breadness = 0f;
        breadness += top.IsBread ? 0.5f : 0;
        breadness += bottom.IsBread ? 0.5f : 0;

        var rowDelay = 1.3f;
        var amount = ingredients.Sum(i => i.Score) * State.Level;
        var breadnessBonus = Mathf.RoundToInt(breadness * amount);
        var missing = ingredients.Any(i => i.OnPlate) ? ingredients.Count(i => !i.IsOk) : ingredients.Count;
        var penalty = Mathf.RoundToInt(1f * missing / ingredients.Count * amount) * 2;
        var heightBonus = height * 3;
        var timeBonus = Mathf.RoundToInt(timeLeft);
        var total = Mathf.Max(0, amount + breadnessBonus + heightBonus - penalty);
        
        var totalDelay = 0f;
        
        ShowEvaluationRow("Base price", GetDesc(), amount.WithSign(), 0.1f);
        ShowEvaluationRow("Breadness", AsPercent(breadness), breadnessBonus.WithSign(), rowDelay * 1);
        ShowEvaluationRow("Height", height + " cm", heightBonus.WithSign(), rowDelay * 2);
        ShowEvaluationRow("Time left", timeBonus + " s", timeBonus.WithSign(), rowDelay * 3);

        if (penalty > 0)
        {
            ShowEvaluationRow("Missing ingredients", missing.ToString(), (-penalty).ToString(), rowDelay * 4);
            totalDelay += rowDelay;
        }
        
        ShowEvaluationRow("Total", "", total.ToString(), rowDelay * 4 + totalDelay, true);
    }

    private string GetDesc()
    {
        var lvl = State.Level;
        if (lvl > 7) return "GOD-TIER";
        if (lvl > 6) return "LUXURIOUS";
        if (lvl > 5) return "DECADENT";
        if (lvl > 4) return "ARTISAN";
        if (lvl > 3) return "NICE";
        if (lvl > 2) return "DECENT";
        if (lvl > 1) return "BASIC";
        return "ENTRY LEVEL";
    }

    private string AsMulti(float value)
    {
        return "x" + value.ToString("F1");
    }

    private string AsPercent(float value)
    {
        return Mathf.RoundToInt(value * 100) + "%";
    }

    private void ShowEvaluationRow(string title, string value, string change, float delay, bool addSpacer = false)
    {
        var timer = new Timer();
        timer.OneShot = true;
        timer.Timeout += () =>
        {
            if (addSpacer)
            {
                evaluationContainer.AddChild(spacer.Instantiate());
            }
            
            var row = evaluationRow.Instantiate() as EvaluationRow;
            row!.Setup(title, value, change);
            evaluationContainer.AddChild(row);
        };
        AddChild(timer);
        timer.Start(delay);
    }

    private void SpawnLetter(int index, bool isBread)
    {
        var prefab = letters[index];
        var letter = prefab.Instantiate() as Letter;
        letter!.Modulate = isBread ? breadColor :  colors.Random();
        letter.IsBread = isBread;
        ingredients.Add(letter);
        spawn.AddChild(letter);
    }
}