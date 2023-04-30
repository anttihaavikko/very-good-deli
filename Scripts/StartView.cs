using System.Linq;
using System.Xml;
using AnttiStarter.Animations;
using AnttiStarter.SceneChanger;
using AnttiStarter.Utils;
using Godot;

namespace Scripts;

public partial class StartView : Node2D
{
    [Export] private Appearer leaderboards;
    [Export] private SceneChanger sceneChanger;
    [Export] private Color[] colors;
    [Export] private Color breadColor;
    [Export] private AudioStream[] letterSounds;
    [Export] private ListWrapper breads, others;
    [Export] private Label adjectiveLabel;
    [Export] private WordDictionary adjectives;
    [Export] private Control paper;
    [Export] private Appearer prevButton, nextButton;
    [Export] private Control backBlur, frontBlur;

    private const float Angle = 0.1f;

    private bool boardShown;

    public bool LookingBoard => boardShown;

    public override void _Ready()
    {
        breads.Get<Letter>().ForEach(b => Init(b, true));
        others.Get<Letter>().ForEach(o => Init(o, false));

        adjectiveLabel.Text = adjectives.GetRandomWord();
        
        paper.Rotation = Rng.Range(-Angle, Angle);
    }

    private void Init(Letter letter, bool makeBread)
    {
        var typeIndex = Rng.Range(0, colors.Length - 1);
        letter.Modulate = makeBread ? breadColor : colors[typeIndex];
        letter.SetSound(makeBread ? letterSounds.Last() : letterSounds[typeIndex]);
        letter.IsBread = makeBread;
    }

    public void Play()
    {
        sceneChanger.ChangeScene("res://Scenes/Main.tscn");
    }

    public void Quit()
    {
        GetTree().Quit();
    }

    public void ToggleLeaderboards()
    {
        boardShown = !boardShown;
        var tween = GetTree().CreateTween();
        tween.TweenProperty(paper, "rotation", Rng.Range(-Angle, Angle), 0.5f)
            .SetTrans(Tween.TransitionType.Elastic)
            .SetEase(Tween.EaseType.Out);
        
        leaderboards.Toggle();
        
        prevButton.Toggle(boardShown);
        nextButton.Toggle(boardShown);

        backBlur.Visible = !boardShown;
        frontBlur.Visible = boardShown;

        Input.MouseMode = boardShown ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Hidden;
    }
}