using System.Linq;
using AnttiStarter.Extensions;
using Godot;

namespace AnttiStarter.Animations;

public partial class Appearer : Node
{
    [Export] private bool startShown;
    [Export] private float showAfter = -1f;

    [Export] private Tween.TransitionType showTransition = Tween.TransitionType.Elastic;
    [Export] private float showDuration = 0.3f;
    
    [Export] private Tween.TransitionType hideTransition = Tween.TransitionType.Elastic;
    [Export] private float hideDuration = 0.3f;

    [Export] private bool isOneSized;
    [Export] private Vector2 closedSize = Vector2.Zero;

    [Export] private AudioStreamPlayer2D sound;

    private bool shown;
    private Vector2 size;

    private Timer timer = new();

    public override void _Ready()
    {
        size = isOneSized ? Vector2.One : Get("scale").AsVector2();

        closedSize = new Vector2(closedSize.X * size.X, closedSize.Y * size.Y);
        shown = startShown;
        
        if (startShown)
        {
            Set("scale", size);
            return;
        }
        
        Set("scale", closedSize);

        timer.OneShot = true;
        timer.Timeout += Toggle;

        if (showAfter >= 0)
        {
            AddChild(timer);
            timer.Stop();
            timer.Start(showAfter);
        }
    }

    public void Toggle(bool state, float delay = 0f)
    {
        if (shown == state) return;

        if (state && sound != default)
        {
            sound.GlobalPosition = Get("global_position").AsVector2();
            sound.PlayWithVariation();
        }  
        
        shown = state;
        
        var tween = GetTree().CreateTween();

        tween.TweenProperty(this, "scale", shown ? size : closedSize, shown ? showDuration : hideDuration)
            .SetTrans(shown ? showTransition : hideTransition)
            .SetDelay(delay)
            .SetEase(Tween.EaseType.Out);
    }

    public void Toggle()
    {
        Toggle(!shown);
    }
}