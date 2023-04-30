using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Animations;
using Godot;
using AnttiStarter.Extensions;
using AnttiStarter.SceneChanger;
using AnttiStarter.Utils;
using Range = Godot.Range;

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
    [Export] private Appearer nextButton;
    [Export] private Bell bell;
    [Export] private RichTextLabel scoreLabel;
    [Export] private Hand hand;
    [Export] private Control paperRotator;
    [Export] private LifeDisplay lifeDisplay;
    [Export] private Control blurLayer;
    [Export] private Appearer buildHelp, ringHelp;
    [Export] private Appearer quitButton, againButton;
    [Export] private AudioStream[] letterSounds;

    private readonly List<Letter> ingredients = new();
    private bool evaluating;
    private float timeLeft = 120f;
    private bool over;
    
    public bool Waiting { get; set; }

    public bool TimeOver => timeLeft <= 0;
    public bool Evaluating => evaluating;

    private bool tutorialDone;

    private GameState State => GetNode<GameState>("/root/GameState");
    private Music Music => GetNode<Music>("/root/Music");

    public override void _Ready()
    {
        Music.Lowpass(false);
        
        if (State.Level == 1)
        {
            buildHelp.Toggle(true, 1.5f);    
        }
        
        Input.MouseMode = Input.MouseModeEnum.Hidden;
            
        UpdateScore();
        
        var word = State.Word ?? wordDictionary.GetRandomWord(2 + State.Level);
        var mod = Mathf.Pow(0.9f, State.Level - 1);
        timeLeft = word.Length * mod * 20f;

        GD.Print($"Word is {word}");
        
        var idx = 0;
        foreach (var letter in word.ToCharArray())
        {
            var index = GetIndex(letter.ToString().ToUpper());
            SpawnLetter(Mathf.Max(index, 0), idx, idx == 0 || idx == word.Length - 1);
            idx++;
        }
    }

    public void ShowRingTutorial()
    {
        if (tutorialDone || State.Level > 1) return;
        tutorialDone = true;
        buildHelp.Toggle();
        ringHelp.Toggle(true, 2f);
    }

    private void UpdateScore()
    {
        scoreLabel.Text = $"[right]{State.Score}[/right] ";
    }

    public void RingBell()
    {
        bell.Ring();
        hand.Release();
    }

    private static int GetIndex(string letter)
    {
        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(letter, StringComparison.Ordinal);
    }

    public void NextLevel()
    {
        Music.Lowpass(false);
        sceneChanger.ChangeScene("res://Scenes/WordPick.tscn");
    }

    public override void _Process(double delta)
    {
        if (!Waiting && !evaluating)
        {
            timeLeft = Mathf.Max(timeLeft - (float)delta, 0);
            var seconds = Mathf.RoundToInt(timeLeft);
            levelTimer.Text = $"{Mathf.FloorToInt(seconds / 60f)}:{seconds % 60:00}";

            if (timeLeft <= 0 && !over)
            {
                over = true;
                hand.Release();
                bell.Ring();
            }
        }
        
        if (!OS.IsDebugBuild()) return;
        
        if (Input.IsKeyPressed(Key.N))
        {
            sceneChanger.ChangeScene("res://Scenes/WordPick.tscn");
        }
        
        if (Input.IsKeyPressed(Key.E) && !evaluating)
        {
            Evaluate();
        }
    }

    public void Evaluate()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
        paper.Toggle();
        
        Music.Lowpass(true);
        
        blurLayer.Show();
        
        var tween = GetTree().CreateTween();
        var angle = 0.1f;
        paperRotator.Rotation = Rng.Range(-angle, angle);
        tween.TweenProperty(paperRotator, "rotation", Rng.Range(-angle, angle), 0.5f)
            .SetTrans(Tween.TransitionType.Elastic)
            .SetEase(Tween.EaseType.Out);
        
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
        var heightBonus = height * 4;
        var time = Mathf.RoundToInt(timeLeft);
        var timeBonus = time * State.Level;
        var ringPenalty = Mathf.RoundToInt((Mathf.Pow(1.2f, bell.ExtraRings) - 1) * amount);
        var total = Mathf.Max(0, amount + breadnessBonus + timeBonus + heightBonus - penalty - ringPenalty);
        
        var totalDelay = 0f;
        
        ShowEvaluationRow("Base price", GetDesc(), amount.WithSign(), 0.1f);
        ShowEvaluationRow("Sandwichness", AsPercent(breadness), breadnessBonus.WithSign(), rowDelay * 1);
        ShowEvaluationRow("Height", height + " cm", heightBonus.WithSign(), rowDelay * 2);
        ShowEvaluationRow("Time left", time + " s", timeBonus.WithSign(), rowDelay * 3);
        
        if (penalty > 0)
        {
            ShowEvaluationRow("Missing ingredients", missing.ToString(), (-penalty).ToString(), rowDelay * 4 + totalDelay);
            totalDelay += rowDelay;
            lifeDisplay.Lose(missing);
            State.Lives -= missing;
        }

        if (ringPenalty > 0)
        {
            ShowEvaluationRow("Extra rings", bell.ExtraRings.ToString(), (-ringPenalty).ToString(), rowDelay * 4 + totalDelay);
            totalDelay += rowDelay;
        }
        
        ShowEvaluationRow("Total", "", total.ToString(), rowDelay * 4 + totalDelay, true);

        State.Score += total;
    }

    private string GetDesc()
    {
        var lvl = State.Level;
        if (lvl > 11) return "GOD-TIER";
        if (lvl > 10) return "MIND-BLOWING";
        if (lvl > 9) return "LUXURIOUS";
        if (lvl > 8) return "DECADENT";
        if (lvl > 7) return "ARTISAN";
        if (lvl > 6) return "MARVELOUS";
        if (lvl > 5) return "EXCELLENT";
        if (lvl > 4) return "GOOD";
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

            if (addSpacer)
            {
                UpdateScore();
                if (State.Lives <= 0)
                {
                    ShowEvaluationRow("You're fired...", "", "", 1f);
                    
                    quitButton.Toggle(true, 0.8f);
                    againButton.Toggle(true, 0.4f);
                    
                    return;
                }
                
                nextButton.Toggle();
            }
        };
        
        AddChild(timer);
        timer.Start(delay);
    }

    private void SpawnLetter(int index, int count, bool isBread)
    {
        var makeBread = isBread || count > 3 && Rng.Value < 0.1f;
        var prefab = letters[index];
        var letter = prefab.Instantiate() as Letter;
        spawn.AddChild(letter);
        letter!.Position += Vector2.Zero.RandomOffset(25f);
        letter.ResetSpot = spawn.GlobalPosition;
        var typeIndex = Rng.Range(0, colors.Length - 1);
        letter.Modulate = makeBread ? breadColor : colors[typeIndex];
        letter.SetSound(makeBread ? letterSounds.Last() : letterSounds[typeIndex]);
        letter.IsBread = makeBread;
        letter.ringBell += RingBell;
        letter.placedOnPlate += ShowRingTutorial;
        ingredients.Add(letter);
    }

    public void HideTutorial()
    {
        ringHelp.Toggle(false);
    }

    public void PlayAgain()
    {
        Music.Lowpass(false);
        State.Reset();
        sceneChanger.ChangeScene("res://Scenes/Main.tscn");
    }

    public void QuitToMenu()
    {
        Music.Lowpass(false);
        State.Reset();
        GD.Print("Go to menu");
    }
}