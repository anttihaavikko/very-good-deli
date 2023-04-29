using Godot;
using Timer = Godot.Timer;

namespace AnttiStarter.Animations;

public partial class Face : Sprite2D
{
	[Export] private Node2D wrapper;
	[Export] private Node2D leftEye, rightEye;
	[Export] private float derpiness = 0.1f;

	[Export] private float lookRange = 20f;
	
	private Vector2 closedSize;
	private RandomNumberGenerator rng;
	private Timer blinkTimer = new();

	public override void _Ready()
	{
		closedSize = new Vector2(leftEye.Scale.X, 0);
		rng = new RandomNumberGenerator();
		
		AddChild(blinkTimer);
		blinkTimer.OneShot = true;
		blinkTimer.Start(GetBlinkDelay());
		blinkTimer.Timeout += Blink;
	}

	private float GetBlinkDelay()
	{
		return rng.RandfRange(1f, 5f);
	}

	private void Blink()
	{
		Blink(leftEye);
		Blink(rightEye);
		blinkTimer.Start(GetBlinkDelay());
	}

	private void Blink(Node2D eye)
	{
		var delay = rng.Randf() * derpiness;
		var tween = GetTree().CreateTween();
		tween.TweenProperty(eye, "scale", closedSize, 0.2f).SetTrans(Tween.TransitionType.Quad).SetDelay(delay);
		tween.TweenProperty(eye, "scale", eye.Scale, 0.2f).SetTrans(Tween.TransitionType.Quad);
	}

	public override void _Process(double delta)
	{
		var mp = GetLocalMousePosition();
		var dir = mp - Position;
		if (dir.Length() > 30f)
		{
			wrapper.Position = dir.Normalized() * 20f;
		}
	}
}
