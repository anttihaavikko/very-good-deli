using AnttiStarter.Animations;
using Godot;

namespace Scripts;

public partial class Bell : Node2D
{
    [Export] private Letters letters;
    [Export] private RichTextLabel timeLabel;
    [Export] private Appearer timeLabelAppearer;
    [Export] private CpuParticles2D particles;
    [Export] private AnimationPlayer anim;

    private readonly Timer timer = new();
    private bool done;

    public override void _Ready()
    {
        AddChild(timer);
        timer.Timeout += () =>
        {
            done = true;
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
        if (done) return;
        timeLabelAppearer.Toggle(false);
        letters.Waiting = false;
        timer.Stop();
    }

    public void Ring()
    {
        if (done) return;
        anim.Play("BellRing");
        particles.Emitting = true;
        timeLabelAppearer.Toggle(true);
        letters.Waiting = true;
        timer.OneShot = true;
        timer.Stop();
        timer.Start(3f);
    }
}