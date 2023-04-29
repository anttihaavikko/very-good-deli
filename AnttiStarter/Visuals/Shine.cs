using Godot;

namespace AnttiStarter.Visuals;

public partial class Shine : Node2D
{
    [Export] private Node2D target;
    [Export] private float distance = 0.1f;

    public override void _Process(double delta)
    {
        Position = Vector2.Zero.MoveToward(ToLocal(target.GlobalPosition), distance);
    }
}