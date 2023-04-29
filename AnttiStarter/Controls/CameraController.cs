using System.Diagnostics;
using Godot;

namespace AnttiStarter.Controls;

public partial class CameraController : Node2D
{
    [Export] private float speed = 1500f;
    [Export] private float edgeSize = 100f;
    [Export] private Node2D camera;

    [Export] private bool panWithLeft = true;
    [Export] private bool panWithRight = true;

    [Export] private bool edgeControls = true;
    [Export] private bool keyControls = true;

    [Export] private bool canZoom = true;
    [Export] private float minZoom = 0.1f;
    [Export] private float maxZoom = 5f;

    private Vector2 direction;
    
    private Vector2 dragStart;
    private bool dragging;

    private Vector2 zoom;

    public override void _Process(double delta)
    {
        zoom = camera.Get("zoom").AsVector2();
        
        direction = Vector2.Zero;
        
        ApplyKeyControls();
        ApplyEdgeControls();

        Position += direction.Normalized() * speed * (float)delta;
        
        ApplyDragControls();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventPanGesture panEvent)
        {
            Zoom(-panEvent.Delta.Y * 0.1f);
        }
        
        if (@event is not InputEventMouseButton mouseEvent) return;

        if (mouseEvent.ButtonIndex == MouseButton.Left && !panWithLeft) return;
        if (mouseEvent.ButtonIndex == MouseButton.Right && !panWithRight) return;

        if (mouseEvent.ButtonIndex == MouseButton.WheelUp) Zoom(0.1f);
        if (mouseEvent.ButtonIndex == MouseButton.WheelDown) Zoom(-0.1f);
        
        dragging = mouseEvent.IsPressed();

        if (dragging)
        {
            dragStart = GetGlobalMousePosition();
        }
    }

    private void Zoom(float amount)
    {
        if (!canZoom) return;
        zoom = (zoom + Vector2.One * amount).Clamp(Vector2.One * minZoom, Vector2.One * maxZoom);
        camera.Set("zoom", zoom);
    }

    private void ApplyDragControls()
    {
        if (!dragging) return;
        
        var mp = GetGlobalMousePosition();
        var diff = dragStart - mp;

        if (diff.Length() > 1f)
        {
            Position += diff;   
        }
    }

    private void ApplyEdgeControls()
    {
        if (!edgeControls) return;
        var mp = GetGlobalMousePosition();
        var win = DisplayServer.WindowGetSize();
        var scaled = new Vector2(win.X / zoom.X, win.Y / zoom.Y);
        var scaledEdgeSize = edgeSize / zoom.X;
        if (mp.X > GlobalPosition.X + scaled.X * 0.5f - scaledEdgeSize) direction += Vector2.Right;
        if (mp.X < GlobalPosition.X - scaled.X * 0.5f + scaledEdgeSize) direction += Vector2.Left;
        if (mp.Y > GlobalPosition.Y + scaled.Y * 0.5f - scaledEdgeSize) direction += Vector2.Down;
        if (mp.Y < GlobalPosition.Y - scaled.Y * 0.5f + scaledEdgeSize) direction += Vector2.Up;
    }

    private void ApplyKeyControls()
    {
        if (!keyControls) return;
        if (Input.IsPhysicalKeyPressed(Key.Left) || Input.IsPhysicalKeyPressed(Key.A)) direction += Vector2.Left;
        if (Input.IsPhysicalKeyPressed(Key.Right) || Input.IsPhysicalKeyPressed(Key.D)) direction += Vector2.Right;
        if (Input.IsPhysicalKeyPressed(Key.Up) || Input.IsPhysicalKeyPressed(Key.W)) direction += Vector2.Up;
        if (Input.IsPhysicalKeyPressed(Key.Down) || Input.IsPhysicalKeyPressed(Key.S)) direction += Vector2.Down;
    }
}