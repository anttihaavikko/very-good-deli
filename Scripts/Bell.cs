using AnttiStarter.Animations;
using AnttiStarter.Extensions;
using Godot;

namespace Scripts;

public partial class Bell : Node2D
{
    [Export] private Letters letters;
    [Export] private RichTextLabel timeLabel;
    [Export] private Appearer timeLabelAppearer;
    [Export] private CpuParticles2D particles;
    [Export] private AnimationPlayer anim;
    [Export] private AudioStreamPlayer2D sound;

    private readonly Timer timer = new();
    private bool done;
    private int rings;

    private float cooldown;

    public int ExtraRings => rings - 1;

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
        cooldown -= (float)delta;
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
        if (done || cooldown > 0) return;
        cooldown = 0.25f;
        rings++;
        sound.PlayWithVariation();
        letters.HideTutorial();
        anim.Play("BellRing");
        particles.Emitting = true;
        timeLabelAppearer.Toggle(true);
        letters.Waiting = true;
        timer.OneShot = true;
        timer.Stop();
        timer.Start(3f);
    }
}