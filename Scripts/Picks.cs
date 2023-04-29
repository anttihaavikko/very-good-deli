using AnttiStarter.SceneChanger;
using AnttiStarter.Utils;
using Godot;

namespace Scripts;

public partial class Picks : Node2D
{
    [Export] private CustomerPick pick1, pick2, pick3;
    [Export] private WordDictionary wordDictionary, adjectives;
    [Export] private SceneChanger sceneChanger;
    
    private GameState State => GetNode<GameState>("/root/GameState");
    
    public override void _Ready()
    {
        SetupPick(pick1);
        SetupPick(pick2);
        SetupPick(pick3);
    }

    private void SetupPick(CustomerPick pick)
    {
        var word = wordDictionary.GetRandomWord(State.Level + 3);
        pick.Setup(word.ToUpper(), Rng.Value < 0.2f ? adjectives.GetRandomWord() : null);
        pick.onPick += () =>
        {
            State.Word = word;
            sceneChanger.ChangeScene("res://Scenes/Main.tscn");
        };
    }
}