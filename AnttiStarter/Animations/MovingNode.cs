using System.Diagnostics;
using AnttiStarter.Utils;
using Godot;

namespace AnttiStarter.Animations;

public partial class MovingNode : Node2D
{
	[Export] private Vector2 direction = Vector2.Zero;
	[Export] private float speed = 1f;
	[Export] private bool oneWay = true;
	[Export] private bool randomOffset;

	private Vector2 original;
	private float time;
	private float offset;

	public override void _Ready()
	{
		if (randomOffset)
		{
			offset = Rng.Value * 100f;
		}
		original = Position;
	}

	public override void _Process(double delta)
	{
		time += (float)delta;
		var val = Mathf.Sin(time * 10f * speed + offset);
		val = oneWay ? Mathf.Abs(val) : val;
		Position = original + direction * val;
	}
}
