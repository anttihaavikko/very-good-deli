using System;
using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Extensions;
using Godot;

namespace Scripts;

public partial class Letter : RigidBody2D
{
    [Export] private int score = 10;
    [Export] private CpuParticles2D splash;

    private CollisionPolygon2D polygon;
    private readonly List<Node> touches = new();

    private AudioStreamPlayer2D audio = new();
    
    public Vector2 ResetSpot { get; set; }
    
    public bool IsBread { get; set; }

    public bool IsOk => touches.All(t => t.Name == "Plate" || t is Letter);
    public bool OnPlate => touches.Any(t => t.Name == "Plate");

    public int Score => score;

    public Action ringBell, placedOnPlate;

    public override void _Ready()
    {
        polygon = GetNode<CollisionPolygon2D>("CollisionPolygon2D");
        BodyEntered += Touch;
        BodyExited += StopTouch;
        AddChild(audio);
    }

    private void StopTouch(Node other)
    {
        touches.Remove(other);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        // splash.Position = state.GetContactLocalPosition(0);
    }

    private void Touch(Node other)
    {
        if (other.Name == "Plate")
        {
            placedOnPlate?.Invoke();
        }
        
        if (other.Name == "BellBody")
        {
            ringBell?.Invoke();
        }
        
        touches.Add(other);

        var velocity = LinearVelocity.Length() + Mathf.Abs(AngularVelocity);
        if (velocity > 50f)
        {
            var minVol = -40f;
            var maxVol = -5f;
            audio.VolumeDb = Mathf.Clamp(Mathf.Remap(velocity, 50f, 400f, minVol, maxVol), minVol, maxVol);
            audio.PlayWithVariation();
            splash.Restart();
        }
    }

    public void Grab()
    {
        audio.VolumeDb = -10f;
        audio.PlayWithVariation();
        splash.Restart();
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

    public override void _Process(double delta)
    {
        if (GlobalPosition.Y is > 2000 or < -2500)
        {
            GD.Print($"Reset {Name}");
            GlobalPosition = ResetSpot;
        } 
    }

    public void SetSound(AudioStream sound)
    {
        audio.Stream = sound;
    }
}