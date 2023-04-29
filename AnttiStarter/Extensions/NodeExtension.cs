using System.Drawing;
using Godot;

namespace AnttiStarter.Extensions;

public static class NodeExtension
{
    public static void MoveTo(this Node node, Vector2 to, float duration = 0.2f, Tween.TransitionType transition = Tween.TransitionType.Elastic, Tween.EaseType easing = Tween.EaseType.InOut)
    {
        node.GetTree().CreateTween().TweenProperty(node, "position", to, duration)
            .SetTrans(transition)
            .SetEase(easing);
    }
    
    public static void MoveToGlobal(this Node node, Vector2 to, float duration = 0.2f, Tween.TransitionType transition = Tween.TransitionType.Elastic, Tween.EaseType easing = Tween.EaseType.InOut)
    {
        node.GetTree().CreateTween().TweenProperty(node, "global_position", to, duration)
            .SetTrans(transition)
            .SetEase(easing);
    }

    public static bool IsMouseInside(this Control node)
    {
        var mp = node.GetGlobalMousePosition();
        return mp.X > node.GlobalPosition.X && mp.X < node.GlobalPosition.X + node.Size.X &&
               mp.Y > node.GlobalPosition.Y && mp.Y < node.GlobalPosition.Y + node.Size.Y;
    }
    
    public static Vector2 GetCenter(this Control node)
    {
        return node.Position + node.Size * 0.5f;
    }
    
    public static Vector2 GetGlobalCenter(this Control node)
    {
        return node.GlobalPosition + node.Size * 0.5f;
    } 
}