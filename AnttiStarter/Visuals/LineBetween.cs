using System.Collections.Generic;
using AnttiStarter.Extensions;
using Godot;

namespace AnttiStarter.Visuals;

public partial class LineBetween : Line2D
{
	[Export] private Node2D mid, tip;
	[Export] private bool bezier;

	public override void _Process(double delta)
	{
		this.SetLimbPoints(mid.GlobalPosition, tip.GlobalPosition, bezier);
	}
}