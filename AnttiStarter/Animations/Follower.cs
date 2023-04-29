using Godot;

namespace AnttiStarter.Animations;

public partial class Follower : Node2D
{
    [Export] private Node2D target;
    [Export] private Vector2 offset;
    [Export] private float damping;

    public override void _Process(double delta)
    {
        if (target != default)
        {
            GlobalPosition = target.GlobalPosition.Lerp(GlobalPosition, damping);
        }
    }
}