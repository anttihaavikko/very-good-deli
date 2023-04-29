using System;
using AnttiStarter.Extensions;
using AnttiStarter.SceneChanger;
using AnttiStarter.Utils;
using Godot;

namespace Scripts;

public partial class CustomerPick : Control
{
    [Export] private RichTextLabel textBubble;

    public Action onPick;

    public void Setup(string word, string adjective)
    {
        textBubble.Text = $"[center]{GetStart()} {WithPrefix(adjective, word)} [b]{word}[/b] {GetBread()}! {GetExtras()}[/center]";
    }

    public void Pick()
    {
        onPick?.Invoke();
    }

    private string GetExtras()
    {
        if (Rng.Value > 0.1f) return "";
        
        return new[]
        {
            "Please hurry!",
            "No onions!",
            "Take your time!",
            "Thank you!",
            "Can you make it vegan?"
        }.Random();
    }

    private string WithPrefix(string adjective, string word)
    {
        return string.IsNullOrEmpty(adjective) ? GetPrefix(word) : $"{GetPrefix(adjective)} {adjective}";
    }

    private string GetPrefix(string adjective)
    {
        return "AEIOUY".Contains(adjective[..1].ToUpper()) ? "an" : "a";
    }

    private string GetMeat()
    {
        return new[]
        {
            "chicken",
            "beef",
            "pork",
            "tofu",
            "cheese",
            "veggie"
        }.Random();
    }

    private string GetStart()
    {
        return new[]
        {
            "I would like",
            "I'll take",
            "Hmm, I want"
        }.Random();
    }

    private string GetBread()
    {
        return new[]
        {
            "burger",
            "sandwich",
            "bagel",
            "sub",
            $"{GetMeat()} wrap"
        }.Random();
    }
}