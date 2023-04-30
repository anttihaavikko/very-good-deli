using System;
using System.Text.RegularExpressions;
using AnttiStarter.SceneChanger;
using Godot;

namespace Scripts;

public partial class NameInput : Node2D
{
    [Export] private LineEdit nameEdit;
    [Export] private SceneChanger sceneChanger;

    private bool started;

    private GameState State => GetNode<GameState>("/root/GameState");

    public override void _Ready()
    {
        nameEdit.SelectAll();
        nameEdit.CaretColumn = nameEdit.Text.Length;
        nameEdit.GrabFocus();
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Enter) && !started)
        {
            started = true;
            SaveAndPlay();
        }
    }

    public void SaveAndPlay()
    {
        State.PlayerName = GetCleanName(nameEdit.Text);
        State.PlayerId = OS.GetUniqueId() ?? Guid.NewGuid().ToString();
        sceneChanger.ChangeScene("res://Scenes/Main.tscn");
    }

    private string GetCleanName(string input)
    {
        if (string.IsNullOrEmpty(input)) return "Anonymous";
        State.NameSaved = true;
        var rgx = new Regex("[^a-zA-Z0-9 -]");
        return rgx.Replace(input, "");
    }
}