using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Scripts;

public partial class Letter : RigidBody2D
{
    [Export] private int score = 10;
    
    private CollisionPolygon2D polygon;
    private readonly List<Node> touches = new();
    
    public bool IsBread { get; set; }

    public bool IsOk => touches.All(t => t.Name == "Plate" || t is Letter);
    public bool OnPlate => touches.Any(t => t.Name == "Plate");

    public int Score => score;

    public Action ringBell;

    public override void _Ready()
    {
        polygon = GetNode<CollisionPolygon2D>("CollisionPolygon2D");
        BodyEntered += Touch;
        BodyExited += StopTouch;
    }

    private void StopTouch(Node other)
    {
        touches.Remove(other);
    }

    private void Touch(Node other)
    {
        if (other.Name == "BellBody")
        {
            ringBell?.Invoke();
        }
        
        touches.Add(other);
    }

    public int GetTouches()
    {
        return touches.Count;
    }

    public float GetHighestPoint()
    {
        return polygon.Polygon.Min(p => GlobalPosition.Y + ToGlobal(p).Y);
    }
    
    public float GetLowestPoint()
    {
        return polygon.Polygon.Max(p => GlobalPosition.Y + ToGlobal(p).Y);
    }
}