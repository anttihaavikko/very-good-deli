using System.Linq;
using Godot;
using Godot.Collections;

namespace Scripts;

public partial class Hand : StaticBody2D
{
    [Export] private PinJoint2D pin;
    [Export] private NodePath target;
    [Export] private StaticBody2D collisionShape;
    [Export] private Bell bell;

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseEvent) return;
        if (mouseEvent.ButtonIndex != MouseButton.Left) return;

        if (!mouseEvent.Pressed)
        {
            pin.NodeB = null;
            return;
        }
        
        FindAndAttach();
    }

    private void FindAndAttach()
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var query = new PhysicsPointQueryParameters2D()
        {
            Position = Position,
            Exclude = new Array<Rid> { GetRid() }
        };
        var result = spaceState.IntersectPoint(query, 1);
        var match = result.FirstOrDefault();
        if (match != default)
        {
            match.TryGetValue("collider", out var coll);
            bell.Cancel();
            pin.NodeB = ((Node2D)coll).GetPath();
        }
    }

    public override void _Process(double delta)
    {
        Position = GetGlobalMousePosition();
    }
}