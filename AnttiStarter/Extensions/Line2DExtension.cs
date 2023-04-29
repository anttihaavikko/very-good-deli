using System.Collections.Generic;
using Godot;

namespace AnttiStarter.Extensions;

public static class Line2DExtension
{
    public static void SetLimbPoints(this Line2D line, Vector2 mid, Vector2 tip, bool bezier)
    {
        if (bezier)
        {
            var points = new List<Vector2> { Vector2.Zero };
			
            for (var i = 1; i < line.Points.Length; i++) {
                var t = i * 1f / (line.Points.Length - 1);
                var a = Mathf.Pow(1f - t, 2f) * Vector2.Zero;
                var b = 2f * (1f - t) * t * line.ToLocal(mid);
                var c = Mathf.Pow(t, 2f) * line.ToLocal(tip);
                points.Add(a + b + c);
            }

            line.Points = points.ToArray();

            return;
        }
		
        line.Points = new[]
        {
            Vector2.Zero, 
            line.ToLocal(mid),
            line.ToLocal(tip)
        };
    }
}