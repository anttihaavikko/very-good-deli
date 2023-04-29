using System;
using AnttiStarter.Extensions;
using AnttiStarter.SceneChanger;
using Godot;

namespace Scripts;

public partial class CustomerPick : Control
{
    [Export] private RichTextLabel textBubble;

    public Action onPick;

    public void Setup(string word, string adjective)
    {
        textBubble.Text = $"[center]{GetStart()} {WithPrefix(adjective)} {word} {GetBread()}![/center]";
    }

    public void Pick()
    {
        onPick?.Invoke();
    }

    private string WithPrefix(string adjective)
    {
        return string.IsNullOrEmpty(adjective) ? "a" : $"{GetPrefix(adjective)} {adjective}";
    }

    private string GetPrefix(string adjective)
    {
        return "AEIOUY".Contains(adjective[..1].ToUpper()) ? "an" : "a";
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
            "sub"
        }.Random();
    }
}