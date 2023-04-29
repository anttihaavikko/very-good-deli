using System.Linq;
using AnttiStarter.Animations;
using AnttiStarter.Utils;
using Godot;

namespace Scripts;

public partial class LifeDisplay : Control
{
	[Export] private ListWrapper lives;

	private int shown;
	
	private GameState State => GetNode<GameState>("/root/GameState");

	public override void _Ready()
	{
		shown = State.Lives;
		lives.Get<Appearer>().Take(State.Lives).ToList().ForEach(l => l.Toggle());
	}

	public void Lose(int amount)
	{
		var left = Mathf.Max(0, State.Lives - amount);
		var delay = 7f;
		var nodes = lives.Get<Appearer>().Skip(left).Take(State.Lives - left).ToList();
		nodes.Reverse();
		nodes.ForEach(l =>
		{
			l.Toggle(false, delay);
			delay += 0.3f;
		});
	}
}
