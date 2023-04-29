using Godot;

namespace AnttiStarter.Animations;

public partial class PulsatingNode : Node2D
{
    [Export] private float amount = 0.1f;
    [Export] private float speed = 1f;
    [Export] private bool alwaysPositive = true;

    private Vector2 original;
    private float time;

    public override void _Ready()
    {
        original = Scale;
    }

    public override void _Process(double delta)
    {
        time += (float)delta;
        var val = 1f + Mathf.Sin(time * 10f * speed) * amount;
        val = alwaysPositive ? Mathf.Abs(val) : val;
        Scale = original * val;
    }
}