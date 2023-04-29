using AnttiStarter.Animations;
using Godot;

namespace Scripts;

public partial class Bell : Node2D
{
    [Export] private Letters letters;
    [Export] private RichTextLabel timeLabel;
    [Export] private Appearer timeLabelAppearer;

    private readonly Timer timer = new();

    public override void _Ready()
    {
        AddChild(timer);
        timer.Timeout += () =>
        {
            timeLabelAppearer.Toggle(false);
            letters.Evaluate();
        };
    }

    public override void _Process(double delta)
    {
        timeLabel.Text = Mathf.CeilToInt(timer.TimeLeft).ToString();
    }

    public void Cancel()
    {
        timeLabelAppearer.Toggle(false);
        letters.Waiting = false;
        timer.Stop();
    }

    public void Ring()
    {
        timeLabelAppearer.Toggle(true);
        letters.Waiting = true;
        timer.OneShot = true;
        timer.Stop();
        timer.Start(3f);
    }
}