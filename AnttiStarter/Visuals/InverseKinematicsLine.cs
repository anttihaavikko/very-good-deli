using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Extensions;
using Godot;

namespace AnttiStarter.Visuals;

[Tool]
public partial class InverseKinematicsLine : Line2D
{
    [Export] private NodePath targetPath;
    [Export] private float length = 100f;
    [Export] private bool limb = true;
    [Export] private bool flip;
    [Export] private bool bezier;
    
    private Node2D target;

    private readonly List<LimbSegment> chain = new();

    public override void _Ready()
    {
        Init();
        // length = GlobalPosition.DistanceTo(target.GlobalPosition) / chain.Count;
    }

    private void Init()
    {
        target ??= GetNode<Node2D>(targetPath);

        if (chain.Any()) return;

        const int amt = 2;
        for (var i = 0; i < amt; i++)
        {
            chain.Add(new LimbSegment(length, i, limb && i == amt - 1));   
        }
    }

    private void UpdateChain()
    {
        if (target == default) return;

        var reversed = chain.ToList();
        reversed.Reverse();

        var pos = target.GlobalPosition;
        var normal = (pos - GlobalPosition).Normalized();
        foreach (var limbSegment in reversed)
        {
            limbSegment.Follow(pos, normal, flip);
            pos = limbSegment.start;
        }

        pos = GlobalPosition;
        foreach (var limbSegment in chain)
        {
            limbSegment.Constrain(pos);
            pos = limbSegment.end;
        }
    }

    public override void _Process(double delta)
    {
        Init();
        UpdateChain();
        
        if (Engine.IsEditorHint())
        {
            QueueRedraw();
            return;
        }
        
        this.SetLimbPoints(chain[0].end, chain[1].end, bezier);
    }

    public override void _Draw()
    {
        if (!Engine.IsEditorHint()) return;
        if (target == default || !chain.Any()) return;
        DrawLine(ToLocal(GlobalPosition), ToLocal(chain[0].end), DefaultColor, Width);
        DrawLine(ToLocal(chain[1].end), ToLocal(chain[0].end), DefaultColor, Width);
    }

    internal class LimbSegment
    {
        public Vector2 start, end;
        private readonly float length;
        private bool isTip;

        public LimbSegment(float len, int index, bool tip)
        {
            start = new Vector2(len * index, 0);
            end = new Vector2(len * (index + 1), 0);
            length = len;
            isTip = tip;
        }

        public void Follow(Vector2 target, Vector2 normal, bool flip)
        {
            var diff = target - start;
            var dir = diff.Normalized() * length;

            if (isTip)
            {
                var flipped = dir.Reflect(normal).Normalized() * length;
                var flipMod = flip ? -1 : 1;
                if (normal.Y * flipMod < 0)
                {
                    dir = dir.X > flipped.X ? flipped : dir;
                }
                
                if (normal.Y * flipMod > 0)
                {
                    dir = dir.X < flipped.X ? flipped : dir;
                }
            }

            if (dir == Vector2.Zero) return;
            end = target;
            start = end - dir;
        }

        public void Constrain(Vector2 pos)
        {
            var dir = end - start;
            if (dir == Vector2.Zero) return;
            start = pos;
            end = start + dir.Normalized() * length;
        }
    }
}

