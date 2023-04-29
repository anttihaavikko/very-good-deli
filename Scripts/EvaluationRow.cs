using AnttiStarter.Animations;
using Godot;

namespace Scripts;

public partial class EvaluationRow : Control
{
	[Export] private RichTextLabel titleLabel, valueLabel, changeLabel;
	
	public void Setup(string title, string value, string change)
	{
		titleLabel.Text = title;
		valueLabel.Text = value;
		changeLabel.Text = $"[right]{change}[/right]";
	}
}
