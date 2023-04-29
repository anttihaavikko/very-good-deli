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
    
    public override void _Ready()
    {
        for (var i = 0; i < 5; i++)
        {
            SpawnLetter();
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.R))
        {
            sceneChanger.ChangeScene("Main");
        }
    }

    private void SpawnLetter()
    {
        var prefab = letters.ToList().Random();
        var letter = prefab.Instantiate();
        ((CanvasItem)letter).Modulate = colors.Random();
        GD.Print($"Spawning {letter.Name}");
        spawn.AddChild(letter);
    }
}