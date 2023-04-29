using AnttiStarter.Extensions;
using Godot;
using Leaderboards;

namespace AnttiStarter.Leaderboards;

public partial class ScoreRow : Node
{
	[Export] private Label namePart, scorePart;
	[Export] private Sprite2D flag;
	[Export] private bool separateThousands = true;
	
	public void Init(LeaderBoardScore entry)
	{
		namePart.Text = $"{entry.position}. {entry.name}";
		scorePart.Text = separateThousands ? int.Parse(entry.score).AsScore() : entry.score;
		var pos = FlagManager.Get(entry.locale);
		flag.RegionRect = new Rect2(pos.X, pos.Y, 32, 32);
	}
}
