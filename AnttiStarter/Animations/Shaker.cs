using AnttiStarter.Extensions;
using AnttiStarter.Utils;
using Godot;

namespace AnttiStarter.Animations;

public partial class Shaker : Node2D
{
	[Export] private bool decreasing = true;
	[Export] private float angleAmount = 1f;
	[Export] private int shakeEveryNthFrame = 2;

	private float amount, duration, length;
	private Vector2 startPos;
	private int frame;
	private float delayedAmount, delayedDuration;

	private Timer timer = new();

	public override void _Ready()
	{
		startPos = Position;
		timer.OneShot = true;
		timer.Timeout += StartDelayed;
		AddChild(timer);
	}
	
	public void ShakeAfter(float delay, float amt, float dur)
	{
		delayedAmount = amt;
		delayedDuration = dur;
		
		timer.Stop();
		timer.Start(delay);
	}

	private void StartDelayed()
	{
		Shake(delayedAmount, delayedDuration);
	}

	public void Shake(float amt, float dur)
	{
		amount = amt;
		duration = length = dur;
	}

	public override void _Process(double delta)
	{
		if (duration <= 0) return;
		duration -= (float)delta;

		frame++;

		if (frame % shakeEveryNthFrame != 0) return;

		var amt = decreasing ?
			amount * duration / length :
			amount;
		
		Position = startPos.RandomOffset(amt);
		Rotation = Mathf.DegToRad(Rng.Range(-amt, amt)) * 0.1f * angleAmount;

		if (duration <= 0)
		{
			amount = 0;
			duration = 0;
			
			Position = startPos;
			Rotation = 0;
		}
	}
}
