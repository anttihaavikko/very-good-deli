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

    public override void _Ready()
    {
        breads.Get<Letter>().ForEach(b => Init(b, true));
        others.Get<Letter>().ForEach(o => Init(o, false));

        adjectiveLabel.Text = adjectives.GetRandomWord();
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
        leaderboards.Toggle();
    }
}