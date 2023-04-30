using Godot;

namespace Scripts;

public partial class Nudger : Area2D
{
    [Export] private RigidBody2D body;
    [Export] private float nudgeAmount = 1f;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D node)
    {
        GD.Print("Pushing");
        var other = node.GetNode<RigidBody2D>(".");
        body.ApplyForce(-other.LinearVelocity * 50f * nudgeAmount);
    }
}