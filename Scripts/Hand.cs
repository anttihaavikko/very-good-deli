using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace Scripts;

public partial class Hand : StaticBody2D
{
	[Export] private PinJoint2D pin;
	[Export] private NodePath target;
	[Export] private StaticBody2D collisionShape;
	[Export] private Bell bell;
	[Export] private Node2D rotatePivot;
	[Export] private Node2D armPos;
	[Export] private Line2D grabLine;
	[Export] private Texture2D handOpen, handGrab;
	[Export] private Sprite2D sprite;
	[Export] private Letters letters;
	[Export] private RigidBody2D body;

	private Vector2 offset;
	private Node2D grabbed;

	public override void _Ready()
	{
		grabLine.Hide();
	}

	public override void _Input(InputEvent @event)
	{
		if (letters.TimeOver) return;
		if (@event is not InputEventMouseButton mouseEvent) return;
		if (mouseEvent.ButtonIndex != MouseButton.Left) return;

		if (!mouseEvent.Pressed)
		{
			Release();
			return;
		}
		
		FindAndAttach();
	}

	public void Release()
	{
		sprite.Texture = handOpen;
		grabbed = null;
		grabLine.Hide();
		pin.NodeB = null;
	}

	private void FindAndAttach()
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		var query = new PhysicsPointQueryParameters2D()
		{
			Position = Position,
			Exclude = new Array<Rid>
			{
				GetRid(),
				body.GetRid()
			}
		};
		var result = spaceState.IntersectPoint(query, 1);
		var match = result.FirstOrDefault();
		if (match != default)
		{
			match.TryGetValue("collider", out var coll);
			bell.Cancel();
			grabbed = (Node2D)coll;
			pin.NodeB = grabbed.GetPath();

			offset = grabbed.ToLocal(GlobalPosition);

			grabLine.Show();
			sprite.Texture = handGrab;
		}
	}

	public override void _Process(double delta)
	{
		// if (letters.Evaluating) return;
		var mp = GetGlobalMousePosition();
		Position = mp.Clamp(new Vector2(-500, -200), new Vector2(3000, 900));
		Input.MouseMode = mp.Y > 900 ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Hidden;
		var dir = armPos.GlobalPosition - GlobalPosition;
		rotatePivot.Rotation = dir.Angle() + Mathf.Pi * 0.5f;

		if (grabbed != default)
		{
			grabLine.Points = new[]
			{
				grabLine.ToLocal(GlobalPosition),
				grabLine.ToLocal(grabbed.ToGlobal(offset))
			};
		}
	}
}
