using AnttiStarter.Utils;
using Godot;

namespace Scripts;

public partial class RandomScale : Node2D
{
    [Export] private float min = 0.8f;
    [Export] private float max = 1.2f;

    public override void _Ready()
    {
        var s = Rng.Range(min, max);
        Scale *= s;
    }
}