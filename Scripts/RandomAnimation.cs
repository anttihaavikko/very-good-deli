using AnttiStarter.Utils;
using Godot;

namespace Scripts;

public partial class RandomAnimation : AnimationPlayer
{
    [Export] private string startWithAnimation;

    public override void _Ready()
    {
        SpeedScale = Rng.Range(0.8f, 1.2f);
        Play(startWithAnimation);
    }
}