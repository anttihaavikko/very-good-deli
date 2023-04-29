using System;
using AnttiStarter.Extensions;
using Godot;

namespace AnttiStarter.Controls;

public abstract partial class Draggable : Control
{
    [Export] private bool snapToGrid = true;
    [Export] private float gridSize = 100f;

    [Export] private bool lockOnDrop = true;
    [Export] private bool returnOnFail = true;

    private bool dragging;
    private Vector2 offset;
    private Vector2 start;

    private Tween tween;
    
    public bool CanDropHere { get; private set; }
    public bool Locked { get; private set; }

    public Action<Draggable> onDrag, onDrop, onReturn;

    protected abstract bool CanDrop(Vector2 at);

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseEvent || Locked || !this.IsMouseInside()) return;

        if (mouseEvent.ButtonIndex != MouseButton.Left) return;

        var wasDragging = dragging;
        dragging = mouseEvent.IsPressed();
        offset = GetGlobalMousePosition() - Position;

        if (dragging)
        {
            start = Position;
            MoveToFront();
        }

        if (wasDragging && !dragging)
        {
            Drop();
        }
        
        GetTree().Root.SetInputAsHandled();
    }

    private Vector2 GetCurrentPosition(bool snapped)
    {
        return snapped ? 
            GetCurrentPosition(false).Snapped(new Vector2(gridSize, gridSize)) : 
            GetGlobalMousePosition() - offset;
    }

    private void Return()
    {
        if (!returnOnFail && !CanDropHere) return;
        var pos = CanDropHere ? GetCurrentPosition(snapToGrid) : start;
        this.MoveTo(pos, 0.3f);
    }

    private void Drop()
    {
        CanDropHere = CanDrop(GetCurrentPosition(snapToGrid));
        Return();
        Locked = CanDropHere && lockOnDrop;

        if (CanDropHere)
        {
            onDrop?.Invoke(this);
            return;
        }
        
        onReturn?.Invoke(this);
    }

    public override void _Process(double delta)
    {
        if (!dragging) return;
        Position = GetCurrentPosition(false);
        CanDropHere = CanDrop(GetCurrentPosition(snapToGrid));
        onDrag?.Invoke(this);
    }
}