using System;
using System.Runtime;
using AnttiStarter.Extensions;
using AnttiStarter.SceneChanger;
using AnttiStarter.Utils;
using Godot;

namespace Scripts;

public partial class CustomerPick : Control
{
    [Export] private RichTextLabel textBubble;
    [Export] private AnimationPlayer anim;
    [Export] private Texture2D[] hairs;
    [Export] private Sprite2D hair;
    [Export] private Node2D head;
    [Export] private Node2D bubble;
    [Export] private Color[] colors, skinColors;
    [Export] private Sprite2D sleeveLeft, sleeveRight, shirt;
    [Export] private Sprite2D headSprite, neck, leftHand, rightHand;
    [Export] private Sprite2D shirtDeco;
    [Export] private Texture2D[] shirtDecorations;

    public Action onPick;

    public override void _Ready()
    {
        hair.Texture = hairs.Random();
        
        anim.Play("Customer");
        anim.SpeedScale = Rng.Range(0.8f, 1.2f);

        var offset = Rng.Value * 40f * Vector2.Up;
        head.Position += offset;
        bubble.Position += offset * 1.5f;

        hair.SelfModulate = colors.Random();
        shirt.SelfModulate = sleeveLeft.SelfModulate = sleeveRight.SelfModulate = colors.Random();
        headSprite.SelfModulate = neck.SelfModulate = leftHand.SelfModulate = rightHand.SelfModulate = skinColors.Random();
        shirtDeco.SelfModulate = colors.Random();
        shirtDeco.Texture = shirtDecorations.Random();
    }

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